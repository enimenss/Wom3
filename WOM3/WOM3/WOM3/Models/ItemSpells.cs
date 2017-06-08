using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class ItemSpells
    {
        [Key]
        [ForeignKey("Item")]
        public int ItemID { get; set; }

        public float Demage { get; set; }

        public float Area { get; set; }

        public virtual Items Item { get; set; }


    }
}