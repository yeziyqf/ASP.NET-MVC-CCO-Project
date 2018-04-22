using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using System;
using System.Web.Mvc;

namespace InterviewEvaluation.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = "ApplicationCookie",
                //AuthenticationMode = Microsoft.Owin.Security.AuthenticationMode.Passive,
                //CookieName = "AuthenitcationCookie",
                //ExpireTimeSpan = TimeSpan.FromMinutes(10),
                LoginPath = new PathString("/Auth/Login")
            });
        }
    }
}