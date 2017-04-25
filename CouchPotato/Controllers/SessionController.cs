using CouchPotato.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CouchPotato.Controllers
{
    public class SessionController : Controller
    {
        private DatabaseContext db = new DatabaseContext();

        //GET: Admin/Login
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
                return RedirectToAction("ScanHD", "Admin");
            else
                return View();
        }

        // POST: Admin/Login
        [HttpPost]
        public ActionResult Login(AdminUsers UserModel)
        {
            try
            {
                var usr = db.AdminUsers.Single(u => u.Username == UserModel.Username && u.Password == UserModel.Password);

                if (usr != null)
                {
                    FormsAuthentication.SetAuthCookie(usr.Username.ToString(), true);
                    return RedirectToAction("ScanHD", "Admin");
                }
            }
            catch (InvalidOperationException ex)
            {
                var error = ex.Message;
                ModelState.AddModelError("User_Auth_Failure", "Error! Login details are incorrect.");
            }

            return View();
        }

        // Logout
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            return RedirectToAction("Index", "Home");
        }
    }
}