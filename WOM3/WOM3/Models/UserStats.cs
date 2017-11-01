using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.ModelBinding;

namespace WOM3.Models
{
    public class UserStats
    { 
        [Key]
        [ForeignKey("User")]
        [Required]
        public string Username { get; set; }

        [DefaultValue(0)]
        public int Wins { get; set; }

        [DefaultValue(0)]
        public int Loses { get; set; }

      
        public int Total { get { return Wins + Loses; } }

        [DefaultValue(0)]
        public int Points  { get; set; }

        [DefaultValue(0)]
        public int Gold { get; set; }

        public virtual User User { get; set; }
    }
}