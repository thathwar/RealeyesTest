using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EXCHNG.ExchngWebApp.Models
{
    public class Cube
    {
        public DateTime Date { get; set; }

        public List<Rate> Rates { get; set; }   
    }

    public class Rate
    {
        public decimal Value { get; set; }

        public string Currency { get; set; }    
    }
}