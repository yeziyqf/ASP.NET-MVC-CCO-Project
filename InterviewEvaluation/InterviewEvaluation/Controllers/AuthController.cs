using InterviewEvaluation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace InterviewEvaluation.Controllers
{
    [AllowAnonymous]
    public class AuthController : Controller
    {
        EntityModels.Entities db = new EntityModels.Entities();

        [HttpGet]
        public ActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            EntityModels.AspNetUser user = db.AspNetUsers.FirstOrDefault(x => x.Email == model.Email);

            if (user != null && EncodePasswordMd5(model.Password) == user.PasswordHash)
            {
                var identity = new ClaimsIdentity(new[] 
                {
                    new Claim(ClaimTypes.Name, user.Email),
                    new Claim(ClaimTypes.Email, user.Email)
                }, "ApplicationCookie");

                var ctx = HttpContext.GetOwinContext();
                var authManager = ctx.Authentication;

                authManager.SignIn(identity);

                return RedirectToAction("Index", "Home");
            }
            else
            {
                model = new LoginViewModel
                {
                    ReturnUrl = model.ReturnUrl
                };
            }

            // user authN failed
            ModelState.AddModelError("", "Invalid email or password");
            return View();
        }

        public ActionResult Logout()
        {
            HttpCookie myCookie = new HttpCookie("ApplicationCookie");

            myCookie.Expires = DateTime.Now.AddYears(-1);
            Response.Cookies.Add(myCookie);

            var ctx = Request.GetOwinContext();
            var authenticationManager = ctx.Authentication;
            string[] cookieTypes = {};

            // We're using OWIN, so need to clear using OWIN Auth Manager. 
            authenticationManager.SignOut(cookieTypes);
            Request.GetOwinContext().Authentication.SignOut(myCookie.Values.AllKeys);
            Session.Abandon();
            Session.RemoveAll();

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public ActionResult Register()
        {
            var model = new RegisterViewModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            if (db.AspNetUsers.FirstOrDefault(x => x.Email == model.Email) != null)
            {
                return View();
            }

            if (model.Password != model.ConfirmPassword)
            {
                return View();
            }

            EntityModels.AspNetUser newUser = new EntityModels.AspNetUser();

            newUser.Email = model.Email;
            newUser.FullName = model.FullName;
            newUser.ImgName = string.Empty;
            newUser.PasswordHash = EncodePasswordMd5(model.Password);
            newUser.AccessFailedCount = 0;
            newUser.AspNetRoles = db.AspNetRoles.FirstOrDefault(x => x.Name == "User");

            db.AspNetUsers.Add(newUser);
            db.SaveChanges();

            LoginViewModel loginModel = new LoginViewModel();

            loginModel.Email = newUser.Email;
            loginModel.Password = model.Password;

            Login(loginModel);

            return RedirectToAction("Index", "Home");
        }

        public static string EncodePasswordMd5(string pass) //Encrypt using MD5    
        {
            Byte[] originalBytes;
            Byte[] encodedBytes;
            MD5 md5;
            //Instantiate MD5CryptoServiceProvider, get bytes for original password and compute hash (encoded password)    
            md5 = new MD5CryptoServiceProvider();
            originalBytes = ASCIIEncoding.Default.GetBytes(pass);
            encodedBytes = md5.ComputeHash(originalBytes);
            //Convert encoded bytes back to a 'readable' string    
            return BitConverter.ToString(encodedBytes);
        }
    }
}
