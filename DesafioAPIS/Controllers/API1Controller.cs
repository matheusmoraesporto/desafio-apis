using Microsoft.AspNetCore.Mvc;

namespace API1.Controllers
{
    /// <summary>
    /// Controller para tratar da taxa dos juros.
    /// </summary>
    [Route("[controller]")]
    [ApiController]
    public class API1Controller : Controller
    {
        private const decimal _taxaJuro1PorCento = 0.01M;

        /// <summary>
        /// Método para obter o valor da taxa de juros.
        /// </summary>
        /// <returns>Valor da taxa de juros.</returns>
        [HttpGet]
        [Route("taxaJuros")]
        public JsonResult TaxaJuros()
        {
            return new JsonResult(_taxaJuro1PorCento);
        }
    }
}
