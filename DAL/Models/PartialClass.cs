using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
   
    [MetadataType(typeof(ClientMetadata))]
    public partial class Client
    {
        
    }
    [MetadataType(typeof(CseMetadata))]
    public partial class ChannelSalesExecutive
    {

    }
    [MetadataType(typeof(ProgressMetadata))]
    public partial class DeliveryProgress
    {

    }
    [MetadataType(typeof(ModeOfDeliveryMetadata))]
    public partial class ModeOfDelivery { }
    [MetadataType(typeof(CourierMetadata))]
    public partial class DeliveryOption { }
    [MetadataType(typeof(EstimatedDeliveryMetadata))]
    public partial class EstimatedDelivery { }
    [MetadataType(typeof(AttachmentMetadata))]
    public partial class Attachment { }
    [MetadataType(typeof(UserMetadata))]
    public partial class User { }
}
