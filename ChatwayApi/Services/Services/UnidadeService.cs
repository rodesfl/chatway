using Domain.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services {
    public class UnidadeService {
        public readonly UnidadeRepository _unidade;

        public UnidadeService(UnidadeRepository unidade) {
            this._unidade = unidade;
        }

        public Unidade Create(Unidade unidade) {
            _unidade.Insert(unidade);
            return unidade;
        }
        public List<Unidade> Get() {
            return _unidade.Find();
        }
        public Unidade Get(string Id) {
            return _unidade.Find(Id);
        }
        public Unidade Update(string Id, Unidade unidade) {
            _unidade.Replace(Id, unidade);
            return unidade;
        }
        public Unidade Delete(string Id) {
            var unidade = _unidade.Find(Id);
            _unidade.Delete(Id);
            return unidade;
        }
    }
}
