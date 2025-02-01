using System;
using System.Web.Mvc;
using UnityCareHospital.Models;

namespace UnityCareHospital.Controllers
{
    public class ContactController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext(); 

        // GET: Contact
        public ActionResult Contact()
        {
            return View();
        }

        // POST: Contact
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(Contact model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    
                    db.Contacts.Add(model);
                    db.SaveChanges();
                    TempData["SuccessMessage"] = "Mesajınız başarıyla gönderildi!";
                }
                catch (Exception ex)
                {
                    // Hata durumunda
                    TempData["ErrorMessage"] = "Mesaj gönderimi sırasında bir hata oluştu: " + ex.Message;
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Lütfen formu doğru bir şekilde doldurunuz.";
            }

            return RedirectToAction("Contact", "Home");
        }

    }
}