using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace UnityCareHospital.Models
{
    using System;
    using System.Collections.Generic;

    public class Patient
    {
        [Key] 
        public int PatientID { get; set; }

        [Required] 
        
        public int UserID { get; set; }

        
        public virtual User User { get; set; }
    }
}
