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
        [EmailAddress(ErrorMessage = "Ge�erli bir e-posta adresi giriniz.")] 
        public string Email { get; set; }

        [Required(ErrorMessage = "�ifre zorunludur.")] 
        [StringLength(50, MinimumLength = 6, ErrorMessage = "�ifre en az 6 karakter olmal�d�r.")] 
        public string Password { get; set; }

        [Required(ErrorMessage = "Ad zorunludur.")] 
        [StringLength(50, ErrorMessage = "Ad en fazla 50 karakter olmal�d�r.")] 
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad zorunludur.")] 
        [StringLength(50, ErrorMessage = "Soyad en fazla 50 karakter olmal�d�r.")] 
        public string LastName { get; set; }

        [Range(0, 120, ErrorMessage = "Ya� 0 ile 120 aras�nda olmal�d�r.")] 
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
