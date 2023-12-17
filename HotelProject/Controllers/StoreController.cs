using HotelProject.Logic;
using HotelProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelProject.Controllers
{
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Product()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Sell()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpGet]
        public JsonResult ListProduct()
        {
            List<Product> oList = new List<Product>();
            oList = ProductLogic.Instance.List_Product().OrderBy(o => o.ProductName).ToList();
            return Json(new { data = oList }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult SaveProduct(Product obj)
        {
            bool response = false;
            response = (obj.ProductID == 0) ? ProductLogic.Instance.Register_Product(obj) : ProductLogic.Instance.Modify_Product(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult DeleteProduct(int id)
        {
            bool response = false;
            response = ProductLogic.Instance.Remove_Product(id);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult RegisterSale(Sale obj)
        {
            bool response = false;
            response = SaleLogic.Instance.Register_Sale(obj);
            return Json(new { result = response }, JsonRequestBehavior.AllowGet);
        }

    }
}