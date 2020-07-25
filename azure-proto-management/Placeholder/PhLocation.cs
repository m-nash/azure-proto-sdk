using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    // Update the signature for new library
    public class PhLocation : TrackedResource<Azure.ResourceManager.Resources.Models.Location>
    {
        
        public PhLocation(Azure.ResourceManager.Resources.Models.Location loc) : base(loc.Id, loc.Name, loc)
        {
        }

        public string SubscriptionId => Data.SubscriptionId;
        public string DisplayName => Data.DisplayName;
        public string Latitude => Data.Metadata.Latitude;
        public string Longitude => Data.Metadata.Longitude;
    }
}
