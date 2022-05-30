using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
namespace WLTIDeliveryTool.ViewModel
{
    public class UpdateDRMViewModel
    {

        [Required(ErrorMessage = "This field is required")]
        public int DeliveryReceiptNumber { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int CseId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int ModId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public Nullable<int> DeliveryOptionId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int ConfirmedShipment { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Invoicing { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int Releasing { get; set; }


        public Nullable<DateTime> DateReceivedByClient { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public DateTime DrmDate { get; set; }
        public string HashId { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public string WayBillNo { get; set; }
        [Required(ErrorMessage = "This field is required")]
        public int QuantityOfParcel { get; set; }
    }
}