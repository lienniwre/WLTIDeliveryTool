using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Interface;
using DAL;
namespace WLTIDeliveryTool.Repository
{
    public class EstimatedDeliveryRepository:Repository<EstimatedDelivery>,IEstimatedDeliveryRepository
    {
        public EstimatedDeliveryRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }
        
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }

        public void Update(EstimatedDelivery edt)
        {
            WLTIDeliveryToolsEntities1.EstimatedDeliveries.Attach(edt);
            var entry = WLTIDeliveryToolsEntities1.Entry(edt);
            entry.State = System.Data.Entity.EntityState.Modified;
        }

    }
}