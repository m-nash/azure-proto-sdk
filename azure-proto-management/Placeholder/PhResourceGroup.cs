using azure_proto_core;
using Microsoft.Azure.Management.ResourceManager.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    public class PhResourceGroup : ResourceGroup, IModel
    {
        public ResourceGroup Data { get; private set; }

        public PhResourceGroup(ResourceGroup rg)
        {
            Data = rg;
        }

        new public string Id => Data.Id;
        new public string Name => Data.Name;
        new public string Type => Data.Type;
        new public ResourceGroupProperties Properties => Data.Properties;
        new public string Location => Data.Location;
        new public string ManagedBy => Data.ManagedBy;
        new public IDictionary<string, string> Tags => Data.Tags;

        object IModel.Data => Data;
    }
}
