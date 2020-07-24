using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhSubnet : AzureResource<Subnet>
    {
        public override Subnet Data { get; protected set; }

        public PhSubnet(Subnet sub, string location) : base(sub.Id, location)
        {
            Data = sub;
            Location = location;
        }


        public ProvisioningState? ProvisioningState => Data.ProvisioningState;
        public string Purpose => Data.Purpose;
        public IList<Delegation> Delegations => Data.Delegations;
        public IList<ServiceAssociationLink> ServiceAssociationLinks => Data.ServiceAssociationLinks;
        public IList<ResourceNavigationLink> ResourceNavigationLinks => Data.ResourceNavigationLinks;
        public IList<SubResource> IpAllocations => Data.IpAllocations;
        public IList<IPConfigurationProfile> IpConfigurationProfiles => Data.IpConfigurationProfiles;
        public IList<IPConfiguration> IpConfigurations => Data.IpConfigurations;
        public string PrivateEndpointNetworkPolicies => Data.PrivateEndpointNetworkPolicies;
        public IList<PrivateEndpoint> PrivateEndpoints => Data.PrivateEndpoints;
        public IList<ServiceEndpointPropertiesFormat> ServiceEndpoints => Data.ServiceEndpoints;
        public SubResource NatGateway => Data.NatGateway;
        public RouteTable RouteTable => Data.RouteTable;
        public NetworkSecurityGroup NetworkSecurityGroup => Data.NetworkSecurityGroup;
        public IList<string> AddressPrefixes => Data.AddressPrefixes;
        public string AddressPrefix => Data.AddressPrefix;
        public string Etag => Data.Etag;
        public IList<ServiceEndpointPolicy> ServiceEndpointPolicies => Data.ServiceEndpointPolicies;
        public string PrivateLinkServiceNetworkPolicies => Data.PrivateLinkServiceNetworkPolicies;
    }
}
