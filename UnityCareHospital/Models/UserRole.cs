using System.ComponentModel.DataAnnotations;

namespace UnityCareHospital.Models
{
    using System;
    using System.Collections.Generic;

    public class UserRole
    {
        [Key] 
        public int UserRoleID { get; set; }

        public string Descr { get; set; }
    }
}
