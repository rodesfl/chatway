using Domain.Models;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services {
    public class MensagemService {
        public readonly MensagemRepository _mensagem;
        public readonly ChatRepository _chat;


        public MensagemService(MensagemRepository mensagem, ChatRepository chat) {
            this._mensagem = mensagem;
            this._chat = chat;
        }

        public Mensagem Create(Mensagem mensagem) {
            if (mensagem.Chat == null) {
                if (mensagem.Remetente != null) {
                    var chat = new Chat() { Motorista = mensagem.Remetente };
                    _chat.Insert(chat);
                    mensagem.Chat = chat.Id;
                }
            } else {
                Chat chat = _chat.Find(mensagem.Chat);
            }
            _mensagem.Insert(mensagem);
            return mensagem;
        }
        public List<Mensagem> Get() {
            return _mensagem.Find();
        }
        public Mensagem Get(string Id) {
            return _mensagem.Find(Id);
        }
        public List<Mensagem> GetPadrao() {
            return _mensagem.FindPadrao();
        }
        public List<Mensagem> GetByChat(string chat) {
            return _mensagem.FindByChat(chat);
        }
        public Mensagem Update(string Id, Mensagem mensagem) {
            _mensagem.Replace(Id, mensagem);
            return mensagem;
        }
        public Mensagem Delete(string Id) {
            var mensagem = _mensagem.Find(Id);
            _mensagem.Delete(Id);
            return mensagem;
        }
    }
}
