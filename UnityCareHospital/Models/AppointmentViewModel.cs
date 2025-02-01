using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityCareHospital.Models
{
    public class AppointmentViewModel
    {
        public DateTime AppointmentDate { get; set; }
        public TimeSpan AppointmentTime { get; set; }
        public string PatientName { get; set; } 
        public string DoctorName { get; set; } 
        public string Specialization { get; set; } 
    }
}