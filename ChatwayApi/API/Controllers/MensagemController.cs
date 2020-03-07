using Domain.Models;
using Hub.Bridges;
using Utils.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace API.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class MensagemController : ControllerBase {

        public readonly MensagemService _mensagemService;
        public readonly ChatBridge _chatBridge;

        public MensagemController(MensagemService mensagemService, ChatBridge chatBridge) {
            this._mensagemService = mensagemService;
            this._chatBridge = chatBridge;
        }

        [HttpGet]
        public ActionResult<List<Mensagem>> Get() {
            try {
                return Ok(_mensagemService.Get());
            } catch (Exception e) {
                return BadRequest("Erro ao buscar Mensagems");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Mensagem> Get(string id) {
            try {
                var mensagem = _mensagemService.Get(id);
                if (mensagem != null) {
                    return Ok(mensagem);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao buscar mensagem.");
            }
        }

        [HttpGet]
        [Route("padrao")]
        public ActionResult<Mensagem> GetPadrao() {
            try {
                return Ok(_mensagemService.GetPadrao());
            } catch (Exception e) {
                return BadRequest("Erro ao buscar mensagens padronizadas.");
            }
        }

        [HttpGet]
        [Route("getbychat/{chat}")]
        public ActionResult<List<Mensagem>> GetByChat(string chat) {
            try {
                return Ok(_mensagemService.GetByChat(chat));
            } catch (Exception e) {
                return BadRequest("Erro ao buscar mensagens do chat");
            }
        }

        [HttpPost]
        public ActionResult<Mensagem> Post([FromBody] Mensagem mensagem) {
            try {
                _mensagemService.Create(mensagem);
                //EXCLUIR
                if (mensagem.Chat != null) {
                    _chatBridge.NotificarDestinatario(mensagem);
                }
                //
                return Ok(mensagem);
            } catch (Exception e) {
                return BadRequest("Erro ao salvar mensagem");
            }
        }

        [HttpPost("upload/{chat}/{remetente}")]
        public async Task<IActionResult> UploadFile(IFormFile file, string chat, string remetente) {

            try {
                if (file.Length > 0) {
                    Mensagem mensagem = new Mensagem { Chat = chat, Remetente = remetente, Tipo = 3 };
                    _mensagemService.Create(mensagem);

                    var path = "D:\\Rodrigo\\media\\" + mensagem.Id + Path.GetExtension(file.FileName);

                    using (var stream = System.IO.File.Create(path)) {
                        await file.CopyToAsync(stream);
                    }
                    mensagem.Path = path;
                    _mensagemService.Update(mensagem.Id, mensagem);

                    return Ok(mensagem);
                }
                return BadRequest("Arquivo inválido");
            } catch (Exception e) {
                return BadRequest("Erro no upload do arquivo");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Mensagem> Put(string id, [FromBody] Mensagem newMensagem) {
            try {
                var oldMensagem = _mensagemService.Get(id);
                if (oldMensagem != null) {
                    _mensagemService.Update(id, newMensagem);
                    return Ok(newMensagem);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao alterar mensagem");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Mensagem> Delete(string id) {
            try {
                var mensagem = _mensagemService.Get(id);
                if (mensagem != null) {
                    _mensagemService.Delete(id);
                    return Ok(mensagem);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao remover mensagem");
            }
        }
    }
}
