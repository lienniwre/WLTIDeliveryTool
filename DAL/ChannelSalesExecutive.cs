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
    
    public partial class ChannelSalesExecutive
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ChannelSalesExecutive()
        {
            this.DispatchRequisitionMonitorings = new HashSet<DispatchRequisitionMonitoring>();
        }
    
        public int CSEID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Nullable<int> CreatedById { get; set; }
        public Nullable<System.DateTime> DateCreated { get; set; }
        public Nullable<int> IsDeleted { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DispatchRequisitionMonitoring> DispatchRequisitionMonitorings { get; set; }
    }
}
