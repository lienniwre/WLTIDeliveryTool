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
    public class DeliveryProgressController : Controller
    {
        // GET: DeliveryProgress
        public ActionResult Index()
        {
            return View();
        }
        public JsonResult DisplayProgress()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var deliveryProgress = new DeliveryProgressModel();
            var list = deliveryProgress.GetAll();
            int totalRows = list.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                list = list.Where(x => x.Definition.ToLower().Contains(searchValue.ToLower())).ToList();
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
        public JsonResult SaveProgress(DeliveryProgress model)
        {
            try
            {



                var progress = new DeliveryProgressModel();
                progress.Add(model);

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult GetProgress(int id)
        {
            try
            {
                var model = new DeliveryProgressModel();
                var progress = model.Get(id);
                return Json(new { data = progress, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult SaveChangesToProgress(DeliveryProgress model)
        {
            try

            {
                var progressModel = new DeliveryProgressModel();
                var progress = progressModel.Get(model.Id);
                progress.Definition = model.Definition;
               

                progressModel.Update(progress);

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }

    }
}