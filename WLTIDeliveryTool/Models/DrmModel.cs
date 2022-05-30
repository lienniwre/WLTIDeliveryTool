using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Repository;
using WLTIDeliveryTool.ViewModel;
using HashidsNet;
using System.Globalization;

namespace WLTIDeliveryTool.Models
{
    public class DrmModel
    {
        private readonly UnitOfWork _unitOfWork;
        public Hashids hashids;
        public DrmModel()
        {
            hashids = new Hashids("washink19", 12);
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        public void CreateDrm(DRMViewModel model)
        {
            var dispatchrequisition = new DispatchRequisitionMonitoring
            {
                ClientId = model.ClientId,
                ConfirmedShipment = model.ConfirmedShipment,
                CreatedById = 1,
                CseId = model.CseId,
                DateCreated = DateTime.Now,
                DateReceived = model.DateReceivedByClient,
                DrmDate = model.DrmDate,
                DrNumber = model.DeliveryReceiptNumber,
                ModOptionId = model.DeliveryOptionId,
                IsDeleted = 0,
                ModId = model.ModId,
                Releasing = model.Releasing,
                Status = 1,
                WaybillNo = model.WayBillNo,
                QuantityOfParcel = model.QuantityOfParcel
              
            };
            dispatchrequisition.DrmProgresses.Add(new DrmProgress {DateCreated= DateTime.Now,DeliveryProgressId=1 });
            _unitOfWork.DRM.Add(dispatchrequisition);
            _unitOfWork.Complete();
        }

        public void UpdateDrm(DispatchRequisitionMonitoring drm)
        {

            _unitOfWork.DRM.Update(drm);
            _unitOfWork.Complete();
        }


        public List<DisplayDrmViewModel> GetList()
        {
            var drmList = (from d in _unitOfWork.DRM.GetAll()
                           join drmProgress in _unitOfWork.DrmProgress.GetAll() on d.DrmId equals drmProgress.DrmId into drc
                           from dr in drc.Where(m => m.DeliveryProgressId == 5).DefaultIfEmpty()
                           

                           join c in _unitOfWork.Client.GetAll() on d.ClientId equals c.ClientId into dc
                           from drmClient in dc.DefaultIfEmpty()

                           join cse in _unitOfWork.CSE.GetAll() on d.CseId equals cse.CSEID into dcse
                           from drmcse in dcse.DefaultIfEmpty()
                           join mod in _unitOfWork.ModeOfDelivery.GetAll() on d.ModId equals mod.ModId into dmod
                           from drmMod in dmod.DefaultIfEmpty()

                           join courier in _unitOfWork.Courier.GetAll() on d.ModOptionId equals courier.DeliveryOptionId into modcourier
                           from modoption in modcourier.DefaultIfEmpty()

                           join cs in _unitOfWork.DeliveryStatus.GetAll() on d.ConfirmedShipment equals cs.Id into dcs
                           from drmcs in dcs.DefaultIfEmpty()


                           join releasing in _unitOfWork.DeliveryStatus.GetAll() on d.Releasing equals releasing.Id into dReleasing
                           from drmReleasing in dReleasing.DefaultIfEmpty()


                               // from drmcs in dcs.DefaultIfEmpty()
                           select new DisplayDrmViewModel
                           {
                               DrmDate = d.DrmDate ?? d.DrmDate.Value,
                               DrNumber = d.DrNumber ?? (int)d.DrNumber,
                               ClientName = drmClient.ClientName,
                               RequestedBy = drmcse.FirstName + " " + drmcse.LastName,
                               ModeOfDelivery = drmMod.Definition,
                               ConfirmedShipment = drmcs.Definition == null ? "" : drmcs.Definition,

                               Releasing = drmReleasing.Definition,
                               DateReceivedByClient = dr?.DateCreated.Value,
                               HashId = hashids.Encode(d.DrmId),
                               Courier = modoption?.Definition
                           }


                           ).OrderByDescending(m=>m.DrmDate).ToList();
            return drmList;
        }


        public DisplayDrmViewModel DisplayDetails(int id)
        {
           
            var drm = _unitOfWork.DRM.Get(id);
            var model = new DisplayDrmViewModel
            {
                ClientName = drm.Client.ClientName,
                ConfirmedShipment = drm.DeliveryStatus1.Definition,
                Releasing = drm.DeliveryStatus.Definition,
                Courier = drm.DeliveryOption.Definition,
                HashId = hashids.Encode(drm.DrmId),
                DrmDate = drm.DrmDate.Value,
                DrNumber = (int)drm.DrNumber,
                QuantityOfParcel = drm.QuantityOfParcel == null ? "---" : drm.QuantityOfParcel == 0 ? "---" : drm.QuantityOfParcel.ToString(),
                ModId = (int)drm.ModId,
                Edt = this.GetEstimatedDeliveryTime(drm.DrmId),
                RequestedBy = drm.ChannelSalesExecutive.FirstName + " " + drm.ChannelSalesExecutive.LastName,
                DeliveryOption = drm.DeliveryOption.DeliveryOptionId,
                WayBillNo = drm.WaybillNo == null ? "---" : drm.WaybillNo == "" ? "---" : drm.WaybillNo.ToString(),
                IsDelivered = this.IsDeliveredToClient(drm.DrmId)

            };



            return model;
        }

        public void AddProgress(int drmId, DrmProgress progress)
        {
            var drm = _unitOfWork.DRM.Get(drmId);
            drm.DrmProgresses.Add(progress);
            _unitOfWork.Complete();
        }
        public List<DisplayProgressViewModel> GetProgress(int drmId)
        {
            var progressList = (from drmProgress in _unitOfWork.DRM.Get(drmId).DrmProgresses
                                join progress in _unitOfWork.DRM.SelectListProgress() on drmProgress.DeliveryProgressId equals progress.Id
                                select new DisplayProgressViewModel
                                {
                                    Id = drmProgress.Id,
                                    Definition = progress.Definition,
                                    ProgressDate = drmProgress.DateCreated.Value
                                }).ToList();
            return progressList;
        }

        public DisplayProgressViewModel GetProgressById(int drmId,int progressId)
        {
            var progressInfo = (from drmProgress in _unitOfWork.DRM.Get(drmId).DrmProgresses.Where(m => m.Id == progressId)
                                join progress in _unitOfWork.DRM.SelectListProgress() on drmProgress.DeliveryProgressId equals progress.Id
                                select new DisplayProgressViewModel
                               {
                                   Id = (int)drmProgress.Id,
                                   Definition = progress.Definition,
                                   ProgressDate = drmProgress.DateCreated.Value,
                                   ProgressId =(int) drmProgress.DeliveryProgressId
                               }).FirstOrDefault();
            return progressInfo;
        }

        public string GetEstimatedDeliveryTime(int id)
        {
           
            var pickupId = 3;
            var isPickedUpByCourier = _unitOfWork.DRM.Get(id).DrmProgresses.Any(m => m.DeliveryProgressId == pickupId || m.DeliveryProgressId ==8 || m.DeliveryProgressId == 9);
            string estimatedDelivery = "---";
            if (isPickedUpByCourier)
            {
                var pickedUpDate = _unitOfWork.DRM.Get(id).DrmProgresses.Where(m => m.DeliveryProgressId == pickupId || m.DeliveryProgressId == 8 || m.DeliveryProgressId == 9).FirstOrDefault().DateCreated.Value;
                var courier = _unitOfWork.DRM.Get(id).DeliveryOption.EstimatedDelivery;

                var from = courier.RangeFrom;
                var to = courier.RangeTo;
                var unit = courier.UnitOfTime;

                if (unit == "hours")
                {
                    var datefrom = pickedUpDate.AddHours((double)from);
                    var dateTo = pickedUpDate.AddHours((double)to);
                    estimatedDelivery = datefrom.ToString("HH:mm") + " to " + dateTo.ToString("HH:mm");
                }
                else if (unit == "days")
                {
                    var datefrom = pickedUpDate.AddDays((double)from);
                    var dateTo = pickedUpDate.AddDays((double)to);
                    estimatedDelivery = datefrom.ToString("MM/dd/yyyy") + " to " + dateTo.ToString("MM/dd/yyyy");
                }
            }
            return estimatedDelivery;
        }

        public string IsDeliveredToClient(int drmId)
        {
           
            if (_unitOfWork.DRM.Get(drmId).DrmProgresses.Any(m => m.DeliveryProgressId == 7 && m.Confirmed == 1)){
                return "confirmed";
            } else if (_unitOfWork.DRM.Get(drmId).DrmProgresses.Any(m => m.DeliveryProgressId == 5)) {
                return "unconfirmed";
            } else { return null; };
            
        }
    }
}