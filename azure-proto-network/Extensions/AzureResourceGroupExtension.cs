using Azure;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Adapters;
using azure_proto_core.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public static class AzureResourceGroupExtension
    {
        #region Virtual Network Operations

        public static ArmOperation<VirtualNetworkOperations> CreateVnet(this ResourceGroupOperations operations, string name, PhVirtualNetwork resourceDetails)
        {
            var container = new VirtualNetworkContainer(operations, operations.Context);
            return new PhArmOperation<VirtualNetworkOperations, ResourceOperationsBase<PhVirtualNetwork>>(container.Create(name, resourceDetails), vnet => new VirtualNetworkOperations(vnet, vnet.Context));
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhVirtualNetwork>>> CreateAsync(this ResourceGroupOperations operations, string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualNetworkContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmBuilder<PhVirtualNetwork> ConstructVnet(this ResourceGroupOperations operations, string vnetCidr, Location location = null)
        {
            var vnet = new VirtualNetwork()
            {
                Location = location ?? operations.DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new ArmBuilder<PhVirtualNetwork>(new VirtualNetworkContainer(operations, operations.Context), new PhVirtualNetwork(vnet));
        }

        public static VirtualNetworkOperations Vnet(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new VirtualNetworkOperations(operations, vnet);
        }

        public static VirtualNetworkOperations Vnet(this ResourceGroupOperations operations, string vnet)
        {
            return new VirtualNetworkOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<VirtualNetworkOperations> ListVnets(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualNetworkCollection(operations, operations.Context);
            return new PhWrappingPageable<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetworkOperations>(collection.List(filter, top, cancellationToken), vnet => new VirtualNetworkOperations(vnet, vnet.Context));
        }

        public static AsyncPageable<VirtualNetworkOperations> ListVnetsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VirtualNetworkCollection(operations, operations.Context);
            return new PhWrappingAsyncPageable<ResourceOperationsBase<PhVirtualNetwork>, VirtualNetworkOperations>(collection.ListAsync(filter, top, cancellationToken), vnet => new VirtualNetworkOperations(vnet, vnet.Context));
        }


        #endregion

        #region Public IP Address Operations
        public static ArmOperation<ResourceOperationsBase<PhPublicIPAddress>> CreatePublicIp(this ResourceGroupOperations operations, string name, PhPublicIPAddress resourceDetails)
        {
            var container = new PublicIpAddressContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhPublicIPAddress>>> CreatePublicIpAsync(this ResourceGroupOperations operations, string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new PublicIpAddressContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmBuilder<PhPublicIPAddress> ConstructIPAddress(this ResourceGroupOperations operations, Location location = null)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location ?? operations.DefaultLocation,
            };

            return new ArmBuilder<PhPublicIPAddress>(new PublicIpAddressContainer(operations, operations.Context),  new PhPublicIPAddress(ipAddress));
        }

        public static PublicIpAddressOperations PublicIp(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new PublicIpAddressOperations(operations, vnet);
        }

        public static PublicIpAddressOperations PublicIp(this ResourceGroupOperations operations, string vnet)
        {
            return new PublicIpAddressOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<ResourceOperationsBase<PhPublicIPAddress>> ListPublicIps(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpAddressCollection(operations, operations.Context);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperationsBase<PhPublicIPAddress>> ListPublicIpsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpAddressCollection(operations, operations.Context);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion

        #region Network Interface (NIC) operations
        public static ArmOperation<ResourceOperationsBase<PhNetworkInterface>> CreateNic(this ResourceGroupOperations operations, string name, PhNetworkInterface resourceDetails)
        {
            var container = new NetworkInterfaceContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhNetworkInterface>>> CreateNicAsync(this ResourceGroupOperations operations, string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkInterfaceContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmBuilder<PhNetworkInterface> ConstructNic(this ResourceGroupOperations operations, PhPublicIPAddress ip, string subnetId, Location location = null)
        {
            var nic = new Azure.ResourceManager.Network.Models.NetworkInterface()
            {
                Location = location ?? operations.DefaultLocation,
                IpConfigurations = new List<NetworkInterfaceIPConfiguration>()
                {
                    new NetworkInterfaceIPConfiguration()
                    {
                        Name = "Primary",
                        Primary = true,
                        Subnet = new Subnet() { Id = subnetId },
                        PrivateIPAllocationMethod = IPAllocationMethod.Dynamic,
                        PublicIPAddress = new PublicIPAddress() { Id = ip.Id }
                    }
                }
            };

            return new ArmBuilder<PhNetworkInterface>(new NetworkInterfaceContainer(operations, operations.Context), new PhNetworkInterface(nic));
        }

        public static NetworkInterfaceOperations Nic(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new NetworkInterfaceOperations(operations, vnet);
        }

        public static NetworkInterfaceOperations Nic(this ResourceGroupOperations operations, string vnet)
        {
            return new NetworkInterfaceOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<ResourceOperationsBase<PhNetworkInterface>> ListNics(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkInterfaceCollection(operations, operations.Context);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperationsBase<PhNetworkInterface>> ListNicsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkInterfaceCollection(operations, operations.Context);
            return collection.ListAsync(filter, top, cancellationToken);
        }


        #endregion

        #region Network Security Group operations
        public static ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>> CreateNsg(this ResourceGroupOperations operations, string name, PhNetworkSecurityGroup resourceDetails)
        {
            var container = new NetworkSecurityGroupContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>>> CreateNsgAsync(this ResourceGroupOperations operations, string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkSecurityGroupContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static ArmBuilder<PhNetworkSecurityGroup> ConstructNsg(this ResourceGroupOperations operations, string nsgName, Location location = null, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = location ?? operations.DefaultLocation };
            var index = 0;
            nsg.SecurityRules = openPorts.Select(openPort => new SecurityRule
            {
                Name = $"Port{openPort}",
                Priority = 1000 + (++index),
                Protocol = SecurityRuleProtocol.Tcp,
                Access = SecurityRuleAccess.Allow,
                Direction = SecurityRuleDirection.Inbound,
                SourcePortRange = "*",
                SourceAddressPrefix = "*",
                DestinationPortRange = $"{openPort}",
                DestinationAddressPrefix = "*",
                Description = $"Port_{openPort}"
            }).ToList();

            return new ArmBuilder<PhNetworkSecurityGroup>(new NetworkSecurityGroupContainer(operations, operations.Context), new PhNetworkSecurityGroup(nsg));
        }

        public static ArmBuilder<PhNetworkSecurityGroup> ConstructNsg(this ResourceGroupOperations operations, string nsgName, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = operations.DefaultLocation };
            var index = 0;
            nsg.SecurityRules = openPorts.Select(openPort => new SecurityRule
            {
                Name = $"Port{openPort}",
                Priority = 1000 + (++index),
                Protocol = SecurityRuleProtocol.Tcp,
                Access = SecurityRuleAccess.Allow,
                Direction = SecurityRuleDirection.Inbound,
                SourcePortRange = "*",
                SourceAddressPrefix = "*",
                DestinationPortRange = $"{openPort}",
                DestinationAddressPrefix = "*",
                Description = $"Port_{openPort}"
            }).ToList();

            return new ArmBuilder<PhNetworkSecurityGroup>(new NetworkSecurityGroupContainer(operations, operations.Context), new PhNetworkSecurityGroup(nsg));
        }

        public static NetworkSecurityGroupOperations Nsgs(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new NetworkSecurityGroupOperations(operations, vnet);
        }

        public static NetworkSecurityGroupOperations Nsgs(this ResourceGroupOperations operations, string vnet)
        {
            return new NetworkSecurityGroupOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<ResourceOperationsBase<PhNetworkSecurityGroup>> ListNsgs(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkSecurityGroupCollection(operations, operations.Context);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperationsBase<PhNetworkSecurityGroup>> ListNsgsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NetworkSecurityGroupCollection(operations, operations.Context);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion

    }
}
