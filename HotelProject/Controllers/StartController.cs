using HotelProject.Logic;
using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelProject.Controllers
{
    public class StartController : Controller
    {
        // GET: Start
        public ActionResult Index()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Start
        public ActionResult User()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Start
        public ActionResult Customer()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListTypePerson()
        {
            List<PersonType> oList = new List<PersonType>();
            oList = PersonLogic.Instance.List_PersonType();
            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult ListPerson()
        {
            List<Person> oList = new List<Person>();

            oList = PersonLogic.Instance.List_Person();

            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SavePerson(Person obj)
        {
            bool response = false;
            obj.Code = obj.Code == null ? "" : obj.Code;
            obj.DocumentType = obj.DocumentType == null ? "" : obj.DocumentType;
            response = (obj.PersonId == 0) ? PersonLogic.Instance.Register_Person(obj) : PersonLogic.Instance.Modify_Person(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeletePerson(int id)
        {
            bool response = false;
            response = PersonLogic.Instance.Remove_Person(id);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult CloseSession()
        {
            Session["User"] = null;
            return RedirectToAction("Index", "Login");
        }
    }
}