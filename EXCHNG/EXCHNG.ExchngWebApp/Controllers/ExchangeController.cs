using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using EXCHNG.ExchngWebApp.Models;
using EXCHNG.ExchngWebApp.Providers.Compononents;
using Microsoft.Ajax.Utilities;
using NLog;

namespace EXCHNG.ExchngWebApp.Controllers
{
    [RoutePrefix("api/Exchange")]
    public class ExchangeController : ApiController
    {
        #region Storage

        readonly Logger _logger = LogManager.GetLogger("ExchangeController");
        #endregion

        #region Methods
        [AcceptVerbs("Post")]
        [Route("Compute")]
        public CurrencyExchangeModel Compute(CurrencyExchangeModel model)
        {
            var result = new CurrencyExchangeModel();
            try
            {
                var exchangeResult = new XmlCurrencyProvider().GetLatestRates(new List<string>() { model.FromCurrency, model.ToCurrency });
                result.ToValue = (exchangeResult[1].Value / exchangeResult[0].Value) * model.FromValue;
                result.ConversionValue = (exchangeResult[1].Value/exchangeResult[0].Value);
            }
            catch (WebException exception)
            {
                model.ErrorMessage = "Please check your network connection. Could dont bring up the currencies. Try again later.";
                _logger.Log(LogLevel.Error, exception);
            }
            catch (Exception exception)
            {
                result.ErrorMessage = "Something went wrong, please try again later.";
                _logger.Log(LogLevel.Error, exception);
            }

            return result;
        }


        [AcceptVerbs("Post")]
        [Route("GetHistoricalGraphData")]
        public GraphModel GetHistoricalGraphData(GraphModel model)
        {
            try
            {
                model.GraphDatas = new XmlCurrencyProvider().GetRatesForCompletePeriod(model.Currency);
            }
            catch (WebException exception)
            {
                model.ErrorMessage = "Please check your network connection. Could dont bring up the currencies. Try again later.";
                _logger.Log(LogLevel.Error, exception);
            }
            catch (Exception exception)
            {
                model.ErrorMessage = "Something went wrong, please try again later.";
                _logger.Log(LogLevel.Error, exception);
            }

            return model;
        }
        #endregion
    }
}
