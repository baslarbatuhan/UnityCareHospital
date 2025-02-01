using System.Web.Mvc;
using System.Web.Routing;

namespace UnityCareHospital
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            // Statik dosyaları (CSS, JS, Images) dışla
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // SEO dostu ve
         

            routes.MapRoute(
               name: "Contact",
               url: "iletisim",
               defaults: new { controller = "Contact", action = "Contact" }
           );


            routes.MapRoute(
                name: "About",
                url: "hakkimizda",
                defaults: new { controller = "Home", action = "About" }
            );

            routes.MapRoute(
                name: "Doctors",
                url: "doktorlarimiz",
                defaults: new { controller = "Home", action = "Doctors" }
            );

            routes.MapRoute(
                name: "Login",
                url: "giris-yap",
                defaults: new { controller = "Account", action = "Login" }
            );

            routes.MapRoute(
                name: "Signup",
                url: "uye-ol",
                defaults: new { controller = "Account", action = "Signup" }
            );

            routes.MapRoute(
                name: "Appointment",
                url: "randevu-al",
                defaults: new { controller = "Appointment", action = "Appointment" } 
            );

          
            routes.MapRoute(
                name: "UserProfile",
                url: "profil",
                defaults: new { controller = "Profile", action = "Profile" }
           );

            // Varsayılan rota (genel yapı)
            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Main", id = UrlParameter.Optional }
            );
        }
    }
}
