using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class Spells
    {
        public int ID { get; set; }

        public float Demage { get; set; }

        public float Area { get; set; }

        public virtual ICollection<HeroSpells> HeroSpells { get; set; }
    }
}