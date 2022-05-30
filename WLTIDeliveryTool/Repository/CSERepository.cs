using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using DAL;
using WLTIDeliveryTool.Interface;
namespace WLTIDeliveryTool.Repository
{
    public class CSERepository:Repository<ChannelSalesExecutive>,ICSERepository
    {
        public CSERepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }

        public void Update(ChannelSalesExecutive cse)
        {
            WLTIDeliveryToolsEntities1.ChannelSalesExecutives.Attach(cse);
             var entry = WLTIDeliveryToolsEntities1.Entry(cse);
            entry.State = EntityState.Modified;
        }
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }

       
    }
}