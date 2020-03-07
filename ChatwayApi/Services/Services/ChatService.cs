using Domain.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services {
    public class ChatService {
        public readonly ChatRepository _chat;

        public ChatService(ChatRepository chat) {
            this._chat = chat;
        }

        public Chat Create(Chat chat) {
            _chat.Insert(chat);
            return chat;
        }
        public List<Chat> Get() {
            return _chat.Find();
        }
        public Chat Get(string Id) {
            return _chat.Find(Id);
        }
        public Chat GetPendente() {
            return _chat.FindPendente();
        }
        public Chat GetAberto(string Id) {
            return _chat.FindAberto(Id);
        }
        public Chat Update(string Id, Chat chat) {
            _chat.Replace(Id, chat);
            return chat;
        }
        public Chat Delete(string Id) {
            var chat = _chat.Find(Id);
            _chat.Delete(Id);
            return chat;
        }
    }
}
