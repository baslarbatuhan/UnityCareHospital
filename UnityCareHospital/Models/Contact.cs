using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;


namespace UnityCareHospital.Models
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; } 

        [Required, StringLength(100)]
        public string FullName { get; set; }

        [Required, StringLength(15)]
        public string Number { get; set; }

        [Required, EmailAddress, StringLength(100)]
        public string Email { get; set; }

        [Required]
        public string Message { get; set; }

    }
}