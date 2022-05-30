using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.ViewModel;
using WLTIDeliveryTool.Repository;
using DAL;
using WLTIDeliveryTool.Models;
using WLTIDeliveryTool.Helper;
using System.Linq.Dynamic.Core;
using HashidsNet;
using System.Globalization;

namespace WLTIDeliveryTool.Controllers
{
    public class DRMController : Controller
    {
        public readonly UnitOfWork _unitOfWork;
        public Hashids hashids;
        public DRMController()
        {
            hashids = new Hashids("washink19",12);
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        // GET: DRM
        [MyAuthorize(Roles = "1,2")]
        public ActionResult Index()
        {
            return View();
        }
        [MyAuthorize(Roles = "1,2")]
        public ActionResult CreateDRM()
        {
            return View();
        }

      
        public ActionResult UpdateDRM(string f)
        {

            try
            {
                var id = hashids.Decode(f)[0];
                var drm = _unitOfWork.DRM.Get(id);

                var drmViewModel = new UpdateDRMViewModel
                {
                    ClientId = (int)drm.ClientId,
                    ConfirmedShipment = (int)drm.ConfirmedShipment,
                    CseId = (int)drm.CseId,
                    DateReceivedByClient = drm.DateReceived == null ? drm.DateReceived : drm.DateReceived.Value,
                    DeliveryOptionId = drm.ModOptionId == null ? drm.ModOptionId : (int)drm.ModOptionId,
                    DeliveryReceiptNumber = (int)drm.DrNumber,
                    WayBillNo = drm.WaybillNo == null?"---": drm.WaybillNo,
                    QuantityOfParcel = drm.QuantityOfParcel == null ? 0 : (int)drm.QuantityOfParcel,
                    ModId = (int)drm.ModId,
                    Releasing = (int)drm.Releasing,
                    DrmDate = drm.DrmDate.Value,
                    HashId = hashids.Encode((int)drm.DrmId),

                };

                return View(drmViewModel);

            }
            catch (Exception)
            {

                return View();
            }
           
           
        }

        [HttpPost]
        public JsonResult SaveDRM(DRMViewModel model)
        {
            try
            {

                var drmModel = new DrmModel();
                drmModel.CreateDrm(model);
                

                return Json(new { returnCode = 200, message="success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }

        public JsonResult IsAlreadyUsed2([Bind(Prefix= "DeliveryReceiptNumber")] int drNumber)
        {
            try
            {
                bool alreadyUsed = _unitOfWork.DRM.GetAll().Any(m => m.DrNumber == drNumber);
                return Json(!alreadyUsed);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [HttpPost]
        public JsonResult SaveProgress(DisplayProgressViewModel model,string f)
        {
            try
            {

                model.ProgressDate.ToString("d", DateTimeFormatInfo.InvariantInfo);

                var id = hashids.Decode(f)[0];
                Nullable<DateTime> dateConfirmed = null;
               if(model.Confirmed ==1)
                {
                    dateConfirmed = DateTime.Now;
                    model.ProgressDate = DateTime.Now;
                }
                var drmProgress = new DrmProgress
                {
                    DateCreated = model.ProgressDate,
                    DeliveryProgressId = model.ProgressId,
                    Confirmed = model.Confirmed,
                    DateConfirmed = dateConfirmed
                    
                };

                var drmModel = new DrmModel();
               drmModel.AddProgress(id,drmProgress);
                if(model.ProgressId == 7)
                {
                    var drm = _unitOfWork.DRM.Get(id);
                    drm.ConfirmedShipment = 3;
                    drm.Releasing = 3;
                    _unitOfWork.DRM.Update(drm);
                    _unitOfWork.Complete();
                }

                if(model.ProgressId == 5 || model.ProgressId == 3 || model.ProgressId == 8 || model.ProgressId == 9 || model.ProgressId == 15 )
                {
                    var drm = _unitOfWork.DRM.Get(id);
                    drm.ConfirmedShipment = 3;
                   
                    _unitOfWork.DRM.Update(drm);
                    _unitOfWork.Complete();
                }

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult SaveChangesToDrm(DRMViewModel model)
        {
            try
            {
                var id = hashids.Decode(model.HashId)[0]; 
                var drm = _unitOfWork.DRM.Get(id);

                
                drm.ClientId = model.ClientId;
                drm.CseId = model.CseId;
                drm.ModId = model.ModId;
                drm.ModOptionId = model.DeliveryOptionId;
                drm.ConfirmedShipment = model.ConfirmedShipment;
             
                drm.Releasing = model.Releasing;
                drm.WaybillNo = model.WayBillNo;
                drm.QuantityOfParcel = model.QuantityOfParcel;

                drm.DrmDate = model.DrmDate;

                if(drm.DrNumber == model.DeliveryReceiptNumber)
                {
                    drm.DrNumber = model.DeliveryReceiptNumber;
                    _unitOfWork.DRM.Update(drm);
                    _unitOfWork.Complete();
                }
                else
                {
                    if(_unitOfWork.DRM.GetAll().Where(m=>m.DrNumber == model.DeliveryReceiptNumber).Any()){
                        return Json(new { returnCode = 300, message = "Dr Number Already exists." });
                    }
                    else
                    {
                        drm.DrNumber = model.DeliveryReceiptNumber;
                        _unitOfWork.DRM.Update(drm);
                        _unitOfWork.Complete();
                    }
                }


               

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }


        [HttpPost]
        public JsonResult DeliveryOptionList(int modId)
        {
            try
            {

                var optionList = SelectListHelper.DeliveryOption(modId);
                return Json(new { data = optionList });
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }

        public ActionResult DisplayDRM(string f)
        {
           
                var id = hashids.Decode(f)[0];

                var drmModel = new DrmModel();
                var drm = drmModel.DisplayDetails(id);

                

                return View(drm);

           
        }
        [MyAuthorize(Roles = "1,2")]
        public ActionResult AdminDisplay(string f)
        {
            var id = hashids.Decode(f)[0];

            var drmModel = new DrmModel();
            var drm = drmModel.DisplayDetails(id);

            var drm2 = new AdminDisplayDrmViewModel
            {
                Details = drm
            };

            return View(drm2);
        }
        [MyAuthorize(Roles = "1,2")]
        public ActionResult CreateProgress()
        {
            return View();
        }

        public JsonResult DisplayProgress(string f)
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var drmId = hashids.Decode(f)[0];

            var drm = new DrmModel();
            var list = drm.GetProgress(drmId);
            int totalRows = list.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                list = list.Where(x => x.Definition.ToLower().Contains(searchValue.ToLower())).ToList();
            }
            int totalRowsAfterFiltering = list.Count();

            list = list.AsQueryable().OrderBy(sortColumnName + " " + sortDirection).ToList();

            

            return Json(new
            {
                data = list,
                draw = Request["draw"],
                recordsTotal = totalRows,
                recordsFiltered = totalRowsAfterFiltering
            }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetProgress(string f,int pId)
        {
            try
            {
                var id = hashids.Decode(f)[0];
                var model = new DrmModel();
                var progress = model.GetProgressById(id,pId);
                var newModel = new DisplayProgressViewModel
                {
                    ProgressId = (int) progress.ProgressId,
                    ProgressDate = progress.ProgressDate,
                    Id = progress.Id,
                    Definition = progress.Definition

                };
                return Json(new { data =  newModel,message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult SaveChangesToProgress(DrmProgress model,string f)
        {
            try

            {
                var id = hashids.Decode(f)[0];
                var drmProgress = _unitOfWork.DRM.Get(id).DrmProgresses.Where(m => m.Id == model.Id).FirstOrDefault();
                drmProgress.DateCreated = model.DateCreated;
                drmProgress.DeliveryProgressId = model.DeliveryProgressId;
                _unitOfWork.DRM.UpdateProgress(drmProgress);
                _unitOfWork.Complete();

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }

        [HttpPost]
        public JsonResult DisplayDrmProgress(string f)
        {
            try

            {
                var id = hashids.Decode(f)[0];
                var drmProgress = from progress in _unitOfWork.DRM.Get(id).DrmProgresses.ToList().OrderByDescending(m=>m.DateCreated)
                                  join definition in _unitOfWork.DeliveryProgress.GetAll() on progress.DeliveryProgressId equals definition.Id
                                  select new
                                  {
                                      Definition = definition.Definition.ToString(),
                                      Date = progress.DateCreated.Value
                                      
                                  };
               

                return Json(new { data = drmProgress, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
    }
}