using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WLTIDeliveryTool.ViewModel
{
    public class DisplayProgressViewModel
    {
        public int Id { get; set; }
        public string Definition { get; set; }
        [Display(Name = "Date"), DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [Required(ErrorMessage="This field is required")]
        public DateTime ProgressDate { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int ProgressId { get; set; }
        public int Confirmed { get; set; }
    }
}