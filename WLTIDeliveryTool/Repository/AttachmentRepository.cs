using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Interface;
namespace WLTIDeliveryTool.Repository
{
    public class AttachmentRepository:Repository<Attachment>,IAttachmentRepository
    {
        public AttachmentRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }
        public void Update(Attachment attachment)
        {
            WLTIDeliveryToolsEntities1.Attachments.Attach(attachment);
            var entry = WLTIDeliveryToolsEntities1.Entry(attachment);
            entry.State = EntityState.Modified;
        }
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }
    }
}