using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace UnityCareHospital.Models
{
    using System;
    using System.Collections.Generic;

    public class User
    {
        [Key] 
        public int UserID { get; set; }

        [Required(ErrorMessage = "E-posta adresi zorunludur.")] 
        [EmailAddress(ErrorMessage = "Geçerli bir e-posta adresi giriniz.")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "Þifre zorunludur.")] 
        [StringLength(50, MinimumLength = 6, ErrorMessage = "Þifre en az 6 karakter olmalýdýr.")] 
        public string Password { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")] 
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olmalýdýr.")] 
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")] 
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olmalýdýr.")] 
        public string LastName { get; set; }

        [Range(0, 120, ErrorMessage = "Yaþ 0 ile 120 arasýnda olmalýdýr.")] 
        public string Age { get; set; }

        
        public string Gender { get; set; }

        [Phone]
        public string Phone { get; set; }

        [Required] 
        [ForeignKey("UserRole")] 
        public int UserRoleID { get; set; }

        public string ImgID { get; set; }

        
        public virtual UserRole UserRole { get; set; }
    }
}
