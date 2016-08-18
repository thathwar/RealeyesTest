using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EXCHNG.ExchngWebApp.Models;

namespace EXCHNG.ExchngWebApp.Providers.Compononents
{
    public interface ICurrencyProvider
    {
        List<string> GetCurrencies();

        List<Rate> GetLatestRates(List<string> currencies);
    }
}
