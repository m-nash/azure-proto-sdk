using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class NetworkInterfaceData : TrackedResource<Azure.ResourceManager.Network.Models.NetworkInterface>, IEntityResource
    {
        public static ResourceType ResourceType => "Microsoft.Network/networkInterfaces";

        public NetworkInterfaceData(Azure.ResourceManager.Network.Models.NetworkInterface nic) : base(nic.Id, nic.Location, nic)
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
        public Azure.ResourceManager.Network.Models.NetworkSecurityGroup NetworkSecurityGroup
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
