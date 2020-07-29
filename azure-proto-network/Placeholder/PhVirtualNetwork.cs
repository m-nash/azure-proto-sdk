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
            if (null == vnet.Tags)
            {
                vnet.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public override IDictionary<string, string> Tags => Model.Tags;
        public override string Name => Model.Name;
        public string Etag => Model.Etag;
        public AddressSpace AddressSpace
        {
            get => Model.AddressSpace;
            set => Model.AddressSpace = value;
        }
        public DhcpOptions DhcpOptions
        {
            get => Model.DhcpOptions;
            set => Model.DhcpOptions = value;
        }
        public IList<Subnet> Subnets
        {
            get => Model.Subnets;
            set => Model.Subnets = value;
        }
        public IList<VirtualNetworkPeering> VirtualNetworkPeerings
        {
            get => Model.VirtualNetworkPeerings;
            set => Model.VirtualNetworkPeerings = value;
        }
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
        public bool? EnableDdosProtection
        {
            get => Model.EnableDdosProtection;
            set => Model.EnableDdosProtection = value;
        }
        public bool? EnableVmProtection
        {
            get => Model.EnableVmProtection;
            set => Model.EnableVmProtection = value;
        }
        public SubResource DdosProtectionPlan

        {
            get => Model.DdosProtectionPlan;
            set => Model.DdosProtectionPlan = value;
        }
        public VirtualNetworkBgpCommunities BgpCommunities
        {
            get => Model.BgpCommunities;
            set => Model.BgpCommunities = value;
        }
        public IList<SubResource> IpAllocations
        {
            get => Model.IpAllocations;
            set => Model.IpAllocations = value;
        }
    }
}
