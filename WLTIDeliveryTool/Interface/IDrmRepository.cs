using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace WLTIDeliveryTool.Interface
{
   public interface IDrmRepository:IRepository<DispatchRequisitionMonitoring>
    {
        void Update(DispatchRequisitionMonitoring drm);
        void UpdateProgress(DrmProgress drmProgress);
        List<DeliveryProgress> SelectListProgress();
    }
}
