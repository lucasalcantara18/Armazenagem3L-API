using Armazenagem3L_API.Data;
using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Services;
using Armazenagem3L_API.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Armazenagem3L_API.Controllers {
    [Route("api/produto")]
    [ApiController]
    public class ProdutosController : ControllerBase {
        
        private readonly ProdutosService _service;
        private readonly ILoggerManager _logger;

        public ProdutosController(ProdutosService service, ILoggerManager logger) {
            _service = service;
            _logger = logger;
        }

        // GET api/produto/listagem?id=5
        [HttpGet("listagem")]
        public IActionResult Get(int id = 0) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): GET Produto id =>" + JsonSerializer.Serialize(id));

            CustomResponse response;

            if (id == 0) {
                response = _service.listagemProdutos();
            } else {
                response = _service.produtosById(id);
            }
 
            return StatusCode((int)response.StatusCode, response.Retorno);
        }

        // POST api/<ProdutosController>
        [HttpPost]
        public IActionResult Post([FromBody] Produto produto) {
          _logger.LogDebug("[INFO] Recebendo requisicao (Controller): POST Produto =>" + JsonSerializer.Serialize(produto));
          CustomResponse response = _service.Add(produto);
          return StatusCode((int)response.StatusCode, response.Retorno);
        }

        // DELETE api/<ProdutosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): DELETE Produto id =>" + JsonSerializer.Serialize(id));
            CustomResponse response = _service.DeletarProduto(id);
            return StatusCode((int)response.StatusCode, response.Retorno);
        }
    }
}
