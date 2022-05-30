using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Interface;

namespace WLTIDeliveryTool.Repository
{
    public class ModeOfDeliveryRepository:Repository<ModeOfDelivery>,IModeOfDeliveryRepository
    {
        
        public ModeOfDeliveryRepository(WLTIDeliveryToolsEntities1 context):base(context)
        {

        }

        public IEnumerable<DeliveryOption> GetDeliveryOptions(int modId)
        {
            var deliveryOptions = WLTIDeliveryToolsEntities1.DeliveryOptions.Where(m => m.ModId == modId).ToList();
            return deliveryOptions;
        }

        public void Update(ModeOfDelivery mod)
        {
            WLTIDeliveryToolsEntities1.ModeOfDeliveries.Attach(mod);
            var entry = WLTIDeliveryToolsEntities1.Entry(mod);
            entry.State = EntityState.Modified;
        }

        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }
    }
}