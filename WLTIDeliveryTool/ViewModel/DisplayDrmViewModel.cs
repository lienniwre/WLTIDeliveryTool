using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLTIDeliveryTool.ViewModel
{
    public class DisplayDrmViewModel
    {
        public DateTime DrmDate { get; set; }
        public int DrNumber { get; set; }
        public string ClientName { get; set; }
        public string RequestedBy { get; set; }
        public string ModeOfDelivery { get; set; }
        public string ConfirmedShipment { get; set; }
        public string Invoicing { get; set; }
        public string Releasing { get; set; }
        public string HashId { get; set; }
        public Nullable<DateTime> DateReceivedByClient { get; set; }
        public Nullable<int> DeliveryOption { get; set; }
        public int ModId { get; set; }
        public string Courier { get; set; }
        public string Edt { get; set; }
        public string WayBillNo { get; set; }
        public string QuantityOfParcel { get; set; }
        public AttachmentViewModel Attachment { get; set; }
        public string IsDelivered { get; set; }
    }
}