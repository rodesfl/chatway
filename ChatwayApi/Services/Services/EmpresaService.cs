using Domain.Models;
using Infrastructure.Repositories;
using System.Collections.Generic;

namespace Services.Services {
    public class EmpresaService {
        public readonly EmpresaRepository _empresa;

        public EmpresaService(EmpresaRepository empresa) {
            this._empresa = empresa;
        }

        public Empresa Create(Empresa empresa) {
            _empresa.Insert(empresa);
            return empresa;
        }
        public List<Empresa> Get() {
            return _empresa.Find();
        }
        public Empresa Get(string Id) {
            return _empresa.Find(Id);
        }
        public Empresa Update(string Id, Empresa empresa) {
            _empresa.Replace(Id, empresa);
            return empresa;
        }
        public Empresa Delete(string Id) {
            var empresa = _empresa.Find(Id);
            _empresa.Delete(Id);
            return empresa;
        }
    }
}
