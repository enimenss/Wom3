using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class PrimljenaPoruka
    {
       public string Sender { get; set; }
       public string Message { get; set; }
        public  string Jpg { get; set; }
        public string Datum { get; set; }
        public int IsSeen { get; set; }
        public int NumOfNotSeen { get; set; }
        public bool Online { get; set; }
    }
}