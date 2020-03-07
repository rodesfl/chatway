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
    public class ChamadoController : ControllerBase {

        public readonly ChamadoService _chamadoService;

        public ChamadoController(ChamadoService chamadoService) {
            _chamadoService = chamadoService;
        }

        [HttpGet]
        public ActionResult<List<Chamado>> Get() {
            try {
                return Ok(_chamadoService.Get());
            } catch (Exception e) {
                return BadRequest("Erro ao buscar Chamados");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Chamado> Get(string id) {
            try {
                var chamado = _chamadoService.Get(id);
                if (chamado != null) {
                    return Ok(chamado);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao buscar chamado.");
            }
        }

        [HttpPost]
        public ActionResult<Chamado> Post([FromBody] Chamado chamado) {
            try {
                _chamadoService.Create(chamado);
                return Ok(chamado);
            } catch (Exception e) {
                return BadRequest("Erro ao salvar chamado");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Chamado> Put(string id, [FromBody] Chamado newChamado) {
            try {
                var oldChamado = _chamadoService.Get(id);
                if (oldChamado != null) {
                    _chamadoService.Update(id, newChamado);
                    return Ok(newChamado);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao alterar chamado");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Chamado> Delete(string id) {
            try {
                var chamado = _chamadoService.Get(id);
                if (chamado != null) {
                    _chamadoService.Delete(id);
                    return Ok(chamado);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao remover chamado");
            }
        }
    }
}
