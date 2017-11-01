using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class UserHeroes
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        [ForeignKey("Hero")]
        public string Name { get; set; }

        [DefaultValue(0)]
        public int Wins { get; set; }

        [DefaultValue(0)]
        public int Loses { get; set; }


        public int Total { get { return Wins + Loses; } }

        public virtual Heroes Hero { get; set; }
        public virtual User User { get; set; }
    }
}