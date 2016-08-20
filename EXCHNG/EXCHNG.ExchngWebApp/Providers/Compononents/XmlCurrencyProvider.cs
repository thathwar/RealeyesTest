using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using EXCHNG.ExchngWebApp.Models;
using EXCHNG.ExchngWebApp.Providers.Contracts;
using EXCHNG.ExchngWebApp.Providers.Helpers;

namespace EXCHNG.ExchngWebApp.Providers.Compononents
{
    public class XmlCurrencyProvider : ICurrencyProvider
    {
        #region Storage

        private const string XmlUrl = "http://www.ecb.europa.eu/stats/eurofxref/eurofxref-hist-90d.xml";

        #endregion

        #region Public Methods
        /// <summary>
        /// Return available curencies from the data source
        /// </summary>
        /// <returns>list of string</returns>
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

                list.AddRange(from XElement descendantNode in cube.DescendantNodes() 
                              select descendantNode.Attribute("currency").Value);

                break;
            }

            return list;
        }

        /// <summary>
        /// Returns latest rates for the currencies provided
        /// </summary>
        /// <param name="currencies"></param>
        /// <returns>list of rates</returns>
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
                foreach (var currency in currencies)
                {
                    list.AddRange(from XElement descendantNode in cube.DescendantNodes()
                                  where descendantNode.Attribute("currency").Value==currency 
                                  select new Rate() { Currency = descendantNode.Attribute("currency").Value,
                                      Value = Convert.ToDecimal(descendantNode.Attribute("rate").Value) });
                }

                break;
            }

            return list;
        }

        /// <summary>
        /// Provides a list of rates favaliable in the data source for a given currency
        /// </summary>
        /// <param name="currency"></param>
        /// <returns>list of GraphData </returns>
        public List<GraphData> GetRatesForCompletePeriod(string currency)
        {
            var list = new List<GraphData>();

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
                while (cube != null)
                {
                    foreach (var xNode in cube.DescendantNodes())
                    {
                        var descendantNode = (XElement)xNode;
                        if (descendantNode.Attribute("currency").Value == currency)
                        {
                            list.Add(new GraphData()
                            {
                                X = DateTimeHelper.DateTimeToUnixTimestamp(Convert.ToDateTime(cube.Attribute("time").Value)),
                                Y = Convert.ToDecimal(descendantNode.Attribute("rate").Value)
                            });

                            break;
                        }
                    }

                    cube = (XElement)cube.NextNode;
                }

                break;
            }

            list = list.OrderBy(d => d.X).ToList();

            return list;
        }
        #endregion
    }
}