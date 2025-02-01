using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UnityCareHospital.Models
{
    public class DoctorViewModel
    {
        public int DoctorID { get; set; }
        public string FullName { get; set; }
        public string Specialization { get; set; }
    }
}