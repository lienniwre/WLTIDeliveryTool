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
    public class EstimatedDeliveryController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        // GET: EstimatedDelivery

            public EstimatedDeliveryController()
        {
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        [MyAuthorize(Roles = "1,2")]
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


            var list = from a in _unitOfWork.EstimatedDelivery.GetAll()
                       select new
                       {
                           RangeFrom = a.RangeFrom,
                           RangeTo = a.RangeTo,
                           UnitOfTime = a.UnitOfTime,
                           Id = a.Id
                       };

            int totalRows = list.Count();

          
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
        [MyAuthorize(Roles = "1,2")]
        [HttpPost]
        public JsonResult SaveNew(EstimatedDelivery edt)
        {
            try

            {

                _unitOfWork.EstimatedDelivery.Add(edt);
                _unitOfWork.Complete();

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult GetEstimatedDelivery(int id)
        {
            try
            {
                var edt = _unitOfWork.EstimatedDelivery.Get(id);
                return Json(new { RangeFrom = edt.RangeFrom, RangeTo = edt.RangeTo, Id = edt.Id, UnitOfTime = edt.UnitOfTime, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.Message });
            }
        }
        [MyAuthorize(Roles = "1,2")]
        [HttpPost]
        public JsonResult SaveChanges(EstimatedDelivery model)
        {
            try

            {

                var leadTime = _unitOfWork.EstimatedDelivery.Get(model.Id);
                leadTime.RangeFrom = model.RangeFrom;
                leadTime.RangeTo = model.RangeTo;
                leadTime.UnitOfTime = model.UnitOfTime;

                _unitOfWork.EstimatedDelivery.Update(leadTime);
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