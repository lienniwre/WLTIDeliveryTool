using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using CompareAttribute = System.ComponentModel.DataAnnotations.CompareAttribute;

namespace WLTIDeliveryTool.ViewModel
{
    public class CreateUserViewModel
    {
        [Required(ErrorMessage="This field is required")]
        [Remote("IsAlreadyUsed", "Account", HttpMethod = "POST", ErrorMessage = "username already used.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        [Compare("Password")]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Role { get; set; }
    }
}