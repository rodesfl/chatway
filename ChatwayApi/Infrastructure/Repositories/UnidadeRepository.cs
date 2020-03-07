using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class UnidadeRepository {

        private readonly IMongoCollection<Unidade> _unidade;

        public UnidadeRepository(Connection connection) {
            _unidade = connection.GetCollection<Unidade>("unidade");
        }

        public Unidade Insert(Unidade unidade) {
            _unidade.InsertOne(unidade);
            return unidade;
        }

        public List<Unidade> Find() => _unidade.Find(u => true).ToList();

        public Unidade Find(string id) => _unidade.Find<Unidade>(u => u.Id == id).FirstOrDefault();

        public void Replace(string id, Unidade unidade) => _unidade.ReplaceOne(u => u.Id == id, unidade);

        public void Delete(string id) => _unidade.DeleteOne(u => u.Id == id);
    }
}
