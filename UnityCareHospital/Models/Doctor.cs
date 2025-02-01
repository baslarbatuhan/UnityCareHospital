
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

       [Required(ErrorMessage = "Kullanýcý ID'si zorunludur.")]
        
       
        public int UserID { get; set; }

        [Required(ErrorMessage = "Uzmanlýk alaný zorunludur.")]
        public string Specialization { get; set; }

        
        public virtual User User { get; set; }
    }
}
