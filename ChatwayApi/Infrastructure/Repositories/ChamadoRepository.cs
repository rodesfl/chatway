using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class ChamadoRepository {
        private readonly IMongoCollection<Chamado> _chamado;

        public ChamadoRepository(Connection connection) {
            _chamado = connection.GetCollection<Chamado>("chamado");
        }

        public Chamado Insert(Chamado chamado) {
            _chamado.InsertOne(chamado);
            return chamado;
        }

        public List<Chamado> Find() => _chamado.Find(c => true).ToList();

        public Chamado Find(string id) => _chamado.Find<Chamado>(c => c.Id == id).FirstOrDefault();

        public void Replace(string id, Chamado chamado) => _chamado.ReplaceOne(c => c.Id == id, chamado);

        public void Delete(string id) => _chamado.DeleteOne(c => c.Id == id);
    }
}
