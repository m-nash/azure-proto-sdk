using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhSubnet : Subnet, IModel
    {
        public Subnet Data { get; private set; }

        public PhSubnet(Subnet sub)
        {
            Data = sub;
        }

        new public string Name => Data.Name;
        new public string Id => Data.Id;
        
        new public ProvisioningState? ProvisioningState => Data.ProvisioningState;
        new public string Purpose => Data.Purpose;
        new public IList<Delegation> Delegations => Data.Delegations;
        new public IList<ServiceAssociationLink> ServiceAssociationLinks => Data.ServiceAssociationLinks;
        new public IList<ResourceNavigationLink> ResourceNavigationLinks => Data.ResourceNavigationLinks;
        new public IList<SubResource> IpAllocations => Data.IpAllocations;
        new public IList<IPConfigurationProfile> IpConfigurationProfiles => Data.IpConfigurationProfiles;
        new public IList<IPConfiguration> IpConfigurations => Data.IpConfigurations;
        new public string PrivateEndpointNetworkPolicies => Data.PrivateEndpointNetworkPolicies;
        new public IList<PrivateEndpoint> PrivateEndpoints => Data.PrivateEndpoints;
        new public IList<ServiceEndpointPropertiesFormat> ServiceEndpoints => Data.ServiceEndpoints;
        new public SubResource NatGateway => Data.NatGateway;
        new public RouteTable RouteTable => Data.RouteTable;
        new public NetworkSecurityGroup NetworkSecurityGroup => Data.NetworkSecurityGroup;
        new public IList<string> AddressPrefixes => Data.AddressPrefixes;
        new public string AddressPrefix => Data.AddressPrefix;
        new public string Etag => Data.Etag;
        new public IList<ServiceEndpointPolicy> ServiceEndpointPolicies => Data.ServiceEndpointPolicies;
        new public string PrivateLinkServiceNetworkPolicies => Data.PrivateLinkServiceNetworkPolicies;

        object IModel.Data => Data;
    }
}
