using Domain.Models;
using Infrastructure.Settings;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Repositories {
    public class UsuarioRepository {
        private readonly IMongoCollection<Usuario> _usuario;

        public UsuarioRepository(Connection connection) {
            _usuario = connection.GetCollection<Usuario>("usuario");
        }

        public Usuario Insert(Usuario usuario) {
            _usuario.InsertOne(usuario);
            return usuario;
        }

        public List<Usuario> Find() => _usuario.Find(u => true).ToList();

        public Usuario Find(string id) => _usuario.Find<Usuario>(u => u.Id == id).FirstOrDefault();

        public void Replace(string id, Usuario usuario) => _usuario.ReplaceOne(u => u.Id == id, usuario);

        public void Delete(string id) => _usuario.DeleteOne(u => u.Id == id);
    }
}
