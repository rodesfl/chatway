using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers {
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase {

        public readonly UsuarioService _usuarioService;

        public UsuarioController(UsuarioService usuarioService) {
            _usuarioService = usuarioService;
        }

        [HttpGet]
        public ActionResult<List<Usuario>> Get() {
            try {
                return Ok(_usuarioService.Get());
            } catch (Exception e) {
                return BadRequest("Erro ao buscar Usuarios");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Usuario> Get(string id) {
            try {
                var usuario = _usuarioService.Get(id);
                if (usuario != null) {
                    return Ok(usuario);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao buscar usuario.");
            }
        }

        [HttpPost]
        public ActionResult<Usuario> Post([FromBody] Usuario usuario) {
            try {
                _usuarioService.Create(usuario);
                return Ok(usuario);
            } catch (Exception e) {
                return BadRequest("Erro ao salvar usuario");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Usuario> Put(string id, [FromBody] Usuario newUsuario) {
            try {
                var oldUsuario = _usuarioService.Get(id);
                if (oldUsuario != null) {
                    _usuarioService.Update(id, newUsuario);
                    return Ok(newUsuario);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao alterar usuario");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Usuario> Delete(string id) {
            try {
                var usuario = _usuarioService.Get(id);
                if (usuario != null) {
                    _usuarioService.Delete(id);
                    return Ok(usuario);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao remover usuario");
            }
        }
    }
}
