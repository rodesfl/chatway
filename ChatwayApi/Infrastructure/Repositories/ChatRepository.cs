using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class ChatRepository {

        private readonly IMongoCollection<Chat> _chat;

        public ChatRepository(Connection connection) {
            _chat = connection.GetCollection<Chat>("chat");
        }

        public Chat Insert(Chat chat) {
            _chat.InsertOne(chat);
            return chat;
        }

        public List<Chat> Find() => _chat.Find(c => true).ToList();

        public Chat Find(string id) => _chat.Find<Chat>(c => c.Id == id).FirstOrDefault();

        public Chat FindPendente() {
            FilterDefinition<Chat> filtro = Builders<Chat>.Filter.Where(e => e.Atendente == null);
            return _chat.Find<Chat>(filtro).FirstOrDefault();
        }

        public Chat FindAberto(string id) {
            FilterDefinition<Chat> filtro = Builders<Chat>.Filter.Where(e => e.Motorista == id && e.Concluido == false);
            return _chat.Find<Chat>(filtro).FirstOrDefault();
        }

        public void Replace(string id, Chat chat) => _chat.ReplaceOne(c => c.Id == id, chat);

        public void Delete(string id) => _chat.DeleteOne(c => c.Id == id);
    }
}
