using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class HeroSpells
    {
        public int ID { get; set; }

        [ForeignKey("Hero")]
        public string Name { get; set; }

        public int SpellID { get; set; }

        public virtual Heroes Hero { get; set; }
        public virtual Spells Spell { get; set; }
    }
}