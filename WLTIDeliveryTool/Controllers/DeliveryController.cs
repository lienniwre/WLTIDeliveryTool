using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.Models;

namespace WLTIDeliveryTool.Controllers
{
    [MyAuthorize(Roles = "1,2")]
    public class DeliveryController : Controller
    {
        // GET: Delivery
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }

        public ActionResult DeliveryDetails()
        {
            return View();
        }
        public ActionResult TrackingDetails()
        {
            return View();
        }
    }
}