using HotelProject.Logic;
using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string mail, string code)
        {

            Person oPerson = PersonLogic.Instance.List_Person().Where(u => u.Mail == mail && u.Code == code && u.oPersonType.PersonTypeId != 3).FirstOrDefault();

            if (oPerson == null)
            {
                ViewBag.Error = "Incorrect username or password";
                return View();
            }

            Session["User"] = oPerson;

            return RedirectToAction("Index", "Start");
        }
    }
}