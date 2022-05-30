using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Interface;
namespace WLTIDeliveryTool.Repository
{
    public class DrmRepository:Repository<DispatchRequisitionMonitoring>,IDrmRepository
    {
        public DrmRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }
      

        public void Update(DispatchRequisitionMonitoring drm)
        {
            WLTIDeliveryToolsEntities1.DispatchRequisitionMonitorings.Attach(drm);
            var entry = WLTIDeliveryToolsEntities1.Entry(drm);
            entry.State = EntityState.Modified;
        }

        public void UpdateProgress(DrmProgress drmProgress)
        {
            WLTIDeliveryToolsEntities1.DrmProgresses.Attach(drmProgress);
            var entry = WLTIDeliveryToolsEntities1.Entry(drmProgress);
            entry.State = EntityState.Modified;
        }


        public List<DeliveryProgress> SelectListProgress()
        {
            return WLTIDeliveryToolsEntities1.DeliveryProgresses.ToList();
        }

       


        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }

        
    }
}