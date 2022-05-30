using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WLTIDeliveryTool.Interface;

namespace WLTIDeliveryTool.Repository
{
    public class UserRepository:Repository<User>,IUserRepository
    {
        public UserRepository(WLTIDeliveryToolsEntities1 context) : base(context)
        {

        }

        public void Update(User user)
        {
            WLTIDeliveryToolsEntities1.Users.Attach(user);
            var entry = WLTIDeliveryToolsEntities1.Entry(user);
            entry.State = System.Data.Entity.EntityState.Modified;
        }
        public WLTIDeliveryToolsEntities1 WLTIDeliveryToolsEntities1
        {
            get { return Context as WLTIDeliveryToolsEntities1; }
        }
    }
}