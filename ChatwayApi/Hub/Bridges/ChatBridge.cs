using Domain.Models;
using Hub.Handlers;
using Hub.Hubs;
using Microsoft.AspNetCore.SignalR;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hub.Bridges {
    public class ChatBridge {

        public readonly IHubContext<ChatHub> _context;
        public readonly ChatService _chatService;
        public readonly UsuarioHandler _usuarioHandler;

        public ChatBridge(IHubContext<ChatHub> context, ChatService chatService, UsuarioHandler usuarioHandler) {
            this._context = context;
            this._chatService = chatService;
            this._usuarioHandler = usuarioHandler;
        }

        public Task NotificarDestinatario(Mensagem mensagem) {
            var chat = _chatService.Get(mensagem.Chat);
            if (mensagem.Remetente == chat.Atendente) {
                return _context.Clients.Client(_usuarioHandler.GetId(chat.Motorista)).SendAsync("MensagemRecebida", mensagem);
            } else {
                return _context.Clients.Client(_usuarioHandler.GetId(chat.Atendente)).SendAsync("MensagemRecebida", mensagem);
            }
        }

        public Chat AtenderPendente(String id) {
            var chat = _chatService.GetPendente();
            chat.Atendente = id;
            _chatService.Update(chat.Id, chat);
            _context.Clients.Client(_usuarioHandler.GetId(chat.Motorista)).SendAsync("ChatAtendido", chat);
            _context.Clients.Group("atendente").SendAsync("ChatAtendido", chat);
            return chat;
        }

        public Chat FinalizarChat(string id) {
            Chat chat = _chatService.Get(id);
            chat.Concluido = true;
            chat = _chatService.Update(id, chat);
            this._context.Clients.Client(_usuarioHandler.GetId(chat.Motorista)).SendAsync("ChatFinalizado", chat);
            return chat;
        }
    }
}
