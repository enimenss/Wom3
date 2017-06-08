using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class UserItems
    {
        public int ID { get; set; }

        [ForeignKey("User")]
        public string Username { get; set; }

        public int ItemID { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DateOfPurchase { get; set; }
        public virtual Items Item { get; set; }
        public virtual User User { get; set; }
    }
}