using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class Heroes
    {
        [Key]
        public string Name { get; set; }

        [Required]
        public string Image { get; set; }

        public int Health { get; set; }

        public int Mana { get; set; }
        public float HealthReg { get; set; }
        public float ManaReg { get; set; }
        public float Armor { get; set; }
        public float Demage { get; set; }
      
        public virtual ICollection<HeroSpells> HeroSpells { get; set; }

        public virtual ICollection<UserHeroes> UserHeroes { get; set; }
    }
}