
namespace UnityCareHospital.Models
{
    using System.ComponentModel.DataAnnotations;

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Doctor
    {
        [Key] 
        public int DoctorID { get; set; }

       [Required(ErrorMessage = "Kullan�c� ID'si zorunludur.")]
        
       
        public int UserID { get; set; }

        [Required(ErrorMessage = "Uzmanl�k alan� zorunludur.")]
        public string Specialization { get; set; }

        
        public virtual User User { get; set; }
    }
}
