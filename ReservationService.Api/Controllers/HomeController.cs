using CryptoRate.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CryptoRate.Application.CryptoCurrencies;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;

namespace CryptoRate.Web.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IConfiguration _configuration;

        public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;
        }

        public async Task<ViewResult> Index()
        {
            var currencies = await Mediator.Send(new GetAllCryptoCurrenciesQuery()).ConfigureAwait(false);
            ViewBag.AllCurrencies=currencies.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Symbol,
            });
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(CryptoRatesViewModel currency)
        {
            var currencies = await Mediator.Send(new GetAllCryptoCurrenciesQuery()).ConfigureAwait(false);
            ViewBag.AllCurrencies=currencies.Select(t => new SelectListItem
            {
                Text = t.Name,
                Value = t.Symbol,
            });
            
            var prices = await Mediator.Send(new GetCryptoCurrencyExchangeRatesQuery {Symbol = currency.SelectedCryptoCurrency}).ConfigureAwait(false);
            
            CryptoRatesViewModel model = new CryptoRatesViewModel {Prices = prices};
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
