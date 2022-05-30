using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WLTIDeliveryTool.Helper;
using DAL;
using WLTIDeliveryTool.Repository;
using HashidsNet;
using WLTIDeliveryTool.ViewModel;
using System.Linq.Dynamic.Core;
using WLTIDeliveryTool.Models;
using System.Data.Entity.Validation;

namespace WLTIDeliveryTool.Controllers
{
   
    public class AttachmentController : Controller
    {
        // GET: Attachment
        private readonly UnitOfWork _unitOfWork;
        private Hashids hashids;
        public ActionResult Index()
        {
            return View();
        }
        public AttachmentController()
        {
            _unitOfWork = new UnitOfWork(new WLTIDeliveryToolsEntities1());
            hashids = new Hashids("washink19", 12);
        }
        [MyAuthorize(Roles = "1,2")]
        [HttpPost]
        public JsonResult SaveAttachment(AttachmentViewModel attachment, string F)
        {
            try
            {
               
               
                    var fileSize = attachment.Attachment.ContentLength;
                var photo = new ImageHelper(attachment.Attachment);
                var length = attachment.Attachment.FileName.Length;
                if(length > 50)
                {
                    ModelState.AddModelError("Result", "Filename exceeded character maxlength of 50");
                }

                if (photo.Isvalid)
                {
                    var id = hashids.Decode(F)[0];
                    var drm = _unitOfWork.DRM.Get(id);


                    var img = new Attachment
                    {

                        Attachment1 = photo.ToByte(500, 500),
                        FileName = attachment.Attachment.FileName,
                        FileSize = fileSize,
                        FileType = attachment.Attachment.ContentType,
                        IsDeleted = 0,
                        AttachmentType = 1,
                        DispatchRequisitionMonitoring = drm

                    };

                    _unitOfWork.Attachments.Add(img);






                    _unitOfWork.Complete();
                    return Json(new { returnCode = 200, message = "success" });
                }
                else
                {
                    ModelState.AddModelError("Result", "Filename exceeded character maxlength of 50");
                    return Json(new { returnCode = 201, message = "file invalid" });
                }


            }
            //catch (Exception e)
            //{

            //    return Json(new { returnCode = 500, message = e.Message });
            //}
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }

        }

        public JsonResult DisplayAttachment(Nullable<int> id)
        {
            try
            {
               var attachmentId = id == null ? 0 :(int) id;
                
                var attachment = _unitOfWork.Attachments.Get(attachmentId);
                var model = new AttachmentViewModel
                {
                    FileName = attachment.FileName,
                    StrAttachment = attachment.Attachment1 == null ? null : Convert.ToBase64String(attachment.Attachment1),
                    FileType = attachment.FileType,
                    F = hashids.Encode(attachment.Id)
                };

                return Json(new { returnCode = 200, data = model ,message = "success" });
            }
            catch (Exception e)
            {
                return Json(new { returnCode = 201, message = e.Message });
            }
        }

        public JsonResult DisplayAttachments(string f)
        {
            try
            {

                var id = hashids.Decode(f)[0];

                int start = Convert.ToInt32(Request["start"]);
                int length = Convert.ToInt32(Request["length"]);
                string searchValue = Request["search[value]"];
                string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
                string sortDirection = Request["order[0][dir]"];


                var attachments = from a in _unitOfWork.DRM.Get(id).Attachments.Where(m=>m.IsDeleted == 0).ToList()
                                  select new {
                                      Attachment = a.Attachment1 == null ? "" : Convert.ToBase64String(a.Attachment1),
                                      FileType = a.FileType,
                                      FileName = a.FileName,
                                      FileSize = a.FileSize,
                                      Id = a.Id

                                  };

                int totalRows = attachments.Count();

               

                int totalRowsAfterFiltering = attachments.Count();

                attachments = attachments.AsQueryable().OrderBy(sortColumnName + " " + sortDirection).ToList();

               


                var jasonResult = Json(new
                {
                    data = attachments,
                    draw = Request["draw"],
                    recordsTotal = totalRows,
                    recordsFiltered = totalRowsAfterFiltering
                }, JsonRequestBehavior.AllowGet) ;
                jasonResult.MaxJsonLength = 50000000;

                return jasonResult;
            }
            catch (Exception e)
            {
                return Json(new { message = e.Message });
            }
        }
        [MyAuthorize(Roles = "1,2")]
        [HttpPost]
        public JsonResult Remove(string f)
        {
            try

            {
                var id = hashids.Decode(f)[0];
                var attachment = _unitOfWork.Attachments.Get(id);
                attachment.IsDeleted = 1;
               
                _unitOfWork.Attachments.Update(attachment);
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