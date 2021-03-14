using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoRate.Domain.Models;

namespace CryptoRate.Web.Models
{
    public class CryptoRatesViewModel
    {
        public string SelectedCryptoCurrency { get; set; }
        public Dictionary<string,decimal> Prices { get; set; }
    }
}
