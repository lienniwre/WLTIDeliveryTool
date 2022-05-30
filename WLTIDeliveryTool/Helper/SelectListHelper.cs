using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL;
using WLTIDeliveryTool.Repository;

namespace WLTIDeliveryTool.Helper
{
    public static class SelectListHelper
    {
        private static TextInfo _textInfo = new CultureInfo("en-Us", false).TextInfo;
        public static readonly UnitOfWork _unitofwork;
        static SelectListHelper()
        {
            _unitofwork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        public static IEnumerable<SelectListItem> DeliveryStatus()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select Status", Value = "", Selected = true });
            var status = _unitofwork.DeliveryStatus.GetAll();

            foreach (var item in status)
            {


                item.Definition = item.Definition == null ? "" : _textInfo.ToTitleCase(item.Definition);
                var listItem = new SelectListItem { Text = item.Definition==null?"":item.Definition, Value = item.Id.ToString() };
                list.Add(listItem);

            }
            return list;

        }

        public static IEnumerable<SelectListItem> ModeOfDelivery()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select Mode of Delivery", Value = "", Selected = true });
            var mod = _unitofwork.ModeOfDelivery.GetAll().OrderBy(m=>m.ModId);

            foreach (var item in mod)
            {


                item.Definition = _textInfo.ToTitleCase(item.Definition);
                var listItem = new SelectListItem { Text = item.Definition, Value = item.ModId.ToString() };
                list.Add(listItem);

            }
            return list;

        }

        public static IEnumerable<SelectListItem> CSE()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select Chanel Sales Executive", Value = "", Selected = true });
            var cseList = _unitofwork.CSE.GetAll();

            foreach (var item in cseList)
            {


               var firstName = _textInfo.ToTitleCase(item.FirstName);
               var lastName = _textInfo.ToTitleCase(item.LastName);
                var listItem = new SelectListItem { Text = firstName +" "+ lastName, Value = item.CSEID.ToString() };
                list.Add(listItem);

            }
            return list;

        }
        public static IEnumerable<SelectListItem> Client()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select Client Name", Value = "", Selected = true });
            var clientList = _unitofwork.Client.GetAll().OrderBy(m=>m.ClientName);

            foreach (var item in clientList)
            {


               
                var listItem = new SelectListItem { Text = item.ClientName , Value = item.ClientId.ToString() };
                list.Add(listItem);

            }
            return list;

        }
        public static IEnumerable<SelectListItem> DeliveryOption(int modId)
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select an option", Value = "", Selected = true });
            var optionlist = _unitofwork.ModeOfDelivery.GetDeliveryOptions(modId);

            foreach (var item in optionlist)
            {



                var listItem = new SelectListItem { Text = item.Definition, Value = item.DeliveryOptionId.ToString() };
                list.Add(listItem);

            }
            return list;

        }
        public static IEnumerable<SelectListItem> DeliveryProgress()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select", Value = "", Selected = true });
            var progressList = _unitofwork.DRM.SelectListProgress().OrderBy(m => m.Definition);

            foreach (var item in progressList)
            {



                var listItem = new SelectListItem { Text = item.Definition, Value = item.Id.ToString() };
                list.Add(listItem);

            }
            return list;

        }
        public static IEnumerable<SelectListItem> EstimatedDelivery()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select", Value = "", Selected = true });
            var estimatedDeliveryList = _unitofwork.EstimatedDelivery.GetAll().OrderBy(m => m.Id);

            foreach (var item in estimatedDeliveryList)
            {



                var listItem = new SelectListItem { Text = item.RangeFrom.ToString() + "-" + item.RangeTo.ToString() +" "+ item.UnitOfTime, Value = item.Id.ToString() };
                list.Add(listItem);

            }
            return list;

        }

        public static IEnumerable<SelectListItem> UnitOfTime()
        {
            var list = new List<SelectListItem>();
            list.Add(new SelectListItem { Text = "Please select", Value = "", Selected = true });
            list.Add(new SelectListItem { Text = "hours", Value = "hours" });
            list.Add(new SelectListItem { Text = "days", Value = "days" });
            list.Add(new SelectListItem { Text = "months", Value = "months" });

            return list;

        }


    }
}