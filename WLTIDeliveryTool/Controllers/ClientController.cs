using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.Repository;
using DAL;
using WLTIDeliveryTool.Models;
using System.Linq.Dynamic.Core;
namespace WLTIDeliveryTool.Controllers
{
    [MyAuthorize(Roles = "1,2")]
    public class ClientController : Controller
    {
        public readonly UnitOfWork _unitOfWork;
        // GET: Client

            public ClientController()
        {
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Create()
        {
            

            return View();
        }

        [HttpPost]
        public JsonResult SaveClient(Client model, string f)
        {
            try
            {

               

                var client = new ClientModel();
                client.Add(model);

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult SaveChangesToClient(Client model)
        {
            try
            {
              
                var client = _unitOfWork.Client.Get(model.ClientId);

               
                client.ClientName = model.ClientName;



                var clientModel = new ClientModel();
                clientModel.Update(client);

                return Json(new { returnCode = 200, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { returnCode = 201, message = e.Message });
            }
        }
        [HttpPost]
        public JsonResult GetClient(int id)
        {
            try
            {
                var model = new ClientModel();
                var client = model.Get(id);
                return Json(new { ClientName = client.ClientName,ClientId = client.ClientId, message = "success" });
            }
            catch (Exception e)
            {

                return Json(new { message = e.Message });
            }
        }

        public JsonResult DisplayClient()
        {
            int start = Convert.ToInt32(Request["start"]);
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];

            var client = new ClientModel();
            var list = client.GetAll();
            int totalRows = list.Count();

            if (!string.IsNullOrEmpty(searchValue))
            {
                list = list.Where(x => x.ClientName.ToLower().Contains(searchValue.ToLower())).ToList();
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
    }
}