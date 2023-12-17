using ClosedXML.Excel;
using HotelProject.Logic;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HotelProject.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        public ActionResult Products()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        public ActionResult Receptions()
        {
            if (Session["User"] == null)
                return RedirectToAction("Index", "Login");

            return View();
        }

        [HttpPost]
        public FileResult ExportProducts(string state,string startdate, string enddate)
        {

            DataTable dt = ReportLogic.Instance.Product_Report(state,startdate,enddate);

            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report Products " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }

        [HttpPost]
        public FileResult ExportReceptions(string roomid, string startdate, string enddate)
        {

            DataTable dt = ReportLogic.Instance.Reception_Report(roomid, startdate, enddate);

            dt.TableName = "Datos";
            using (XLWorkbook wb = new XLWorkbook())
            {
                wb.Worksheets.Add(dt);
                using (MemoryStream stream = new MemoryStream())
                {
                    wb.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Report Receptions " + DateTime.Now.ToString() + ".xlsx");
                }
            }
        }
    }
}