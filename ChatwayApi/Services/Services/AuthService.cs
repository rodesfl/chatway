using Domain.Models;
using Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Services {
    public class AuthService {

        public readonly AuthRepository _authRepository;

        public AuthService(AuthRepository authRepository) {
            this._authRepository = authRepository;
        }

        public Usuario LoginDispositivo(String identificador) {
            return _authRepository.LoginDispositivo(identificador);
        }
    }
}
