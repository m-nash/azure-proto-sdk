using azure_proto_core;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    public class PhResourceGroup : TrackedResource<ResourceGroup>, IManagedByResource
    {

        public PhResourceGroup(ResourceGroup rg) : base(rg.Id, rg.Location, rg)
        {
            Data = rg;
        }

        public ResourceGroupProperties Properties => Data.Properties;
        public string ManagedBy { get { return Data.ManagedBy; } set { Data.ManagedBy = value; } }
    }
}
