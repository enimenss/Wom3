using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class ItemZaPrikaz
    {

            public int ID { get; set; }

            public string Image { get; set; }

            public int Health { get; set; }

            public int Mana { get; set; }
            public float HealthReg { get; set; }
            public float ManaReg { get; set; }
            public float Armor { get; set; }
            public float Demage { get; set; }
            public int Price { get; set; }
        }
    
}