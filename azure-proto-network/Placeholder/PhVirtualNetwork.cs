using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhVirtualNetwork : VirtualNetwork, IModel
    {
        public VirtualNetwork Data { get; private set; }

        public PhVirtualNetwork(VirtualNetwork vnet)
        {
            Data = vnet;
        }

        new public string Name => Data.Name;
        new public string Id => Data.Id;
        new public string Type => Data.Type;
        new public string Location => Data.Location;
        new public IDictionary<string, string> Tags => Data.Tags;

        new public string Etag => Data.Etag;
        new public AddressSpace AddressSpace => Data.AddressSpace;
        new public DhcpOptions DhcpOptions => Data.DhcpOptions;
        new public IList<Subnet> Subnets => Data.Subnets;
        new public IList<VirtualNetworkPeering> VirtualNetworkPeerings => Data.VirtualNetworkPeerings;
        new public string ResourceGuid => Data.ResourceGuid;
        new public ProvisioningState? ProvisioningState => Data.ProvisioningState;
        new public bool? EnableDdosProtection => Data.EnableDdosProtection;
        new public bool? EnableVmProtection => Data.EnableVmProtection;
        new public SubResource DdosProtectionPlan => Data.DdosProtectionPlan;
        new public VirtualNetworkBgpCommunities BgpCommunities => Data.BgpCommunities;
        new public IList<SubResource> IpAllocations => Data.IpAllocations;

        object IModel.Data => Data;
    }
}
