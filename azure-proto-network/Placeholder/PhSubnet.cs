using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhSubnet : TrackedResource<Subnet>
    {
        public PhSubnet(Subnet sub, string location) : base(sub.Id, location, sub)
        {
            Model = sub;
            Location = location;
        }


        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
        public string Purpose => Model.Purpose;
        public IList<Delegation> Delegations => Model.Delegations;
        public IList<ServiceAssociationLink> ServiceAssociationLinks => Model.ServiceAssociationLinks;
        public IList<ResourceNavigationLink> ResourceNavigationLinks => Model.ResourceNavigationLinks;
        public IList<SubResource> IpAllocations => Model.IpAllocations;
        public IList<IPConfigurationProfile> IpConfigurationProfiles => Model.IpConfigurationProfiles;
        public IList<IPConfiguration> IpConfigurations => Model.IpConfigurations;
        public string PrivateEndpointNetworkPolicies => Model.PrivateEndpointNetworkPolicies;
        public IList<PrivateEndpoint> PrivateEndpoints => Model.PrivateEndpoints;
        public IList<ServiceEndpointPropertiesFormat> ServiceEndpoints => Model.ServiceEndpoints;
        public SubResource NatGateway => Model.NatGateway;
        public RouteTable RouteTable => Model.RouteTable;
        public NetworkSecurityGroup NetworkSecurityGroup => Model.NetworkSecurityGroup;
        public IList<string> AddressPrefixes => Model.AddressPrefixes;
        public string AddressPrefix => Model.AddressPrefix;
        public string Etag => Model.Etag;
        public IList<ServiceEndpointPolicy> ServiceEndpointPolicies => Model.ServiceEndpointPolicies;
        public string PrivateLinkServiceNetworkPolicies => Model.PrivateLinkServiceNetworkPolicies;
    }
}
