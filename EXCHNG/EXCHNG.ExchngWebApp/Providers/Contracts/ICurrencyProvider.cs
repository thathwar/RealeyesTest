using System.Collections.Generic;
using EXCHNG.ExchngWebApp.Models;

namespace EXCHNG.ExchngWebApp.Providers.Contracts
{
    public interface ICurrencyProvider
    {
        List<string> GetCurrencies();

        List<Rate> GetLatestRates(List<string> currencies);

        List<GraphData> GetRatesForCompletePeriod(string currency);
    }
}
