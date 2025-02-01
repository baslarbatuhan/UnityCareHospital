using System;
using System.ComponentModel.DataAnnotations;

namespace UnityCareHospital.Models
{
    public class Appointment
    {
        [Key] 
        public int AppointmentID { get; set; }

        [Required(ErrorMessage = "Doktor ID'si zorunludur.")]
        public int DoctorID { get; set; }

        [Required(ErrorMessage = "Hasta ID'si zorunludur.")]
        public int PatientID { get; set; }

        [Required(ErrorMessage = "Randevu tarihi zorunludur.")]
        [DataType(DataType.Date)] // Tarih formatý
        public DateTime AppointmentDate { get; set; }

        [Required(ErrorMessage = "Randevu saati zorunludur.")]
        [DataType(DataType.Time)] // Saat formatý
        public TimeSpan AppointmentTime { get; set; }


       
        public virtual Doctor Doctor { get; set; }
        public virtual Patient Patient { get; set; }
    }
}
