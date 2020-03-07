using Domain.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services {
    public class ChamadoService {
        public readonly ChamadoRepository _chamado;

        public ChamadoService(ChamadoRepository chamado) {
            this._chamado = chamado;
        }

        public Chamado Create(Chamado chamado) {
            _chamado.Insert(chamado);
            return chamado;
        }
        public List<Chamado> Get() {
            return _chamado.Find();
        }
        public Chamado Get(string Id) {
            return _chamado.Find(Id);
        }
        public Chamado Update(string Id, Chamado chamado) {
            _chamado.Replace(Id, chamado);
            return chamado;
        }
        public Chamado Delete(string Id) {
            var chamado = _chamado.Find(Id);
            _chamado.Delete(Id);
            return chamado;
        }
    }
}
