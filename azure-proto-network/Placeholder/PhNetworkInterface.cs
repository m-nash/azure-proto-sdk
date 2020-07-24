using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhNetworkInterface : NetworkInterface, IModel
    {
        public NetworkInterface Data { get; private set; }

        public PhNetworkInterface(NetworkInterface nic)
        {
            Data = nic;
        }

        new public string Name => Data.Name;
        new public string Id => Data.Id;
        new public string Type => Data.Type;
        new public string Location => Data.Location;
        new public IDictionary<string, string> Tags => Data.Tags;

        new public string Etag => Data.Etag;
        new public SubResource VirtualMachine => Data.VirtualMachine;
        new public NetworkSecurityGroup NetworkSecurityGroup => Data.NetworkSecurityGroup;
        new public PrivateEndpoint PrivateEndpoint => Data.PrivateEndpoint;
        new public IList<NetworkInterfaceIPConfiguration> IpConfigurations => Data.IpConfigurations;
        new public IList<NetworkInterfaceTapConfiguration> TapConfigurations => Data.TapConfigurations;
        new public NetworkInterfaceDnsSettings DnsSettings => Data.DnsSettings;
        new public string MacAddress => Data.MacAddress;
        new public bool? Primary => Data.Primary;
        new public bool? EnableAcceleratedNetworking => Data.EnableAcceleratedNetworking;
        new public bool? EnableIPForwarding => Data.EnableIPForwarding;
        new public IList<string> HostedWorkloads => Data.HostedWorkloads;
        new public string ResourceGuid => Data.ResourceGuid;
        new public ProvisioningState? ProvisioningState => Data.ProvisioningState;

        object IModel.Data => Data;
    }
}
