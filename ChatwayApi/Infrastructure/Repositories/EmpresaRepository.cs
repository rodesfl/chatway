using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class EmpresaRepository {
        private readonly IMongoCollection<Empresa> _empresa;

        public EmpresaRepository(Connection connection) {
            _empresa = connection.GetCollection<Empresa>("empresa");
        }

        public Empresa Insert(Empresa empresa) {
            _empresa.InsertOne(empresa);
            return empresa;
        }

        public List<Empresa> Find() => _empresa.Find(e => true).ToList();

        public Empresa Find(string id) => _empresa.Find<Empresa>(e => e.Id == id).FirstOrDefault();

        public void Replace(string id, Empresa empresa) => _empresa.ReplaceOne(e => e.Id == id, empresa);

        public void Delete(string id) => _empresa.DeleteOne(e => e.Id == id);
    }
}
