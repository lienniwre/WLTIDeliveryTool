using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Interface;
using DAL;
using System.Data.Entity;
namespace WLTIDeliveryTool.Repository
{
    public class DeliveryProgressRepository:Repository<DeliveryProgress>,IDeliveryProgressRepository
    {
        public DeliveryProgressRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }

        public void Update(DeliveryProgress progress)
        {
            WLTIDeliveryToolsEntities1.DeliveryProgresses.Attach(progress);
            var entry = WLTIDeliveryToolsEntities1.Entry(progress);
            entry.State = EntityState.Modified;
        }
    }
}