using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.Repository;
using System.Linq.Dynamic.Core;
using WLTIDeliveryTool.Models;

namespace WLTIDeliveryTool.Controllers
{
    [MyAuthorize(Roles = "1,2")]
    public class CourierController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        public CourierController()
        {
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        // GET: Courier
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult DisplayTable()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            var list = from a in _unitOfWork.Courier.GetAll()
                       join b in _unitOfWork.ModeOfDelivery.GetAll() on a.ModId equals b.ModId into ab
                       from c in ab.DefaultIfEmpty()
                       join d in _unitOfWork.EstimatedDelivery.GetAll() on a.EstimatedDeliveryTime equals d.Id into ad
                       from e in ad.DefaultIfEmpty()
                       select new
                       {
                           ModId = a.ModId,
                           Definition = a.Definition,
                           ModeOfDelivery = c.Definition,
                           CourierId = a.DeliveryOptionId,
                           EstimatedDeliveryTime = e.RangeFrom.ToString() + "-" + e.RangeTo.ToString() + " " + e.UnitOfTime
                       };
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
        public JsonResult GetCourier(int id)
        {
            try
            {
                var courier =  _unitOfWork.Courier.Get(id);
                return Json(new { Definition = courier.Definition,ModId = courier.ModId,Id=courier.DeliveryOptionId,Eta =courier.EstimatedDeliveryTime, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult SaveChanges(DeliveryOption model)
        {
            try

            {

                var courier = _unitOfWork.Courier.Get(model.DeliveryOptionId);
                courier.Definition = model.Definition;
                courier.ModId = model.ModId;
                courier.EstimatedDeliveryTime = model.EstimatedDeliveryTime;
                
                _unitOfWork.Courier.Update(courier);
                _unitOfWork.Complete();

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult SaveNew(DeliveryOption courier)
        {
            try

            {

                _unitOfWork.Courier.Add(courier);
                _unitOfWork.Complete();

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
    }
}