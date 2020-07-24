using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhVirtualNetwork : AzureResource<VirtualNetwork>
    {
        public override VirtualNetwork Data { get; protected set; }

        public PhVirtualNetwork(VirtualNetwork vnet) : base(vnet.Id, vnet.Location)
        {
            Data = vnet;
        }

        new public IDictionary<string, string> Tags => Data.Tags;

        public string Etag => Data.Etag;
        public AddressSpace AddressSpace => Data.AddressSpace;
        public DhcpOptions DhcpOptions => Data.DhcpOptions;
        public IList<Subnet> Subnets => Data.Subnets;
        public IList<VirtualNetworkPeering> VirtualNetworkPeerings => Data.VirtualNetworkPeerings;
        public string ResourceGuid => Data.ResourceGuid;
        public ProvisioningState? ProvisioningState => Data.ProvisioningState;
        public bool? EnableDdosProtection => Data.EnableDdosProtection;
        public bool? EnableVmProtection => Data.EnableVmProtection;
        public SubResource DdosProtectionPlan => Data.DdosProtectionPlan;
        public VirtualNetworkBgpCommunities BgpCommunities => Data.BgpCommunities;
        public IList<SubResource> IpAllocations => Data.IpAllocations;
    }
}
