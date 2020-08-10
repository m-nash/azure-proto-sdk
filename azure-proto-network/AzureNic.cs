﻿using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureNic : AzureOperations<PhNetworkInterface>
    {
        public AzureNic(TrackedResource resourceGroup, PhNetworkInterface nic) : base(resourceGroup, nic) 
        { 
        }

    }
}
