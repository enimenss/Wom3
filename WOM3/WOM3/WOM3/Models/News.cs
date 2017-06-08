using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class News
    {
        public int ID { get; set; }

        [Required]
        public string Naslov { get; set; }

        [Required]
        public string Info { get; set; }

        
        public DateTime Datum { get; set; }
    }
}