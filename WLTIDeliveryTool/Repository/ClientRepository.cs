using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Interface;
using DAL;
using System.Data.Entity;

namespace WLTIDeliveryTool.Repository
{
    public class ClientRepository:Repository<Client>,IClientRepository
    {
        public ClientRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }
        public void Update(Client client)
        {
            WLTIDeliveryToolsEntities1.Clients.Attach(client);
            var entry = WLTIDeliveryToolsEntities1.Entry(client);
            entry.State = EntityState.Modified;
        }
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }
    }
}