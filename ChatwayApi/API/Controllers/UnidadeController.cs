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
    public class UnidadeController : ControllerBase {

        public readonly UnidadeService _unidadeService;

        public UnidadeController(UnidadeService unidadeService) {
            _unidadeService = unidadeService;
        }

        [HttpGet]
        public ActionResult<List<Unidade>> Get() {
            try {
                return Ok(_unidadeService.Get());
            } catch (Exception e) {
                return BadRequest("Erro ao buscar Unidades");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Unidade> Get(string id) {
            try {
                var unidade = _unidadeService.Get(id);
                if (unidade != null) {
                    return Ok(unidade);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao buscar unidade.");
            }
        }

        [HttpPost]
        public ActionResult<Unidade> Post([FromBody] Unidade unidade) {
            try {
                _unidadeService.Create(unidade);
                return Ok(unidade);
            } catch (Exception e) {
                return BadRequest("Erro ao salvar unidade");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Unidade> Put(string id, [FromBody] Unidade newUnidade) {
            try {
                var oldUnidade = _unidadeService.Get(id);
                if (oldUnidade != null) {
                    _unidadeService.Update(id, newUnidade);
                    return Ok(newUnidade);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao alterar unidade");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Unidade> Delete(string id) {
            try {
                var unidade = _unidadeService.Get(id);
                if (unidade != null) {
                    _unidadeService.Delete(id);
                    return Ok(unidade);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao remover unidade");
            }
        }
    }
}
