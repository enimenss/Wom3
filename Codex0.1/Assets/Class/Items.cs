using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Class
{
    [System.Serializable]
    public class Items
    {
        public int ID;

        public int SpellID;

        public string Image;

        public int Health;

        public int Mana;
        public float HealthReg;
        public float ManaReg;
        public float Armor;
        public float Demage;
        public int Price;



        public ItemSpells Spell;
    }
}
