using Domain.Models;
using Infrastructure.Repositories;

namespace Services.Services {
    public class DispositivoService {

        public readonly DispositivoRepository _dispositivo;

        public DispositivoService(DispositivoRepository dispositivo) {
            this._dispositivo = dispositivo;
        }

        public Dispositivo Create(Dispositivo dispositivo) {
            _dispositivo.Insert(dispositivo);
            return dispositivo;
        }

        public Dispositivo GetByCodigoAtivacao(string codigo) {
            return _dispositivo.FindByCodigoAtivacao(codigo);
        }

    }
}
