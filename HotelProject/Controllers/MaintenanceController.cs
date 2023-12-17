using HotelProject.Logic;
using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelProject.Controllers
{
    public class MaintenanceController : Controller
    {
		// GET: Maintenance
		public ActionResult Rooms()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Maintenance
        public ActionResult Category()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Maintenance
        public ActionResult Floors()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }



        [HttpGet]
        public JsonResult ListCategory()
        {
            List<Category> oList = new List<Category>();
            oList = CategoryLogic.Instance.List_Category();
            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveCategory(Category obj)
        {
            bool response = false;
            response = (obj.CategoryId == 0) ? CategoryLogic.Instance.Register_Category(obj) : CategoryLogic.Instance.Modify_Category(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteCategory(int id)
        {
            bool response = false;
            response = CategoryLogic.Instance.Remove_Category(id);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListFloor()
        {
            List<HotelFloor> oList = new List<HotelFloor>();
            oList = HotelFloorLogic.Instance.List_HotelFloor();
            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveFloor(HotelFloor obj)
        {
            bool response = false;
            response = (obj.FloorId == 0) ? HotelFloorLogic.Instance.Register_HotelFloor(obj) : HotelFloorLogic.Instance.Modify_HotelFloor(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteFloor(int id)
        {
            bool response = false;
            response = HotelFloorLogic.Instance.Remove_HotelFloor(id);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }


        [HttpGet]
        public JsonResult ListRoom()
        {
            List<Room> oList = new List<Room>();
            oList = RoomLogic.Instance.List_Room();
            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveRoom(Room obj)
        {
            bool response = false;
            response = (obj.RoomId == 0) ? RoomLogic.Instance.Register_Room(obj) : RoomLogic.Instance.Modify_Room(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteRoom(int id)
        {
            bool response = false;
            response = RoomLogic.Instance.Remove_Room(id);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
    }
}