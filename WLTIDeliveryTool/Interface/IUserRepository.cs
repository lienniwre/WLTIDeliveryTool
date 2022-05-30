using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLTIDeliveryTool.Interface
{
    public interface IUserRepository : IRepository<User>
    {
        void Update(User user);
    }
}
