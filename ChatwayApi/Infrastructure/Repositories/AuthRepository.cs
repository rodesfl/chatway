using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class AuthRepository {

        private readonly IMongoCollection<Usuario> _usuario;

        public AuthRepository(Connection connection) {
            _usuario = connection.GetCollection<Usuario>("usuario");
        }

        public Usuario LoginDispositivo(String identificador) {
            FilterDefinition<Usuario> filtro = Builders<Usuario>.Filter.Where(e => e.Dispositivo == identificador);
            return _usuario.Find<Usuario>(filtro).First();
        }
    }
}
