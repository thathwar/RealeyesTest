using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXCHNG.ExchngWebApp.Models
{
    public class CurrencyInfoModel
    {
        public string ErrorMessage { get; set; }

        public List<string> Currencies { get; set; }
    }
}