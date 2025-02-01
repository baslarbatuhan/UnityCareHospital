using System.Linq;
using System.Web.Mvc;
using UnityCareHospital.Models;
using System;

namespace UnityCareHospital.Controllers
{
    public class AppointmentController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Appointment
        public ActionResult Appointment()
        {



            // Dinamik olarak uzmanlık ve doktor bilgilerini ViewBag ile gönderiyoruz
            ViewBag.Specializations = db.Doctors
                .Select(d => d.Specialization)
                .Distinct()
                .ToList();

                var doctors = db.Doctors
                    .Select(d => new DoctorViewModel
                    {
                        DoctorID = d.DoctorID,
                        FullName = "Dr. " + d.User.FirstName + " " + d.User.LastName,
                        Specialization = d.Specialization
                    })
                    .ToList();

                return View(doctors);

            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public ActionResult ReservationAppointment(string specialty, int doctorId, DateTime dateTime)
            {
                if (Session["UserID"] == null)
                {
                    // Eğer kullanıcı giriş yapmadıysa, giriş sayfasına yönlendir
                    TempData["AppointmentErrorMessage"] = "Randevu alabilmek için giriş yapmalısınız.";
                    TempData["RedirectAfterLogin"] = Url.Action("Appointment", "Appointment");
                    return RedirectToAction("Login", "Account");
                }

               

                int userId = Convert.ToInt32(Session["UserID"]);

                // Appointment nesnesi oluştur
                var newAppointment = new Appointment
                {
                    DoctorID = doctorId,
                    PatientID = db.Patients.FirstOrDefault(p => p.UserID == userId)?.PatientID ?? 0,
                    AppointmentDate = dateTime.Date,
                    AppointmentTime = dateTime.TimeOfDay,

                };

            // Aynı tarih ve saat için doktorun zaten randevusu var mı kontrol et
            var existingAppointment = db.Appointments
                .FirstOrDefault(a => a.DoctorID == doctorId && a.AppointmentDate == dateTime.Date && a.AppointmentTime == dateTime.TimeOfDay);

            if (existingAppointment != null)
            {
                // Eğer randevu doluysa kullanıcıyı bilgilendir
                TempData["AppointmentErrorMessage"] = "Bu tarih ve saat için randevu doludur. Lütfen başka bir zaman seçiniz.";
                return RedirectToAction("Appointment", "Appointment");
            }


            try
            {
                db.Appointments.Add(newAppointment);
                db.SaveChanges();
            } catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Mesaj gönderimi sırasında bir hata oluştu: " + ex.Message;
            }

            // Başarılı bir işlem sonrası ana sayfaya yönlendirme
            //TempData["SuccessfullAppointment"] = "Başarıyla Randevu Alındı.";
            //TempData["RedirectAfterLogin"] = Url.Action("Main", "Home");
            return RedirectToAction("Main", "Home");
            }


        } 
} 


