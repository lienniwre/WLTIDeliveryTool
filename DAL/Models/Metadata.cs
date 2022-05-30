using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.Web.Script.Serialization;
using System.Web;
namespace DAL
{
    


    public class ClientMetadata {
        [Required(ErrorMessage = "This field is required")]
        public string ClientName { get; set; }
    }

    public class CseMetadata
    {
        [Required(ErrorMessage = "This field is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string LastName { get; set; }
    }

    public class ProgressMetadata{
        [Required(ErrorMessage = "This field is required")]
        public string Definition { get; set; }
    }
    
    public class ModeOfDeliveryMetadata
    {
        [Required(ErrorMessage = "This field is required")]
        public string Definition { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int ModId { get; set; }
    }

    public class CourierMetadata
    {
        [Required(ErrorMessage = "This field is required")]
        public int ModId { get; set; }
        
        public int DeliveryOptionId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Definition { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string EstimatedDeliveryTime { get; set; }
    }

    public class EstimatedDeliveryMetadata
    {
        [Required(ErrorMessage = "This field is required")]
        public int RangeFrom { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int RangeTo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string UnitOfTime { get; set; }

    }
    public class AttachmentMetadata
    {
        [Required(ErrorMessage = "This field is required")]
        public HttpPostedFileBase Attachment1 { get; set; }
    }
    public class UserMetadata
    {
        [Required(ErrorMessage = "This field is required")]
        public string UserName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string Password { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int UserRole{ get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Status { get; set; }

    }
}
