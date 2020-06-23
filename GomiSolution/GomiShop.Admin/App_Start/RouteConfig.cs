using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace GomiShop.Admin
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*botdetect}", new { botdetect = @"(.*)BotDetectCaptcha\.ashx" });

            //===============================================
            //                  *Product*
            //===============================================
            // List
            routes.MapRoute(
               name: "Product",
               url: "products",
               defaults: new { controller = "Product", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "GomiShop.Admin.Controllers" }
              );
            routes.MapRoute(
               name: "Category",
               url: "category",
               defaults: new { controller = "Category", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "GomiShop.Admin.Controllers" }
              );
            routes.MapRoute(
               name: "Home",
               url: "admin",
               defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
               namespaces: new string[] { "GomiShop.Admin.Controllers" }
              );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);

            //===============================================
            //                  *ACCOUNT*
            //===============================================

            // Sign In
            //routes.MapRoute("AccountSignIn", "sign-in", new
            //{
            //    controller = "Account",
            //    action = "SignIn",
            //}, namespaces: new[] { "Web.Admin.Controllers" });


        }
    }
}
