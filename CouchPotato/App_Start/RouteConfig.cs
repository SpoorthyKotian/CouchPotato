using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace CouchPotato
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
            name: "Login",
            url: "Admin/Login",
            defaults: new { controller = "Session", action = "Login" }
        );

            routes.MapRoute(
               name: "AddHardDrive",
               url: "HardDrives/Add",
               defaults: new { controller = "HardDrives", action = "Add" }
           );

            routes.MapRoute(
              name: "AddType",
              url: "Types/Add",
              defaults: new { controller = "Types", action = "Add" }
          );

            routes.MapRoute(
             name: "AddLanguage",
             url: "Languages/Add",
             defaults: new { controller = "Languages", action = "Add" }
         );

            routes.MapRoute(
            name: "AddMovie",
            url: "MoviesList/Add",
            defaults: new { controller = "Admin", action = "ScanHD" }
        );

            routes.MapRoute(
           name: "SyncAllMovies",
           url: "MoviesList/SyncAll",
           defaults: new { controller = "MoviesList", action = "SyncAll" }
       );

            routes.MapRoute(
           name: "MovieDetails",
           url: "MovieDetails/Details/{id}",
           defaults: new { controller = "MovieDetails", action = "Details" , id=UrlParameter.Optional}
       );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

        }
    }
}
