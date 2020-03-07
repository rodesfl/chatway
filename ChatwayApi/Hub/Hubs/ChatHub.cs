using Domain.Models;
using Hub.Handlers;
using Microsoft.AspNetCore.SignalR;
using Services.Services;
using System;
using System.Threading.Tasks;

namespace Hub.Hubs {
    public class ChatHub : Microsoft.AspNetCore.SignalR.Hub {

        public readonly MensagemService _mensagemService;
        public readonly ChatService _chatService;
        public readonly UsuarioHandler _usuarioHandler;

        public ChatHub(MensagemService mensagemService, ChatService chatService, UsuarioHandler usuarioHandler) {
            this._mensagemService = mensagemService;
            this._chatService = chatService;
            this._usuarioHandler = usuarioHandler;
        }

        public Mensagem EnviarMensagem(Mensagem mensagem) {
            bool notificarPendente = false;
            if (mensagem.Chat == null) {
                notificarPendente = true;
            }
            try {
                _mensagemService.Create(mensagem);
                var chat = _chatService.Get(mensagem.Chat);
                if (notificarPendente) {
                    Clients.Group("atendente").SendAsync("NovoChatPendente", chat);
                } else {
                    NotificarDestinatario(mensagem);
                }
                return mensagem;
            } catch (Exception e) {
                return null;
            }
        }

        public Chat AtenderChatPendente() {
            var chat = _chatService.GetPendente();
            chat.Atendente = _usuarioHandler.GetUsuario(Context.ConnectionId);
            _chatService.Update(chat.Id, chat);
            Clients.Client(_usuarioHandler.GetId(chat.Motorista)).SendAsync("ChatAtendido", chat);
            Clients.Group("atendente").SendAsync("ChatAtendido", chat);
            return chat;
        }

        public Chat FinalizarChat(string id) {
            Chat chat = _chatService.Get(id);
            chat.Concluido = true;
            chat = _chatService.Update(id, chat);
            Clients.Client(_usuarioHandler.GetId(chat.Motorista)).SendAsync("ChatFinalizado", chat);
            return chat;
        }

        public Task NotificarDestinatario(Mensagem mensagem) {
            var chat = _chatService.Get(mensagem.Chat);
            if (mensagem.Remetente == chat.Atendente) {
                return Clients.Client(_usuarioHandler.GetId(chat.Motorista)).SendAsync("MensagemRecebida", mensagem);
            } else {
                return Clients.Client(_usuarioHandler.GetId(chat.Atendente)).SendAsync("MensagemRecebida", mensagem);
            }
        }

        public Task Autenticar(Usuario usuario, string funcao) {
            try {
                this._usuarioHandler.Registrar(Context.ConnectionId, usuario.Id);
                switch (funcao) {
                    case "atendente":
                        return Groups.AddToGroupAsync(Context.ConnectionId, "atendente");
                    case "motorista":
                        return Groups.AddToGroupAsync(Context.ConnectionId, "motorista");
                    default:
                        break;
                }
                return null;
            } catch (Exception e) {
                return null;
            }
        }

        public override async Task OnConnectedAsync() {
            Console.WriteLine(Context.ConnectionId);
            await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
            await base.OnConnectedAsync();
        }
        public override async Task OnDisconnectedAsync(Exception exception) {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, "SignalR Users");
            try {
                this._usuarioHandler.Remover(Context.ConnectionId);
            } catch (Exception e) {
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
