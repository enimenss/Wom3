using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class FriendRequests
    {
        [Key]
        public int ID { get; set; }

        [ForeignKey("User")]
        [Required]
        public string UsernameId { get; set; }

        [Required]
        public string FriendRId { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Datum { get; set; }
        public virtual User User { get; set; }
        public virtual User FriendR { get; set; }
    }
}