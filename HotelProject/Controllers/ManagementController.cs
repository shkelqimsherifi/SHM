using HotelProject.Logic;
using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelProject.Controllers
{
    public class ManagementController : Controller
    {
		// GET: Management
		public ActionResult Reception()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Management
        public ActionResult ReceptionRegistration(int roomid)
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            Room oRoom = RoomLogic.Instance.List_Room().Where(h => h.RoomId == roomid).FirstOrDefault();

            return View(oRoom);
        }

        public ActionResult DetailReception(int roomid)
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            Reception oReception = ReceptionLogic.Instance.List_Reception().Where(h => h.oRoom.RoomId == roomid && h.State_text == true).FirstOrDefault();

            if (oReception != null)
            {

                List<Sale> oSale = (from vn in SaleLogic.Instance.List_Sale()
                                      where vn.oReception.ReceptionId == oReception.ReceptionId
                                      select new Sale()
                                      {
                                          SalesID = vn.SalesID,
                                          oReception = new Reception() { ReceptionId = vn.oReception.ReceptionId },
                                          Total = vn.Total,
                                          State_text = vn.State_text,
                                          oSaleDetail = SaleDetailLogic.Instance.List_SaleDetail().Where(dv => dv.SalesID == vn.SalesID).ToList()
                                      }).ToList();

                oReception.oSale = oSale;
            }

            return View(oReception);
        }

        // GET: Management
        public ActionResult Exit()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        // GET: Management
        public ActionResult DepartureRegistration(int roomid)
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            Reception oReception = ReceptionLogic.Instance.List_Reception().Where(h => h.oRoom.RoomId == roomid && h.State_text == true).FirstOrDefault();

            if (oReception != null) {

                List<Sale> oSale = (from vn in SaleLogic.Instance.List_Sale()
                                      where vn.oReception.ReceptionId == oReception.ReceptionId
                                      select new Sale()
                                      {
                                          SalesID = vn.SalesID,
                                          oReception = new Reception() { ReceptionId = vn.oReception.ReceptionId },
                                          Total = vn.Total,
                                          State_text = vn.State_text,
                                          oSaleDetail = SaleDetailLogic.Instance.List_SaleDetail().Where(dv => dv.SalesID == vn.SalesID).ToList()
                                      }).ToList();

                oReception.oSale = oSale;
            }

            return View(oReception);
        }

        // GET: Management
        public ActionResult Sale(int roomid)
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            Reception oReception = ReceptionLogic.Instance.List_Reception().Where(h => h.oRoom.RoomId == roomid && h.State_text == true).FirstOrDefault();

            return View(oReception);
        }


        [HttpGet]
        public JsonResult ListRoom(int floorid)
        {
            List<Room> oList = new List<Room>();
            oList = RoomLogic.Instance.List_Room().Where(x => x.oHotelFloor.FloorId == (floorid == 0 ? x.oHotelFloor.FloorId : floorid) ).OrderBy(o => o.Number).ToList();
            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult UpdateRoomStatus(int roomid,int idstateroom_text)
        {

            bool response = false;
            response = RoomLogic.Instance.UpdateStatus_Room(roomid, idstateroom_text);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SavePerson(Reception obj)
        {
            bool response = false;
            response = ReceptionLogic.Instance.Register_Reception(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult RegisterReception(Reception obj)
        {
            bool response = false;
            obj.Observation = obj.Observation == null ? "" : obj.Observation;
            response = ReceptionLogic.Instance.Register_Reception(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult CloseReception(Reception obj)
        {
            bool response = false;
            response = ReceptionLogic.Instance.Close_Reception(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
    }
}