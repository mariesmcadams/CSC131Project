using MVCCSC131Project.Models;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace MVCCSC131Project.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        //insert code  for login/security 
        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        /// Implement custom membership and role providers as per usual
        /// Put [Authorize] on any method, or class to restrict access to
        /// To restrict access to a specific user or users, set as:  [Authorize(Users="SomeUser")] //The Users property accepts a comma separated list of user account names.
        /// To restrict access to a specific role, set as: [Authorize(Roles = "SomeRole")] //The Roles property accepts a comma separated list of user account names.
        /// can specify roles AND users, but if that is done, BOTH sets must pass (user has to be in user list AND have role in roles list)
        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(string username, string password)
        {
            //string queryUsername = "select * from users where '" + username + "' = username and '" + password + "' = password"
            //if(username == (DBConnection.values(query, fname) && password == (DBConnection.values(query, pswd))
           // if (Membership.ValidateUser(username, password)) Todo: UNCOMMENT THIS LATER!!!!!!!
            if(true)//MAKE SURE YOU CHANGE THIS!! DO NOT USE! 
            {
                //var authTicket = new FormsAuthenticationTicket(1, username, DateTime.Now, DateTime.Now.AddMinutes(30), true, Role);
                var authTicket = new FormsAuthenticationTicket(username, true, 30);
                string cookieContents = FormsAuthentication.Encrypt(authTicket);
                var cookie = new HttpCookie(FormsAuthentication.FormsCookieName, cookieContents)
                {
                    Expires = authTicket.Expiration,
                    Path = FormsAuthentication.FormsCookiePath
                };
                Response.Cookies.Add(cookie);
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }
        }
        [HttpGet]
        public ActionResult Logout()
        {
            if (FormsAuthentication.FormsCookieName != null)
            {
                HttpCookie cookie = new HttpCookie(FormsAuthentication.FormsCookieName);
                cookie.Expires = DateTime.Now.AddDays(-1d);
                Response.Cookies.Add(cookie);
            }
            Session.Abandon();
            Session.Clear();
            Response.Cookies.Add(new HttpCookie(FormsAuthentication.FormsCookieName, ""));
            //FormsAuthentication.SignOut();
            return RedirectToAction("Index");
        }

        // GET: Home
        public ActionResult Index()
        {
            //Testing code, in the future make another index function 
            
            DBConnection currentQuery = DBConnection.Instance();
            List<String> results = currentQuery.values("Select * from users", "lname");

            ViewBag.results = results;
            return View();
            
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            //return Json(new { foo = "bar", baz = "blech" }, JsonRequestBehavior.AllowGet);    
            return View();
        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
