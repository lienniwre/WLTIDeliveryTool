﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;

namespace WLTIDeliveryTool.Interface
{
  public interface ICSERepository:IRepository<ChannelSalesExecutive>
    {
        void Update(ChannelSalesExecutive client);
    }
}
