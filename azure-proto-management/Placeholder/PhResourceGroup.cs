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
            if (null == rg.Tags)
            {
                rg.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public override IDictionary<string, string> Tags => Model.Tags;
        public override string Name => Model.Name;
        public ResourceGroupProperties Properties
        {
            get => Model.Properties;
            set => Model.Properties = value;
        }
        public string ManagedBy
        {
            get => Model.ManagedBy;
            set => Model.ManagedBy = value;
        }
    }
}
