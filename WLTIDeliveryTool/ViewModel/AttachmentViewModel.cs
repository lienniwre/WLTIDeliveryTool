using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WLTIDeliveryTool.ViewModel
{
    public class AttachmentViewModel
    {
        [Required(ErrorMessage = "This field is required")]
       
        public HttpPostedFileBase Attachment { get; set; }
        [MaxLength(50,ErrorMessage = "The file name must be a string with a maximum length of '50")]
        public string FileName { get; set; }
        public int FileSize { get; set; }
        public string StrAttachment { get; set; }
        public string FileType { get; set; }
        public string F { get; set; }
    }
}