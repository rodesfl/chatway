using Domain.Models;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using Utils.Utils;

namespace API.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmpresaController : ControllerBase {

        public readonly EmpresaService _empresaService;

        public EmpresaController(EmpresaService empresaService) {
            _empresaService = empresaService;
        }

        [HttpGet]
        public ActionResult<List<Empresa>> Get() {
            Console.WriteLine(TokenGenerator.GerarMd5Aleatorio());
            try {
                return Ok(_empresaService.Get());
            } catch (Exception e) {
                return BadRequest("Erro ao buscar Empresas");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Empresa> Get(string id) {
            try {
                var empresa = _empresaService.Get(id);
                if (empresa != null) {
                    return Ok(empresa);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao buscar empresa.");
            }
        }

        [HttpPost]
        public ActionResult<Empresa> Post([FromBody] Empresa empresa) {
            try {
                _empresaService.Create(empresa);
                return Ok(empresa);
            } catch (Exception e) {
                return BadRequest("Erro ao salvar empresa");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Empresa> Put(string id, [FromBody] Empresa newEmpresa) {
            try {
                var oldEmpresa = _empresaService.Get(id);
                if (oldEmpresa != null) {
                    _empresaService.Update(id, newEmpresa);
                    return Ok(newEmpresa);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao alterar empresa");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Empresa> Delete(string id) {
            try {
                var empresa = _empresaService.Get(id);
                if (empresa != null) {
                    _empresaService.Delete(id);
                    return Ok(empresa);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao remover empresa");
            }
        }
    }
}
