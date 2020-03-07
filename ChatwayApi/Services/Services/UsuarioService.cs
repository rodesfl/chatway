using Domain.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services {
    public class UsuarioService {
        public readonly UsuarioRepository _usuario;

        public UsuarioService(UsuarioRepository usuario) {
            this._usuario = usuario;
        }

        public Usuario Create(Usuario usuario) {
            _usuario.Insert(usuario);
            return usuario;
        }
        public List<Usuario> Get() {
            return _usuario.Find();
        }
        public Usuario Get(string Id) {
            return _usuario.Find(Id);
        }
        public Usuario Update(string Id, Usuario usuario) {
            _usuario.Replace(Id, usuario);
            return usuario;
        }
        public Usuario Delete(string Id) {
            var usuario = _usuario.Find(Id);
            _usuario.Delete(Id);
            return usuario;
        }
    }
}
