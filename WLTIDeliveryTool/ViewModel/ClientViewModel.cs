using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WLTIDeliveryTool.ViewModel
{
    public class ClientViewModel
    {
        [Required(ErrorMessage = "")]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
    }
}