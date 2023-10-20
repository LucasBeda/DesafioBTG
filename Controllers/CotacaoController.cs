using Confluent.Kafka;
using DesafioBTG.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net;

namespace DesafioBTG.Controllers
{
    [Route("api/[controller]")]
    public class CotacaoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger<CotacaoController> _log;
        private readonly ApplicationInstance _application;
        public CotacaoController(ILogger<CotacaoController> logger, IConfiguration configuration, ApplicationInstance application)
        {
            _log = logger;
            _configuration = configuration;
            _application = application;
        }

        [HttpPost("EnviarCotacaoMensageriaKafka")]
        public void EnviarCotacaoMensageriaKafka([FromBody]Cotacao payload)
        {
            try
            {
                var config = new ProducerConfig
                {
                    BootstrapServers = _configuration.GetValue<string>("KafkaConfiguration:Server"),
                };

                var producer = new ProducerBuilder<Null, string>(config)
                               .SetErrorHandler((_, e) => _log.LogError($"Erro ConsumerBuilder: {e.Reason}"))
                               .Build();

                producer.Produce(_configuration.GetValue<string>("KafkaConfiguration:Topic"), new Message<Null, string> { Value = JsonConvert.SerializeObject(payload) });
            }
            catch (Exception e)
            {
                _log.LogError($"Erro: {e.Message}");
                throw;
            }
        }

        [HttpPost("SalvarCotacao")]
        public void SalvarCotacao([FromBody] Cotacao payload)
        {
            try
            {
                if (payload != null)
                {
                    if (_application.cotacao.GetCotacoes().Count == 0)
                        _application.cotacao.GravarCotacao(payload);
                    else
                        _application.cotacao.AtualizarCotacao(payload);
                }
            }
            catch (Exception e)
            {
                _log.LogError($"Erro: {e.Message}");
                throw;
            }
        }

        [HttpGet("ObterCotacao")]
        public List<Cotacao> ObterCotacao()
        {
            if (_application.cotacao.GetCotacoes().Count == 0)
                return new List<Cotacao>();

            return _application.cotacao.GetCotacoes();
        }
    }
}
