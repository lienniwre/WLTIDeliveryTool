using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WLTIDeliveryTool.Models
{
    public class Credential
    {
        public string Password { get; set; }
        public string Salt { get; set; }
    }
}