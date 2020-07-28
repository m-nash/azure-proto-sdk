using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhVirtualNetwork : TrackedResource<VirtualNetwork>
    {
        public PhVirtualNetwork(VirtualNetwork vnet) : base(vnet.Id, vnet.Location, vnet)
        {
            Model = vnet;
        }

        new public IDictionary<string, string> Tags => Model.Tags;

        public string Etag => Model.Etag;
        public AddressSpace AddressSpace => Model.AddressSpace;
        public DhcpOptions DhcpOptions => Model.DhcpOptions;
        public IList<Subnet> Subnets => Model.Subnets;
        public IList<VirtualNetworkPeering> VirtualNetworkPeerings => Model.VirtualNetworkPeerings;
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
        public bool? EnableDdosProtection => Model.EnableDdosProtection;
        public bool? EnableVmProtection => Model.EnableVmProtection;
        public SubResource DdosProtectionPlan => Model.DdosProtectionPlan;
        public VirtualNetworkBgpCommunities BgpCommunities => Model.BgpCommunities;
        public IList<SubResource> IpAllocations => Model.IpAllocations;
    }
}
