using Azure;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
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

        public static ArmOperation<ResourceOperations<PhVirtualNetwork>> CreateVnet(this ResourceGroupOperations operations, string name, PhVirtualNetwork resourceDetails)
        {
            var container = new VnetContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperations<PhVirtualNetwork>>> CreateAsync(this ResourceGroupOperations operations, string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VnetContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static PhVirtualNetwork ConstructVnet(this ResourceGroupOperations operations, string vnetCidr, Location location = null)
        {
            var vnet = new VirtualNetwork()
            {
                Location = location ?? operations.DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new PhVirtualNetwork(vnet);
        }

        public static VnetOperations Vnet(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new VnetOperations(operations, vnet);
        }

        public static VnetOperations Vnet(this ResourceGroupOperations operations, string vnet)
        {
            return new VnetOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<ResourceOperations<PhVirtualNetwork>> ListVnets(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VnetCollection(operations, operations.Context);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhVirtualNetwork>> ListVnetsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new VnetCollection(operations, operations.Context);
            return collection.ListAsync(filter, top, cancellationToken);
        }


        #endregion

        #region Public IP Address Operations
        public static ArmOperation<ResourceOperations<PhPublicIPAddress>> Create(this ResourceGroupOperations operations, string name, PhPublicIPAddress resourceDetails)
        {
            var container = new PublicIpContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperations<PhPublicIPAddress>>> CreatePublicIpAsync(this ResourceGroupOperations operations, string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new PublicIpContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static PhPublicIPAddress ConstructIPAddress(this ResourceGroupOperations operations, Location location = null)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location ?? operations.DefaultLocation,
            };

            return new PhPublicIPAddress(ipAddress);
        }

        public static PublicIpOperations PublicIp(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new PublicIpOperations(operations, vnet);
        }

        public static PublicIpOperations PublicIp(this ResourceGroupOperations operations, string vnet)
        {
            return new PublicIpOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<ResourceOperations<PhPublicIPAddress>> ListPublicIps(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpCollection(operations, operations.Context);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhPublicIPAddress>> ListPublicIpsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new PublicIpCollection(operations, operations.Context);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion

        #region Network Interface (NIC) operations
        public static ArmOperation<ResourceOperations<PhNetworkInterface>> CreateNic(this ResourceGroupOperations operations, string name, PhNetworkInterface resourceDetails)
        {
            var container = new NicContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperations<PhNetworkInterface>>> CreateNicAsync(this ResourceGroupOperations operations, string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NicContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static PhNetworkInterface ConstructNic(this ResourceGroupOperations operations, PhPublicIPAddress ip, string subnetId, Location location = null)
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

            return new PhNetworkInterface(nic);
        }

        public static NicOperations Nic(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new NicOperations(operations, vnet);
        }

        public static NicOperations Nic(this ResourceGroupOperations operations, string vnet)
        {
            return new NicOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<ResourceOperations<PhNetworkInterface>> ListNics(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NicCollection(operations, operations.Context);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhNetworkInterface>> ListNicsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NicCollection(operations, operations.Context);
            return collection.ListAsync(filter, top, cancellationToken);
        }


        #endregion

        #region Network Security Group operations
        public static ArmOperation<ResourceOperations<PhNetworkSecurityGroup>> Create(this ResourceGroupOperations operations, string name, PhNetworkSecurityGroup resourceDetails)
        {
            var container = new NsgContainer(operations, operations.Context);
            return container.Create(name, resourceDetails);
        }

        public static Task<ArmOperation<ResourceOperations<PhNetworkSecurityGroup>>> CreateAsync(this ResourceGroupOperations operations, string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NsgContainer(operations, operations.Context);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static PhNetworkSecurityGroup ConstructNsg(this ResourceGroupOperations operations, string nsgName, Location location = null, params int[] openPorts)
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

            return new PhNetworkSecurityGroup(nsg);
        }

        public static PhNetworkSecurityGroup ConstructNsg(this ResourceGroupOperations operations, string nsgName, params int[] openPorts)
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

            return new PhNetworkSecurityGroup(nsg);
        }

        public static NsgOperations Nsg(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new NsgOperations(operations, vnet);
        }

        public static NsgOperations Nsg(this ResourceGroupOperations operations, string vnet)
        {
            return new NsgOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<ResourceOperations<PhNetworkSecurityGroup>> ListNsgs(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NsgCollection(operations, operations.Context);
            return collection.List(filter, top, cancellationToken);
        }

        public static AsyncPageable<ResourceOperations<PhNetworkSecurityGroup>> ListNsgsAsync(this ResourceGroupOperations operations, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            var collection = new NsgCollection(operations, operations.Context);
            return collection.ListAsync(filter, top, cancellationToken);
        }

        #endregion

    }
}
