using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXCHNG.ExchngWebApp.Models
{
    public class CurrencyExchangeModel
    {
        public string FromCurrency { get; set; }

        public string ToCurrency { get; set; }

        public decimal FromValue { get; set; }

        public decimal ToValue { get; set; }

        public decimal ConversionValue { get; set; }

        public string ErrorMessage { get; set; }
    }
}