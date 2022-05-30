using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.Models;
using System.Linq.Dynamic.Core;


namespace WLTIDeliveryTool.Controllers
{
    public class HomeController : Controller
    {
       [Authorize]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize(Roles = "1,2")]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [MyAuthorize(Roles = "1,2")]
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public JsonResult DisplayDrm()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var drm = new DrmModel();
            var list = drm.GetList();
            int totalRows = list.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                list = list.Where(x => x.ClientName.ToLower().Contains(searchValue.ToLower()) ||
                x.RequestedBy.ToString().Contains(searchValue.ToLower()) || x.DrNumber.ToString().Contains(searchValue.ToLower())).OrderByDescending(m=>m.DrmDate).ToList();
            }
            int totalRowsAfterFiltering = list.Count();

            list = list.AsQueryable().OrderBy(sortColumnName + " " + sortDirection).ToList();

            list = list.Skip(start).Take(length).ToList();

            return Json(new
            {
                data = list,
                draw = Request["draw"],
                recordsTotal = totalRows,
                recordsFiltered = totalRowsAfterFiltering
            }, JsonRequestBehavior.AllowGet);
        }
    }
}