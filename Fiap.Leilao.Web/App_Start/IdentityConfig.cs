using Fiap.Leilao.Dominio.DataAccess;
using Fiap.Leilao.Dominio.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fiap.Leilao.Web.App_Start
{
    public class IdentityConfig
    {
        public static Func<UserManager<User>> UserManagerFactory { get; private set; }
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/User/LogIn")
            });
            UserManagerFactory = () =>
            {
                var usermanager = new UserManager<User>(
                new UserStore<User>(new UserContext()));
                usermanager.UserValidator = new UserValidator<User>(usermanager)
                {
                    AllowOnlyAlphanumericUserNames = false
                };
                return usermanager;
            };
        }
    }
}