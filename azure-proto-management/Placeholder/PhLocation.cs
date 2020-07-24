using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    public class PhLocation : AzureResource<Azure.ResourceManager.Resources.Models.Location>
    {
        
        public PhLocation(Azure.ResourceManager.Resources.Models.Location loc) : base(loc.Id, loc.Name)
        {
            Data = loc;
        }

        public string SubscriptionId => Data.SubscriptionId;
        public string DisplayName => Data.DisplayName;
        public string Latitude => Data.Metadata.Latitude;
        public string Longitude => Data.Metadata.Longitude;


        public override Azure.ResourceManager.Resources.Models.Location Data { get; protected set; }
    }
}
