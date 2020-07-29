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
            Model = rg;
        }

        public override string Name => Model.Name;
        public ResourceGroupProperties Properties => Model.Properties;
        public string ManagedBy { get { return Model.ManagedBy; } set { Model.ManagedBy = value; } }
    }
}
