using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class User
    {
        [Key]
        [Required (ErrorMessage ="Morate uneti Username")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Minimalno 4 karaktera")]
        public string Username { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "nevalidan mail")]
       
        public string Email { get; set; }

        [Required(ErrorMessage = "Morate uneti sifru")]
        [MinLength(6, ErrorMessage = "Sifra mora imati minimum 6 karaktera")]
        public string Pass { get; set; }

        [Required(ErrorMessage = "Morate izabrati Avatara")]
        public string Avatar { get; set; }

        [DefaultValue(0)]
        public int NumOfNews { get; set; }

        public virtual ICollection<UserItems> UserItems {get;set;}
        public virtual ICollection<UserHeroes> UserHeroes { get; set; }

        //public virtual ICollection<Messages> Messages { get; set; }
        //public virtual ICollection<FriendRequests> FriendRequests { get; set; }

        //public virtual ICollection<Friends> Friends { get; set; }

        public virtual UserStats UserStats { get; set; }


    }
}