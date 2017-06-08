using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class Token
    {
        [Key]
        public string token { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        public virtual User User { get; set; }
    }
}