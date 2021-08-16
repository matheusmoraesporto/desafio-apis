using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;

namespace API2.Controllers
{
    /// <summary>
    /// Controller da api de cálculo de juros.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class API2Controller : Controller
    {
        private readonly HttpClient _client;
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Construtor para inicializar a classe.
        /// </summary>
        public API2Controller(IConfiguration configuration)
        {
            _configuration = configuration;
            _client = new HttpClient();
        }

        /// <summary>
        /// Método para realizar o cáluclo dos juros do valor informado junto à quantidade de meses.
        /// </summary>
        /// <param name="valorInicial">Valor inicial para base do cálculo de juros.</param>
        /// <param name="meses">Quantidade de meses para calcular o valor dos juros.</param>
        /// <returns>Valor com os juros calculados.</returns>
        [HttpGet]
        [Route("calculajuros")]
        public JsonResult CalculoJuros(decimal valorInicial, int meses)
        {
            var taxaJuros = GetTaxaJuros().Result;

            var calculoJurosXTempo = Math.Pow((double)(1 + taxaJuros), meses);

            var valorFinalComPrecisao = ((double)valorInicial) * calculoJurosXTempo;

            var valorFinal = valorFinalComPrecisao.ToString("N2");

            return new JsonResult(valorFinal);
        }

        /// <summary>
        /// Método para exibir o repositório onde encontra-se o projeto.
        /// </summary>
        /// <returns>URL do repositório do projeto.</returns>
        [HttpGet]
        [Route("showmethecode")]
        public JsonResult ShowMeTheCode()
        {
            var repositorioGithub = "https://github.com/matheusmoraesporto/desafio-apis";

            return new JsonResult(repositorioGithub);
        }

        /// <summary>
        /// Método que consome a API1, para obter o valor da taxa de juros.
        /// </summary>
        /// <returns>Valor da taxa de juros.</returns>
        private async Task<decimal> GetTaxaJuros()
        {
            decimal taxa = 0;

            var urlBase = _configuration.GetValue<string>("UrlBaseApi1");

            HttpResponseMessage response = await _client.GetAsync($"{urlBase}/taxaJuros");

            if (response.IsSuccessStatusCode)
            {
                var taxaString = await response.Content.ReadAsStringAsync();
                taxa = decimal.Parse(taxaString, new CultureInfo("en"));
            }
            else
            {
                throw new ApplicationException("Não foi possível obter o valor base para o cálculo da taxa de juros.");
            }

            return taxa;
        }
    }
}
