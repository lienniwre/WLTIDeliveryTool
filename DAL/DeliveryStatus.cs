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
    
    public partial class DeliveryStatus
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public DeliveryStatus()
        {
            this.DispatchRequisitionMonitorings = new HashSet<DispatchRequisitionMonitoring>();
            this.DispatchRequisitionMonitorings1 = new HashSet<DispatchRequisitionMonitoring>();
        }
    
        public int Id { get; set; }
        public string Definition { get; set; }
        public string Code { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DispatchRequisitionMonitoring> DispatchRequisitionMonitorings { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DispatchRequisitionMonitoring> DispatchRequisitionMonitorings1 { get; set; }
    }
}
