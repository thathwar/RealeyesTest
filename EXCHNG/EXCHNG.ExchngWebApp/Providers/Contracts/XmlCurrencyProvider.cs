using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EXCHNG.ExchngWebApp.Models;
using EXCHNG.ExchngWebApp.Providers.Compononents;

namespace EXCHNG.ExchngWebApp.Providers.Contracts
{
    public class XmlCurrencyProvider : ICurrencyProvider
    {
        #region Storage

        private const string XmlUrl = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";

        #endregion

        #region Public Methods

        public List<string> GetCurrencies()
        {
            var list = new List<string>();

            var xelement = XElement.Load(XmlUrl);
            IEnumerable<XElement> elements = xelement.Elements();
            int count = 0;
            foreach (var element in elements)
            {
                if (count <= 1)
                {
                    count++; continue;
                }

                var cube = (XElement)element.FirstNode;

                list.AddRange(from XElement descendantNode in cube.DescendantNodes() select descendantNode.Attribute("currency").Value);

                break;
            }

            return list;
        }

        public List<Rate> GetLatestRates(List<string> currencies)
        {
            var list = new List<Rate>();

            var xelement = XElement.Load(XmlUrl);
            IEnumerable<XElement> elements = xelement.Elements();
            int count = 0;
            foreach (var element in elements)
            {
                if (count <= 1)
                {
                    count++; continue;
                }

                var cube = (XElement)element.FirstNode;

                list.AddRange(from XElement descendantNode in cube.DescendantNodes() where currencies.Contains(descendantNode.Attribute("currency").Value) select new Rate() { Currency = descendantNode.Attribute("currency").Value, Value = Convert.ToDecimal(descendantNode.Attribute("rate").Value) });

                break;
            }

            return list;
        }
        #endregion
    }
}