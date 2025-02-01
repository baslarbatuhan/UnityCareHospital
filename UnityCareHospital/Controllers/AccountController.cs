using System.Linq;
using System.Web.Mvc;
using UnityCareHospital.Models;
using System;

namespace UnityCareHospital.Controllers
{
    public class AccountController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

     

        // GET: Signup
        public ActionResult Signup()
        {
            return View();
        }

        // POST: Signup
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Signup(User model)
        {

            // Test
            if (Session["UserID"] != null)
            {
                System.Diagnostics.Debug.WriteLine("Session set successfully!");
            }
            if (ModelState.IsValid)
            {
                // Kullanıcının e-posta adresinin zaten kayıtlı olup olmadığını kontrol et
                var existingUser = db.Users.FirstOrDefault(u => u.Email == model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "Bu e-posta adresi zaten kayıtlı.");
                    return View(model);
                }

                // Kullanıcıyı kaydet
                var newUser = new User
                {
                    Email = model.Email,
                    Password = model.Password,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Age = model.Age,
                    Gender = model.Gender,
                    Phone = model.Phone,
                    UserRoleID = 2 // Varsayılan olarak hasta rolü (ör. UserRole tablosunda 2'nin hasta olduğunu varsayıyoruz)
                };


                db.Users.Add(newUser);
                db.SaveChanges();

                // Eklenen kullanıcının UserID'sini al
                var userId = newUser.UserID;

                // Patient tablosuna kayıt ekle
                var newPatient = new Patient
                {
                    UserID = userId // Eklenen kullanıcının UserID'sini kullan
                };

                db.Patients.Add(newPatient);
                db.SaveChanges();

                // Başarılı kayıt sonrası giriş sayfasına yönlendir
                return RedirectToAction("Login", "Account");
            }

            return View(model);
        }

        // GET: Login
        public ActionResult Login()     
        {
            return View();
        }

        // POST: Login
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(string email, string password)
        {
            var user = db.Users.Include("UserRole").FirstOrDefault(u => u.Email == email && u.Password == password);

            if (user != null)
            {
                // Oturum açma işlemi başarılı
                Session["UserID"] = user.UserID;
                Session["UserRole"] = user.UserRole.Descr;
                Session["FirstName"] = user.FirstName;
                Session["LastName"] = user.LastName;
                Session["UserRoleID"] = user.UserRoleID.ToString();

                if (Session["UserID"] != null)
                {
                    System.Diagnostics.Debug.WriteLine("Session set successfully!");
                    System.Diagnostics.Debug.WriteLine(Session["UserRoleID"]);
                }

                // Kullanıcı rolüne göre yönlendirme
                return RedirectToAction("Main", "Home");
            }

            // Hatalı giriş durumunda hata mesajı göster
            ViewBag.ErrorMessage = "Geçersiz e-posta veya şifre.";
            return View();
        }

        // POST: Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            Session["UserID"] = null;
            Session["UserRole"] = null;
            Session["UserName"] = null;

            return RedirectToAction("Main", "Home");
        }

        public ActionResult GetUser()
        {
            var userId = Session["UserID"];
            if (userId != null)
            {
                var user = db.Users.Find(userId);
                if (user != null)
                {
                    System.Diagnostics.Debug.WriteLine("USER ıd found");
                    ViewBag.UserName = user.FirstName + " " + user.LastName; // Kullanıcı adı View'a gönderilir
                }
            } else
            {
                System.Diagnostics.Debug.WriteLine("USER ID NOT FOUND");
            }

            return View();
        }


       

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookAppointment(int doctorId, DateTime appointmentDateTime)
        {
            if (Session["UserID"] == null)
            {
                TempData["AppointmentErrorMessage"] = "Randevu alabilmek için giriş yapmalısınız.";
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserID"]);

            var existingAppointment = db.Appointments
                .FirstOrDefault(a => a.DoctorID == doctorId &&
                                     a.AppointmentDate == appointmentDateTime.Date &&
                                     a.AppointmentTime == appointmentDateTime.TimeOfDay);

            if (existingAppointment != null)
            {
                TempData["AppointmentErrorMessage"] = "Bu tarih ve saat dolu. Lütfen başka bir zaman seçiniz.";
                return RedirectToAction("Calendar");
            }

            var patient = db.Patients.FirstOrDefault(p => p.UserID == userId);

            if (patient == null)
            {
                TempData["ErrorMessage"] = "Hasta bilgileri bulunamadı.";
                return RedirectToAction("Profile");
            }

            var newAppointment = new Appointment
            {
                DoctorID = doctorId,
                PatientID = patient.PatientID,
                AppointmentDate = appointmentDateTime.Date,
                AppointmentTime = appointmentDateTime.TimeOfDay
            };

            db.Appointments.Add(newAppointment);
            db.SaveChanges();

            TempData["SuccessMessage"] = "Randevunuz başarıyla oluşturuldu.";
            return RedirectToAction("Calendar");
        }
    }

}
