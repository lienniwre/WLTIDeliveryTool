//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DAL
{
    using System;
    using System.Collections.Generic;
    
    public partial class DispatchRequisitionMonitoring
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DispatchRequisitionMonitoring()
        {
            this.Attachments = new HashSet<Attachment>();
            this.DrmProgresses = new HashSet<DrmProgress>();
        }
    
        public int DrmId { get; set; }
        public Nullable<System.DateTime> DrmDate { get; set; }
        public Nullable<int> DrNumber { get; set; }
        public Nullable<int> ClientId { get; set; }
        public Nullable<int> CseId { get; set; }
        public Nullable<int> ModId { get; set; }
        public Nullable<int> ModOptionId { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> DateReceived { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> ConfirmedShipment { get; set; }
        public Nullable<int> Releasing { get; set; }
        public string WaybillNo { get; set; }
        public Nullable<int> QuantityOfParcel { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Attachment> Attachments { get; set; }
        public virtual ChannelSalesExecutive ChannelSalesExecutive { get; set; }
        public virtual Client Client { get; set; }
        public virtual DeliveryOption DeliveryOption { get; set; }
        public virtual DeliveryStatus DeliveryStatus { get; set; }
        public virtual DeliveryStatus DeliveryStatus1 { get; set; }
        public virtual ModeOfDelivery ModeOfDelivery { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DrmProgress> DrmProgresses { get; set; }
    }
}
