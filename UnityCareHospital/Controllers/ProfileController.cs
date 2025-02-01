using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using UnityCareHospital.Models;

namespace UnityCareHospital.Controllers
{
    public class ProfileController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public ActionResult Profile()
        {
            if (Session["UserID"] == null)
            {
                return RedirectToAction("Login", "Account");
            }

            int userId = Convert.ToInt32(Session["UserID"]);
            string userRole = Session["UserRole"].ToString();
            var user = db.Users.FirstOrDefault(u => u.UserID == userId);

            if (user == null)
            {
                TempData["ErrorMessage"] = "Kullanıcı bilgileri bulunamadı.";
                return RedirectToAction("Login", "Account");
            }
            // ImgID'yi ViewBag ile gönder
            ViewBag.ImgID = user.ImgID;

            List<AppointmentViewModel> appointments = new List<AppointmentViewModel>();

            if (userRole == "Doctor")
            {
                appointments = (from a in db.Appointments
                                join p in db.Patients on a.PatientID equals p.PatientID
                                join u in db.Users on p.UserID equals u.UserID
                                where a.DoctorID == db.Doctors
                                    .Where(d => d.UserID == userId)
                                    .Select(d => d.DoctorID)
                                    .FirstOrDefault()
                                select new AppointmentViewModel
                                {
                                    AppointmentDate = a.AppointmentDate,
                                    AppointmentTime = a.AppointmentTime,
                                    PatientName = u.FirstName + " " + u.LastName
                                }).ToList();
            }
            else if (userRole == "Patient")
            {
                appointments = (from a in db.Appointments
                                join d in db.Doctors on a.DoctorID equals d.DoctorID
                                join u in db.Users on d.UserID equals u.UserID
                                where a.PatientID == db.Patients
                                    .Where(p => p.UserID == userId)
                                    .Select(p => p.PatientID)
                                    .FirstOrDefault()
                                select new AppointmentViewModel
                                {
                                    AppointmentDate = a.AppointmentDate,
                                    AppointmentTime = a.AppointmentTime,
                                    DoctorName = "Dr. " + u.FirstName + " " + u.LastName,
                                    Specialization = d.Specialization
                                }).ToList();
            }

            ViewBag.Appointments = appointments;
            ViewBag.User = user;
            return View();
        }


        public ActionResult Calendar()
        {
            // Eğer `db` null ise, Database Context'inizi kontrol edin
            var appointmentIds = db.Appointments
                                   .Select(a => a.AppointmentID)
                                   .ToList();

            if (appointmentIds == null || !appointmentIds.Any())
            {
                // Eğer veritabanında kayıt yoksa, boş bir liste döndürün
                appointmentIds = new List<int>();
            }

            return View("Profile",appointmentIds);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UploadImage(System.Web.HttpPostedFileBase profileImage)
        {
            if (profileImage != null && profileImage.ContentLength > 0)
            {
                try
                {
                    // Kullanıcı kimliğini al
                    if (Session["UserID"] == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }
                    int userId = Convert.ToInt32(Session["UserID"]);

                    // Dosyanın uzantısını kontrol edin
                    string fileExtension = System.IO.Path.GetExtension(profileImage.FileName);
                    if (fileExtension != ".jpg" && fileExtension != ".png" && fileExtension != ".jpeg")
                    {
                        TempData["ErrorMessage"] = "Sadece JPG, PNG veya JPEG formatındaki dosyalar kabul edilmektedir.";
                        return RedirectToAction("Profile");
                    }

                    // Dosyayı kaydetmek için dosya adı oluştur
                    string fileName = $"User_{userId}{fileExtension}";
                    string filePath = Server.MapPath("~/Content/Images/" + fileName);

                    // Dosyayı kaydet
                    profileImage.SaveAs(filePath);

                    // Kullanıcının ImgID'sini güncelle
                    var user = db.Users.FirstOrDefault(u => u.UserID == userId);
                    if (user != null)
                    {
                        user.ImgID = fileName; // ImgID sütununu güncelle
                        db.SaveChanges();
                        TempData["SuccessMessage"] = "Profil resmi başarıyla yüklendi.";
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Dosya yüklenirken bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Lütfen bir dosya seçin.";
            }

            return RedirectToAction("Profile");
        }

    }

}

