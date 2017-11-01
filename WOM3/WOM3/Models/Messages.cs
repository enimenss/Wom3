using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class Messages
    {
        public int id { get; set; }
        
        [ForeignKey("User")]
        [Required]
        public string UsernameId { get; set; }

        [Required]
        public string ReceiverId { get; set; }

        [Required]
        public string Message { get; set; }

        public int isRead { get; set; }


        public DateTime Datum { get; set; }
        public virtual User User { get; set; }
        public virtual User Receiver { get; set; }

       
    }
}