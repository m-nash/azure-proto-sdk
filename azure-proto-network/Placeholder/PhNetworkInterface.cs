using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class PhNetworkInterface : TrackedResource<NetworkInterface>, IEntityResource
    {
        public PhNetworkInterface(NetworkInterface nic) : base(nic.Id, nic.Location, nic)
        {
            if (null == nic.Tags)
            {
                nic.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public override IDictionary<string, string> Tags => Model.Tags;

        public override string Name => Model.Name;
        public string Etag => Model.Etag;
        public SubResource VirtualMachine => Model.VirtualMachine;
        public NetworkSecurityGroup NetworkSecurityGroup
        {
            get => Model.NetworkSecurityGroup;
            set => Model.NetworkSecurityGroup = value;
        }
        public PrivateEndpoint PrivateEndpoint => Model.PrivateEndpoint;
        public IList<NetworkInterfaceIPConfiguration> IpConfigurations
        {
            get => Model.IpConfigurations;
            set => Model.IpConfigurations = value;
        }
        public IList<NetworkInterfaceTapConfiguration> TapConfigurations=> Model.TapConfigurations;
          
        public NetworkInterfaceDnsSettings DnsSettings
        {
            get => Model.DnsSettings;
            set => Model.DnsSettings = value;
        }
        public string MacAddress => Model.MacAddress;
        public bool? Primary => Model.Primary;
        public bool? EnableAcceleratedNetworking
        {
            get => Model.EnableAcceleratedNetworking;
            set => Model.EnableAcceleratedNetworking = value;
        }
        public bool? EnableIPForwarding
        {
            get => Model.EnableIPForwarding;
            set => Model.EnableIPForwarding = value;
        }

        public IList<string> HostedWorkloads => Model.HostedWorkloads;
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
    }
}
