using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using System.ComponentModel.DataAnnotations;
namespace WLTIDeliveryTool.ViewModel
{
    public class ChangePasswordViewModel
    {
        [Required(ErrorMessage ="This field is required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "New Password")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Compare("NewPassword")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        

    }
}