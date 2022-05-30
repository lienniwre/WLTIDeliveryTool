using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.Models;
using System.Linq.Dynamic.Core;
using DAL;
namespace WLTIDeliveryTool.Controllers
{
    [MyAuthorize(Roles = "1,2")]
    public class CSEController : Controller
    {
        // GET: CSE
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DisplayCSE()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var cse = new CseModel();
            var list = cse.GetAll();
            int totalRows = list.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                list = list.Where(x => x.FirstName.ToLower().Contains(searchValue.ToLower()) || x.LastName.ToLower().Contains(searchValue.ToLower())).ToList();
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
        [HttpPost]
        public JsonResult SaveCse( ChannelSalesExecutive model)
        {
            try
            {



                var cse = new CseModel();
                cse.Add(model);

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult GetCse(int id)
        {
            try
            {
                var model = new CseModel();
                var cse = model.Get(id);
                return Json(new { FirstName = cse.FirstName , LastName= cse.LastName,Id = cse.CSEID, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult SaveChangesToCse(ChannelSalesExecutive model)
        {
            try

            {
                var cseModel = new CseModel();
                var cse = cseModel.Get(model.CSEID);
                cse.FirstName = model.FirstName;
                cse.LastName = model.LastName;

                cseModel.Update(cse);

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
    }
}