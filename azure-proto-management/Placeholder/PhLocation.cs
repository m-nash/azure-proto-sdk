using azure_proto_core;
using Microsoft.Azure.Management.Subscription.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_management
{
    public class PhLocation : Location, IModel
    {
        public Location Data { get; private set; }

        public PhLocation(Location loc)
        {
            Data = loc;
        }

        new public string Id => Data.Id;
        new public string SubscriptionId => Data.SubscriptionId;
        new public string Name => Data.Name;
        new public string DisplayName => Data.DisplayName;
        new public string Latitude => Data.Latitude;
        new public string Longitude => Data.Longitude;

        object IModel.Data => Data;
    }
}
