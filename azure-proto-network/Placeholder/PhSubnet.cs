using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_network
{
    /// <summary>
    /// TODO: Subnet is a proxy resource, not a TrackedResource - need to adapt to using Resource as the base class
    /// </summary>
    public class PhSubnet : ChildResource<Subnet>
    {
        public PhSubnet(Subnet sub, string location) : base(sub.Id, sub)
        {
        }

        public override string Name => Model.Name;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
        public string Purpose => Model.Purpose;
        public IList<Delegation> Delegations
        {
            get => Model.Delegations;
            set => Model.Delegations = value;
        }
        public IList<ServiceAssociationLink> ServiceAssociationLinks => Model.ServiceAssociationLinks;
        public IList<ResourceNavigationLink> ResourceNavigationLinks => Model.ResourceNavigationLinks;
        public IList<SubResource> IpAllocations
        {
            get => Model.IpAllocations;
            set => Model.IpAllocations = value;
        }
        public IList<IPConfigurationProfile> IpConfigurationProfiles => Model.IpConfigurationProfiles;
        public IList<IPConfiguration> IpConfigurations => Model.IpConfigurations;
        public string PrivateEndpointNetworkPolicies
        {
            get => Model.PrivateEndpointNetworkPolicies;
            set => Model.PrivateEndpointNetworkPolicies = value;
        }
        public IList<PrivateEndpoint> PrivateEndpoints => Model.PrivateEndpoints;
        public IList<ServiceEndpointPropertiesFormat> ServiceEndpoints
        {
            get => Model.ServiceEndpoints;
            set => Model.ServiceEndpoints = value;
        }
        public SubResource NatGateway
        {
            get => Model.NatGateway;
            set => Model.NatGateway = value;
        }
        public RouteTable RouteTable
        {
            get => Model.RouteTable;
            set => Model.RouteTable = value;
        }
        public NetworkSecurityGroup NetworkSecurityGroup
        {
            get => Model.NetworkSecurityGroup;
            set => Model.NetworkSecurityGroup = value;
        }
        public IList<string> AddressPrefixes
        {
            get => Model.AddressPrefixes;
            set => Model.AddressPrefixes = value;
        }
        public string AddressPrefix
        {
            get => Model.AddressPrefix;
            set => Model.AddressPrefix = value;
        }
        public string Etag => Model.Etag;
        public IList<ServiceEndpointPolicy> ServiceEndpointPolicies
        {
            get => Model.ServiceEndpointPolicies;
            set => Model.ServiceEndpointPolicies = value;
        }
        public string PrivateLinkServiceNetworkPolicies
        {
            get => Model.PrivateLinkServiceNetworkPolicies;
            set => Model.PrivateLinkServiceNetworkPolicies = value;
        }
    }
}
