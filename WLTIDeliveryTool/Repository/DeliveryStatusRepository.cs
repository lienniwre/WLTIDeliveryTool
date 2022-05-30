using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Interface;

namespace WLTIDeliveryTool.Repository
{
    public class DeliveryStatusRepository:Repository<DeliveryStatus>,IDeliveryStatusRepository
    {
        public DeliveryStatusRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }
    }
}