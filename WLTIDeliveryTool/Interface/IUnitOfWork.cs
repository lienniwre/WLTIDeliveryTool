using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WLTIDeliveryTool.Interface
{
    interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IModeOfDeliveryRepository ModeOfDelivery { get; }
        IDeliveryStatusRepository DeliveryStatus { get; }
        ICSERepository CSE { get; }
        IClientRepository Client { get; }
        IDrmRepository DRM { get; }
        IDeliveryProgressRepository DeliveryProgress { get; }
        IDrmProgressRepository DrmProgress { get; }
        ICourierRepository Courier { get; }
        IEstimatedDeliveryRepository EstimatedDelivery { get; }
        IAttachmentRepository Attachments { get; }
    }
}
