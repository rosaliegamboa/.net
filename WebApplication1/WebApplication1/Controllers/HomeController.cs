using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult input()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult AddUserToDatabase(FormCollection fc)
        {
            String firstName = fc["firstname"];
            String lastName = fc["lastname"];
            String email = fc["email"];
            String diko = fc["password"];

            user use = new user();
            use.firstName = firstName;
            use.lastName = lastName;
            use.email = email;
            use.password = diko;
            use.roleId = 1;

            mydatabaseEntities4 fe = new mydatabaseEntities4();
            fe.users.Add(use);
            fe.SaveChanges();

            //insert the code that will save these information to the DB

            return RedirectToAction("input");
        }
        public ActionResult ShowUser()
        {
            mydatabaseEntities4 fe = new mydatabaseEntities4();
            var userList = (from a in fe.users
                            select a).ToList();

            ViewData["UserList"] = userList;
            return View();
        }

        public ActionResult EditUser(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("ShowUser");
            }

            mydatabaseEntities4 fe = new mydatabaseEntities4();
            var user = fe.users.Find(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditUser(user updatedUser)
        {
            if (ModelState.IsValid)
            {
                mydatabaseEntities4 fe = new mydatabaseEntities4();
                var existingUser = fe.users.Find(updatedUser.userId);

                if (existingUser == null)
                {
                    return HttpNotFound();
                }

                existingUser.firstName = updatedUser.firstName;
                existingUser.lastName = updatedUser.lastName;
                existingUser.email = updatedUser.email;
                existingUser.password = updatedUser.password;

                fe.SaveChanges();

                return RedirectToAction("ShowUser");
            }

            return View(updatedUser);
        }


        public ActionResult DeleteUser(int id)
        {
            mydatabaseEntities4 fe = new mydatabaseEntities4();
            var user = fe.users.Find(id);

            if (user != null)
            {
                fe.users.Remove(user);
                fe.SaveChanges();
            }

            return RedirectToAction("ShowUser");
        }


    }
}
