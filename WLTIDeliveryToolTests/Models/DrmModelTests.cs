using Microsoft.VisualStudio.TestTools.UnitTesting;
using WLTIDeliveryTool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLTIDeliveryTool.Repository;
using DAL;
using WLTIDeliveryTool.Models;

namespace WLTIDeliveryTool.Models.Tests
{
    [TestClass()]
    public class DrmModelTests
    {
        public readonly UnitOfWork _unitofwork;
        public DrmModelTests()
        {
            this._unitofwork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        [TestMethod()]
        public void GetListTest()
        {
            var drm = new DrmModel();
            var list = drm.GetList();
            var a = list.Count;
            Assert.Fail();
        }

        [TestMethod()]
        public void DisplayDetailsTest()
        {
            var estimated = _unitofwork.DRM.Get(1).DeliveryOption.EstimatedDelivery.RangeFrom;
            var client = _unitofwork.DRM.Get(1).Client.ClientName;

            Assert.AreEqual(1 , estimated);
        }
        [TestMethod()]
        public void DisplayEstimatedDeliveryTime()
        {
            var id = 3;
            var pickupId = 3;
            var isPickedUpByCourier = _unitofwork.DRM.Get(id).DrmProgresses.Any(m => m.DeliveryProgressId == pickupId);
            string estimatedDelivery = "---";
            if (isPickedUpByCourier)
            {
                var pickedUpDate = _unitofwork.DRM.Get(id).DrmProgresses.Where(m => m.DeliveryProgressId == pickupId).FirstOrDefault().DateCreated.Value;
                var courier = _unitofwork.DRM.Get(id).DeliveryOption.EstimatedDelivery;

                var from = courier.RangeFrom;
                var to = courier.RangeTo;
                var unit = courier.UnitOfTime;

                if(unit == "hours")
                {
                    var datefrom = pickedUpDate.AddHours((double) from);
                    var dateTo = pickedUpDate.AddHours((double)to);
                    estimatedDelivery = datefrom.ToString("HH:mm") + " to " + dateTo.ToString("HH:mm");
                }else if(unit == "days")
                {
                    var datefrom = pickedUpDate.AddDays((double)from);
                    var dateTo = pickedUpDate.AddDays((double)to);
                    estimatedDelivery = datefrom.ToString("MM/dd/yyyy") + " to " + dateTo.ToString("MM/dd/yyyy");
                }
            }

            Assert.AreEqual("", estimatedDelivery);
        }
    }
}