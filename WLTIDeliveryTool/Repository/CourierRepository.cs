using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Interface;
namespace WLTIDeliveryTool.Repository
{
    public class CourierRepository:Repository<DeliveryOption>,ICourierRepository
    {
        public CourierRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }

        public void Update(DeliveryOption courier)
        {
            WLTIDeliveryToolsEntities1.DeliveryOptions.Attach(courier);
            var entry = WLTIDeliveryToolsEntities1.Entry(courier);
            entry.State = System.Data.Entity.EntityState.Modified;
        }

        WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
           get { return Context as WLTIDeliveryToolsEntities1; }
        }
    }
}