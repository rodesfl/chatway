using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;

namespace API.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase {

        public readonly AuthService _authService;

        public AuthController(AuthService authService) {
            this._authService = authService;
        }

        [HttpGet]
        [Route("loginDispositivo/{identificador}")]
        public ActionResult<Usuario> LoginDispositivo(string identificador) {
            try {
                Usuario usr = _authService.LoginDispositivo(identificador);
                if (usr != null) {
                    return Ok(usr);
                }
                return Unauthorized();
            } catch (Exception e) {
                return BadRequest("Erro ao logar o dispositivo");
            }
        }
    }
}
