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
    
    public partial class Attachment
    {
        public int Id { get; set; }
        public byte[] Attachment1 { get; set; }
        public string FileType { get; set; }
        public string FileName { get; set; }
        public Nullable<int> FileSize { get; set; }
        public Nullable<int> IsDeleted { get; set; }
        public Nullable<int> AttachmentType { get; set; }
        public Nullable<int> DrmId { get; set; }
    
        public virtual DispatchRequisitionMonitoring DispatchRequisitionMonitoring { get; set; }
    }
}
