using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;


namespace WLTIDeliveryTool.Interface
{
   public  interface IModeOfDeliveryRepository: IRepository<ModeOfDelivery>
    {
        IEnumerable<DeliveryOption> GetDeliveryOptions(int modId);
        void Update(ModeOfDelivery mod);
    }
}
