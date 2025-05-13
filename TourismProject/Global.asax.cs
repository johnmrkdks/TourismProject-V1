using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using TourismProject.Models;

namespace TourismProject
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            CreateRoles();
        }

        private void CreateRoles()
        {
            var context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists("Tourist"))
                roleManager.Create(new IdentityRole("Tourist"));

            if (!roleManager.RoleExists("Agency"))
                roleManager.Create(new IdentityRole("Agency"));
        }

    }
}
