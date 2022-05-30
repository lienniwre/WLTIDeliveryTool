using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using WLTIDeliveryTool.Repository;
using System.Linq.Dynamic.Core;
using System.Web.Script.Serialization;
using WLTIDeliveryTool.Models;

namespace WLTIDeliveryTool.Controllers
{
    public class ModeOfDeliveryController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        // GET: ModeOfDelivery
        public ModeOfDeliveryController(){
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        [MyAuthorize(Roles = "1,2")]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize(Roles = "1,2")]
        public JsonResult DisplayTable()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];


            var list =from a in _unitOfWork.ModeOfDelivery.GetAll() 
                      
                      select new {
                ModId = a.ModId,
                Definition = a.Definition
               
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
        [MyAuthorize(Roles = "1,2")]
        [HttpPost]
        public JsonResult SaveModeOfDelivery(ModeOfDelivery model)
        {
            try
            {


                _unitOfWork.ModeOfDelivery.Add(model);
                _unitOfWork.Complete();
              

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult GetModeOfDelivery(int id)
        {
            try
            {
                var modeOfDelivery =(from mod in _unitOfWork.ModeOfDelivery.GetAll().Where(m=>m.ModId == id) select new {Definition = mod.Definition.ToString(),ModId = (int)mod.ModId }).FirstOrDefault();
                return Json(new { data = modeOfDelivery, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.Message });
            }
        }
        [MyAuthorize(Roles = "1,2")]
        [HttpPost]
        public JsonResult SaveChangesToModeOfDelivery(ModeOfDelivery model)
        {
            try

            {

                var mod = _unitOfWork.ModeOfDelivery.Get(model.ModId);
                mod.Definition = model.Definition;
                _unitOfWork.ModeOfDelivery.Update(mod);
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