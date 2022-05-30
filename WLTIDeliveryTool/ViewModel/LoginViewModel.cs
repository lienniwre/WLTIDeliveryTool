using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
namespace WLTIDeliveryTool.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage="This field is required.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field is required.")]
        public string Password { get; set; }
    }
}