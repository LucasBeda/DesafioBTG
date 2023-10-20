using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using DesafioBTG.Models;

namespace DesafioBTG.Controllers
{
    public class HistoricoCotacaoController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly HttpClient _client;
        private readonly ILogger<CotacaoController> _log;

        public HistoricoCotacaoController(ILogger<CotacaoController> logger, IConfiguration configuration)
        {
            _log = logger;
            _configuration = configuration;
            _client = new HttpClient();
            _client.BaseAddress = new Uri(_configuration.GetValue<string>("EndPointAPI"));
        }

        public IActionResult Index()
        {
            List<Cotacao> cotacoes = new List<Cotacao>();
            HttpResponseMessage response = _client.GetAsync(_client.BaseAddress + "/ObterCotacao").Result;

            if (response.IsSuccessStatusCode)
            {
                string cotacaoAPI = response.Content.ReadAsStringAsync().Result;
                cotacoes = JsonConvert.DeserializeObject<List<Cotacao>>(cotacaoAPI);
            }

            return View(cotacoes);
        }
    }
}
