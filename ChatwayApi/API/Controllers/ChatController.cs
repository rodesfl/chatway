using Domain.Models;
using Hub.Bridges;
using Microsoft.AspNetCore.Mvc;
using Services.Services;
using System;
using System.Collections.Generic;

namespace API.Controllers {

    [Route("api/v1/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase {

        public readonly ChatService _chatService;
        public readonly ChatBridge _chatBridge;

        public ChatController(ChatService chatService, ChatBridge chatBridge) {
            _chatService = chatService;
            this._chatBridge = chatBridge;
        }

        [HttpGet]
        public ActionResult<List<Chat>> Get() {
            try {
                return Ok(_chatService.Get());
            } catch (Exception e) {
                return BadRequest("Erro ao buscar Chats");
            }
        }

        [HttpGet("{id}")]
        public ActionResult<Chat> Get(string id) {
            try {
                var chat = _chatService.Get(id);
                if (chat != null) {
                    return Ok(chat);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao buscar chat.");
            }
        }

        //EXCLUIR
        [HttpGet]
        [Route("atenderPendente/{id}")]
        public ActionResult<Chat> AtenderPendente(string id) {
            try {
                return Ok(_chatBridge.AtenderPendente(id));
            } catch (Exception e) {
                return BadRequest("Erro ao atender chat pendente");
            }
        }

        [HttpGet]
        [Route("finalizar/{id}")]
        public ActionResult<Chat> Finalizar(string id) {
            try {
                return Ok(_chatBridge.FinalizarChat(id));
            } catch (Exception e) {
                return BadRequest("Erro ao finalizar chat");
            }
        }


        //

        [HttpGet]
        [Route("aberto/{id}")]
        public ActionResult<Chat> Aberto(string id) {
            try {
                return Ok(_chatService.GetAberto(id));
            } catch (Exception e) {
                return BadRequest("Erro ao buscar chat aberto");
            }
        }

        [HttpPost]
        public ActionResult<Chat> Post([FromBody] Chat chat) {
            try {
                _chatService.Create(chat);
                return Ok(chat);
            } catch (Exception e) {
                return BadRequest("Erro ao salvar chat");
            }
        }

        [HttpPut("{id}")]
        public ActionResult<Chat> Put(string id, [FromBody] Chat newChat) {
            try {
                var oldChat = _chatService.Get(id);
                if (oldChat != null) {
                    _chatService.Update(id, newChat);
                    return Ok(newChat);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao alterar chat");
            }
        }

        [HttpDelete("{id}")]
        public ActionResult<Chat> Delete(string id) {
            try {
                var chat = _chatService.Get(id);
                if (chat != null) {
                    _chatService.Delete(id);
                    return Ok(chat);
                }
                return NoContent();
            } catch (Exception e) {
                return BadRequest("Erro ao remover chat");
            }
        }
    }
}
