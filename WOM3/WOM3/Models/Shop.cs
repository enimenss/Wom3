using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class Shop
    {
        public ICollection<Items> Itemi { get; set; }
        public User User { get; set; }
    }
}