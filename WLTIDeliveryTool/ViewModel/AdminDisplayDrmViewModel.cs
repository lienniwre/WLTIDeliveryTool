using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.ViewModel;
using System.ComponentModel.DataAnnotations;
using DAL;
namespace WLTIDeliveryTool.ViewModel
{
    public class AdminDisplayDrmViewModel
    {
        public DisplayDrmViewModel Details { get; set; }
       
        public DisplayProgressViewModel Progress { get; set; }
        public AttachmentViewModel Attachment { get; set; }
    }
}