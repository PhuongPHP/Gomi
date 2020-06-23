using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GomiShop.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
            routes.MapRoute(
                name: "Trang chủ",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "GomiShop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Trang Admin",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Admin", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "GomiShop.Web.Controllers" }
            );
            routes.MapRoute(
                name: "Trang login",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional },
                namespaces: new string[] { "GomiShop.Web.Controllers" }
            );
            routes.MapRoute(
               name: "Product",
               url: "products",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "GomiShop.Web.Controllers" }
              );
        }
    }
}
