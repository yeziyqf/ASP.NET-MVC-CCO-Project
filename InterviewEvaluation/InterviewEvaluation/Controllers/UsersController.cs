using InterviewEvaluation.Models;
using Newtonsoft.Json;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Services;
using System.IO;
using System.Web.Script.Serialization;

namespace InterviewEvaluation.Controllers
{
    public class UsersController : Controller
    {
        private EntityModels.Entities db = new EntityModels.Entities();

		[HttpGet]
        public ActionResult List(string sortOrder, string searchString, int? page)
        {
            UsersListViewModel model = new UsersListViewModel();
            List<EntityModels.AspNetUser> users = db.AspNetUsers.OrderBy(x => x.Email).ToList();

            // Add Data Management evaluation section changes here.



            // End changes.

            // Solution:
            // Sorting
            ViewBag.IdSortParam = string.IsNullOrEmpty(sortOrder) ? "Id_desc" : "";
            ViewBag.EmailSortParam = sortOrder == "Email" ? "Email_desc" : "Email";

            switch (sortOrder)
            {
                case "Id_desc":
                    users.OrderByDescending(x => x.Id);
                    break;
                case "Email":
                    users.OrderBy(x => x.Email);
                    break;
                case "Email_desc":
                    users.OrderByDescending(x => x.Email);
                    break;
                default:
                    users.OrderBy(x => x.Id);
                    break;
            }

            //Paging
            int pageSize = 5;
            int pageNumber = (page ?? 1);

            // End solution.

            return View(users.ToPagedList(pageNumber, pageSize));
        }

        // Use this method to complete the JavaScript section of the evaluation.
        //[HttpGet]
        [HttpPost]
        [System.Web.Services.WebMethod]
        public JsonResult Search(string query)
        {
            List<EntityModels.AspNetUser> users = new List<EntityModels.AspNetUser>();
            users = db.AspNetUsers.Where(x => x.Email.StartsWith(query)).ToList();
            List<SelectListItem> userObjs = new List<SelectListItem>();

            foreach (var user in users)
            {
                var userObj = new SelectListItem
                {
					Value = user.Id.ToString(),
					Text = user.Email
                };

                userObjs.Add(userObj);
            }

            return new JsonResult()
            {
                Data = userObjs,
                JsonRequestBehavior = System.Web.Mvc.JsonRequestBehavior.AllowGet
            };
        }

		[HttpGet]
		// Param id is the Id of the user to edit.
        public ActionResult Edit(int? id)
        {
            UsersEditViewModel model = new UsersEditViewModel();
            EntityModels.AspNetUser user = new EntityModels.AspNetUser();

            if (id.HasValue)
            {
                user = db.AspNetUsers.FirstOrDefault(x => x.Id == id);
            }
            else
            {
                user = db.AspNetUsers.FirstOrDefault(x => x.Email == User.Identity.Name);
            }

            model.Id = user.Id;
            model.UserRole = user.AspNetRoles.Id;

            return View();
        }

        [HttpPost]
        public ActionResult Edit(UsersEditViewModel model)
        {
            // Add Data Management evaluation section changes here.



            // End changes.

            // Solution:

            
            if (model.Id != 0)
            {
                EntityModels.AspNetUser user = db.AspNetUsers.FirstOrDefault(x => x.Id == model.Id);
                // Modifying password and roles.
                if (model.NewPassword != string.Empty)
                {
                    user.PasswordHash = AuthController.EncodePasswordMd5(model.NewPassword);
                }

                if (model.UserRole != user.AspNetRoles.Id)
                {
                    user.AspNetRoles = db.AspNetRoles.FirstOrDefault(x => x.Id == model.UserRole);
                    //user.AspNetRoles.Name = model.UserRole.ToString();
                }

                db.SaveChanges();
                return RedirectToAction("Edit", new { id = user.Id });                
            }
            else
            {
                EntityModels.AspNetUser user = new EntityModels.AspNetUser();
                user = db.AspNetUsers.FirstOrDefault(x => x.Email == User.Identity.Name);

                // Modifying password and roles.
                if (model.NewPassword != string.Empty)
                {
                    user.PasswordHash = AuthController.EncodePasswordMd5(model.NewPassword);
                }

                if (model.UserRole != user.AspNetRoles.Id)
                {
                    user.AspNetRoles = db.AspNetRoles.FirstOrDefault(x => x.Id == model.UserRole);
                    //user.AspNetRoles.Name = model.UserRole.ToString();
                }

                db.SaveChanges();
                return RedirectToAction("Edit", new { id = user.Id });
            }

            
            // End solution.

            
        }
    }
}
