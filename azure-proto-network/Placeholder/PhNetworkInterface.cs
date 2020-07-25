using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhNetworkInterface : TrackedResource<NetworkInterface>, IEntityResource
    {
        public PhNetworkInterface(NetworkInterface nic) : base(nic.Id, nic.Location, nic)
        {
            Data = nic;
        }

        new public IDictionary<string, string> Tags => Data.Tags;

        public string ETag => Data.Etag;
        public SubResource VirtualMachine => Data.VirtualMachine;
        public NetworkSecurityGroup NetworkSecurityGroup => Data.NetworkSecurityGroup;
        public PrivateEndpoint PrivateEndpoint => Data.PrivateEndpoint;
        public IList<NetworkInterfaceIPConfiguration> IpConfigurations => Data.IpConfigurations;
        public IList<NetworkInterfaceTapConfiguration> TapConfigurations => Data.TapConfigurations;
        public NetworkInterfaceDnsSettings DnsSettings => Data.DnsSettings;
        public string MacAddress => Data.MacAddress;
        public bool? Primary => Data.Primary;
        public bool? EnableAcceleratedNetworking => Data.EnableAcceleratedNetworking;
        public bool? EnableIPForwarding => Data.EnableIPForwarding;
        public IList<string> HostedWorkloads => Data.HostedWorkloads;
        public string ResourceGuid => Data.ResourceGuid;
        public ProvisioningState? ProvisioningState => Data.ProvisioningState;
    }
}
