using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WLTIDeliveryTool.Repository;
using DAL;
namespace WLTIDeliveryTool.Interface
{
    public interface IDeliveryProgressRepository:IRepository<DeliveryProgress>
    {
        void Update(DeliveryProgress progress);
    }
}
