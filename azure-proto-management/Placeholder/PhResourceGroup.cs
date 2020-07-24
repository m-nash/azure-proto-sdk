using azure_proto_core;
using Azure.ResourceManager.Resources.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    public class PhResourceGroup : AzureResource<ResourceGroup>, IManagedByResource
    {
        
        public override ResourceGroup Data { get; protected set; }

        public PhResourceGroup(ResourceGroup rg) : base(rg.Id, rg.Location)
        {
            Data = rg;
        }

        public ResourceGroupProperties Properties => Data.Properties;
        public string ManagedBy { get { return Data.ManagedBy; } set { Data.ManagedBy = value; } }
    }
}
