using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class DispositivoRepository {

        private readonly IMongoCollection<Dispositivo> _collection;

        public DispositivoRepository(Connection connection) {
            _collection = connection.GetCollection<Dispositivo>("dispositivo");
        }

        public Dispositivo Insert(Dispositivo dispositivo) {
            _collection.InsertOne(dispositivo);
            return dispositivo;
        }

        public Dispositivo FindByCodigoAtivacao(string codigo) {
            FilterDefinition<Dispositivo> filtro = Builders<Dispositivo>.Filter.Where(d => d.CodigoAtivacao == codigo);
            return _collection.Find(filtro).FirstOrDefault();
        }
    }
}
