using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;
using EXCHNG.ExchngWebApp.Models;
using EXCHNG.ExchngWebApp.Providers.Compononents;
using NLog;

namespace EXCHNG.ExchngWebApp.Controllers
{
    public class HomeController : Controller
    {
        readonly Logger _logger = LogManager.GetLogger("HomeController");
        public ActionResult Index()
        {
            var model = new CurrencyInfoModel(){Currencies = new List<string>()};
            try
            {
                model.Currencies = new XmlCurrencyProvider().GetCurrencies();
            }
            catch (WebException exception)
            {
                model.ErrorMessage = "Please check your network connection. Could dont bring up the currencies. Try again later.";
                _logger.Log(LogLevel.Error, exception);
            }
            catch (Exception exception)
            {
                model.ErrorMessage = "Something went wrong. Could dont bring up the currencies. Please try again later.";
                _logger.Log(LogLevel.Error, exception);
            }

            return View(model);
        }
    }
}
