using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Interface;
using DAL;
namespace WLTIDeliveryTool.Repository
{
    public class DrmProgressRepository:Repository<DrmProgress>,IDrmProgressRepository
    {
        public DrmProgressRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }
    }
}