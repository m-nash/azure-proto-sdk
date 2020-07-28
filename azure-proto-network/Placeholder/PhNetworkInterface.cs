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
            Model = nic;
        }

        new public IDictionary<string, string> Tags => Model.Tags;

        public string Etag => Model.Etag;
        public SubResource VirtualMachine => Model.VirtualMachine;
        public NetworkSecurityGroup NetworkSecurityGroup => Model.NetworkSecurityGroup;
        public PrivateEndpoint PrivateEndpoint => Model.PrivateEndpoint;
        public IList<NetworkInterfaceIPConfiguration> IpConfigurations => Model.IpConfigurations;
        public IList<NetworkInterfaceTapConfiguration> TapConfigurations => Model.TapConfigurations;
        public NetworkInterfaceDnsSettings DnsSettings => Model.DnsSettings;
        public string MacAddress => Model.MacAddress;
        public bool? Primary => Model.Primary;
        public bool? EnableAcceleratedNetworking => Model.EnableAcceleratedNetworking;
        public bool? EnableIPForwarding => Model.EnableIPForwarding;
        public IList<string> HostedWorkloads => Model.HostedWorkloads;
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
    }
}
