using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class MensagemRepository {

        private readonly IMongoCollection<Mensagem> _mensagem;

        public MensagemRepository(Connection connection) {
            _mensagem = connection.GetCollection<Mensagem>("mensagem");
        }
        public Mensagem Insert(Mensagem mensagem) {
            _mensagem.InsertOne(mensagem);
            return mensagem;
        }
        public List<Mensagem> Find() => _mensagem.Find(m => true).ToList();
        public Mensagem Find(string id) => _mensagem.Find<Mensagem>(m => m.Id == id).FirstOrDefault();
        public List<Mensagem> FindPadrao() {
            FilterDefinition<Mensagem> filtro = Builders<Mensagem>.Filter.Where(e => e.Remetente == null);
            return _mensagem.Find(filtro).ToList();
        }
        public List<Mensagem> FindByChat(string chat) {
            FilterDefinition<Mensagem> filtro = Builders<Mensagem>.Filter.Where(e => e.Chat == chat);
            return _mensagem.Find(filtro).ToList();
        }
        public void Replace(string id, Mensagem mensagem) => _mensagem.ReplaceOne(m => m.Id == id, mensagem);
        public void Delete(string id) => _mensagem.DeleteOne(m => m.Id == id);
    }
}
