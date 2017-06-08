using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WOM3.Models
{
    public class EmailReg
    {
        [Key]
        [Required]
        [EmailAddress(ErrorMessage ="Neispravan mail")]
        public string Email { get; set; }
    }
}