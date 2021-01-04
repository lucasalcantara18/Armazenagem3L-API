using Armazenagem3L_API.Logger;
using Armazenagem3L_API.Models;
using Armazenagem3L_API.Services;
using Armazenagem3L_API.Util;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Armazenagem3L_API.Controllers {
    [Route("api/motorista")]
    [ApiController]
    public class MotoristaController : ControllerBase {

        private readonly ILoggerManager _logger;
        private readonly MotoristaService _service;

        public MotoristaController(ILoggerManager logger, MotoristaService service) {
            _logger = logger;
            _service = service;
        }

        // POST api/<ValuesController>
        [HttpPost]
        public IActionResult Post([FromBody] Motorista value) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): POST Motorista =>" + JsonSerializer.Serialize(value));
            CustomResponse response = _service.Add(value);

            return StatusCode((int)response.StatusCode, response.Retorno);
        }

        // GET api/motorista/id
        [HttpGet("{id}")]
        public IActionResult Get(int id) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): Get =>" + JsonSerializer.Serialize(id));
            CustomResponse result = _service.FindById(id);
            return StatusCode((int)result.StatusCode, result.Retorno);
        }

        // GET api/motorista/login
        [HttpPost("login")]
        public IActionResult Login([FromBody] DadosMotorista value) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): Login =>" + JsonSerializer.Serialize(value));
            CustomResponse result = _service.login(value);
            return StatusCode((int)result.StatusCode, result.Retorno);
        }

        // POST api/<ValuesController>
        [HttpPut("recuperar")]
        public IActionResult Put([FromBody] DadosMotorista value) {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): Put Motorista =>" + JsonSerializer.Serialize(value));
            CustomResponse response = _service.RecuperarSenha(value);

            return StatusCode((int)response.StatusCode, response.Retorno);
        }

        // POST api/motorista/verificar
        [HttpPost("verificar")]
        public IActionResult Verificar([FromBody] DadosMotorista value)
        {
            _logger.LogDebug("[INFO] Recebendo requisicao (Controller): Put Motorista =>" + JsonSerializer.Serialize(value));
            CustomResponse response = _service.VerificarEmail(value);

            return StatusCode((int)response.StatusCode, response.Retorno);
        }

    }
}
