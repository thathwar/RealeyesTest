using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace EXCHNG.ExchngWebApp.Models
{
    public class GraphModel
    {
        public string Currency { get; set; }

        public List<GraphData> GraphDatas { get; set; }

        public string ErrorMessage { get; set; }
    }
}