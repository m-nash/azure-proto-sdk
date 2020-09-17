using Azure;
using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using azure_proto_core.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace azure_proto_network
{
    public static class AzureResourceGroupExtension
    {
        #region Virtual Network Operations
        public static ArmResponse<ResourceOperationsBase<PhVirtualNetwork>> CreateVirtualNetwork(this ResourceGroupOperations resourceGroup, string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualNetworkContainer(resourceGroup, resourceGroup.Id);
            return container.Create(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmResponse<ResourceOperationsBase<PhVirtualNetwork>>> CreateVirtualNetworkAsync(this ResourceGroupOperations resourceGroup, string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualNetworkContainer(resourceGroup, resourceGroup.Id);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmOperation<ResourceOperationsBase<PhVirtualNetwork>> StartCreateVirtualNetwork(this ResourceGroupOperations resourceGroup, string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualNetworkContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreate(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhVirtualNetwork>>> StartCreateVirtualNetworkAsync(this ResourceGroupOperations resourceGroup, string name, PhVirtualNetwork resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new VirtualNetworkContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmBuilder<PhVirtualNetwork> ConstructVirtualNetwork(this ResourceGroupOperations resourceGroup, string vnetCidr, Location location = null)
        {
            var vnet = new VirtualNetwork()
            {
                Location = location ?? resourceGroup.DefaultLocation,
                AddressSpace = new AddressSpace() { AddressPrefixes = new List<string>() { vnetCidr } },
            };
            return new ArmBuilder<PhVirtualNetwork>(new VirtualNetworkContainer(resourceGroup, resourceGroup.Id), new PhVirtualNetwork(vnet));
        }

        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new VirtualNetworkOperations(resourceGroup, vnet);
        }

        public static VirtualNetworkOperations VirtualNetwork(this ResourceGroupOperations resourceGroup, string vnet)
        {
            return new VirtualNetworkOperations(resourceGroup, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<VirtualNetworkOperations> ListVirtualNetworks(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<VirtualNetworkOperations, PhVirtualNetwork>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<VirtualNetworkOperations> ListVvirtualNetworksAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<VirtualNetworkOperations, PhVirtualNetwork>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion

        #region Public IP Address Operations
        public static ArmResponse<ResourceOperationsBase<PhPublicIPAddress>> CreatePublicIpAddress(this ResourceGroupOperations resourceGroup, string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new PublicIpAddressContainer(resourceGroup, resourceGroup.Id);
            return container.Create(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmResponse<ResourceOperationsBase<PhPublicIPAddress>>> CreatePublicIpAddressAsync(this ResourceGroupOperations resourceGroup, string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new PublicIpAddressContainer(resourceGroup, resourceGroup.Id);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmOperation<ResourceOperationsBase<PhPublicIPAddress>> StartCreatePublicIpAddress(this ResourceGroupOperations resourceGroup, string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new PublicIpAddressContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreate(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhPublicIPAddress>>> StartCreatePublicIpAddressAsync(this ResourceGroupOperations resourceGroup, string name, PhPublicIPAddress resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new PublicIpAddressContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmBuilder<PhPublicIPAddress> ConstructIPAddress(this ResourceGroupOperations resourceGroup, Location location = null)
        {
            var ipAddress = new PublicIPAddress()
            {
                PublicIPAddressVersion = Azure.ResourceManager.Network.Models.IPVersion.IPv4.ToString(),
                PublicIPAllocationMethod = IPAllocationMethod.Dynamic,
                Location = location ?? resourceGroup.DefaultLocation,
            };

            return new ArmBuilder<PhPublicIPAddress>(new PublicIpAddressContainer(resourceGroup, resourceGroup.Id),  new PhPublicIPAddress(ipAddress));
        }

        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new PublicIpAddressOperations(resourceGroup, vnet);
        }

        public static PublicIpAddressOperations PublicIpAddress(this ResourceGroupOperations resourceGroup, string vnet)
        {
            return new PublicIpAddressOperations(resourceGroup, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<PublicIpAddressOperations> ListPublicIpAddresses(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<PublicIpAddressOperations, PhPublicIPAddress>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<PublicIpAddressOperations> ListPublicIpAddressesAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<PublicIpAddressOperations, PhPublicIPAddress>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion

        #region Network Interface (NIC) operations
        public static ArmResponse<ResourceOperationsBase<PhNetworkInterface>> CreateNetworkInterface(this ResourceGroupOperations resourceGroup, string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkInterfaceContainer(resourceGroup, resourceGroup.Id);
            return container.Create(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmResponse<ResourceOperationsBase<PhNetworkInterface>>> CreateNetworkInterfaceAsync(this ResourceGroupOperations resourceGroup, string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkInterfaceContainer(resourceGroup, resourceGroup.Id);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmOperation<ResourceOperationsBase<PhNetworkInterface>> StartCreateNetworkInterface(this ResourceGroupOperations resourceGroup, string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkInterfaceContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreate(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhNetworkInterface>>> StartCreateNetworkInterfaceAsync(this ResourceGroupOperations resourceGroup, string name, PhNetworkInterface resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkInterfaceContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmBuilder<PhNetworkInterface> ConstructNetworkInterface(this ResourceGroupOperations resourceGroup, PhPublicIPAddress ip, string subnetId, Location location = null)
        {
            var nic = new Azure.ResourceManager.Network.Models.NetworkInterface()
            {
                Location = location ?? resourceGroup.DefaultLocation,
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

            return new ArmBuilder<PhNetworkInterface>(new NetworkInterfaceContainer(resourceGroup, resourceGroup.Id), new PhNetworkInterface(nic));
        }

        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new NetworkInterfaceOperations(resourceGroup, vnet);
        }

        public static NetworkInterfaceOperations NetworkInterface(this ResourceGroupOperations resourceGroup, string vnet)
        {
            return new NetworkInterfaceOperations(resourceGroup, new ResourceIdentifier($"{resourceGroup.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<NetworkInterfaceOperations> ListNetworkInterfaces(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<NetworkInterfaceOperations, PhNetworkInterface>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<NetworkInterfaceOperations> ListNetworkInterfacesAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<NetworkInterfaceOperations, PhNetworkInterface>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion

        #region Network Security Group operations
        public static ArmResponse<ResourceOperationsBase<PhNetworkSecurityGroup>> CreateNetworkSecurityGroup(this ResourceGroupOperations resourceGroup, string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkSecurityGroupContainer(resourceGroup, resourceGroup.Id);
            return container.Create(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmResponse<ResourceOperationsBase<PhNetworkSecurityGroup>>> CreateNsgAsync(this ResourceGroupOperations resourceGroup, string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkSecurityGroupContainer(resourceGroup, resourceGroup.Id);
            return container.CreateAsync(name, resourceDetails, cancellationToken);
        }

        public static ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>> StartCreateNetworkSecurityGroup(this ResourceGroupOperations resourceGroup, string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkSecurityGroupContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreate(name, resourceDetails, cancellationToken);
        }

        public static Task<ArmOperation<ResourceOperationsBase<PhNetworkSecurityGroup>>> StartCreateNetworkSecurityGroupAsync(this ResourceGroupOperations resourceGroup, string name, PhNetworkSecurityGroup resourceDetails, CancellationToken cancellationToken = default)
        {
            var container = new NetworkSecurityGroupContainer(resourceGroup, resourceGroup.Id);
            return container.StartCreateAsync(name, resourceDetails, cancellationToken);
        }

        /// <summary>
        /// Create an NSG with the given open TCP ports
        /// </summary>
        /// <param name="openPorts">The set of TCP ports to open</param>
        /// <returns>An NSG, with the given TCP ports open</returns>
        public static ArmBuilder<PhNetworkSecurityGroup> ConstructNetworkSecurityGroup(this ResourceGroupOperations resourceGroup, string nsgName, Location location = null, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = location ?? resourceGroup.DefaultLocation };
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

            return new ArmBuilder<PhNetworkSecurityGroup>(new NetworkSecurityGroupContainer(resourceGroup, resourceGroup.Id), new PhNetworkSecurityGroup(nsg));
        }

        public static ArmBuilder<PhNetworkSecurityGroup> ConstructNetworkSecurityGroup(this ResourceGroupOperations resourceGroup, string nsgName, params int[] openPorts)
        {
            var nsg = new NetworkSecurityGroup { Location = resourceGroup.DefaultLocation };
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

            return new ArmBuilder<PhNetworkSecurityGroup>(new NetworkSecurityGroupContainer(resourceGroup, resourceGroup.Id), new PhNetworkSecurityGroup(nsg));
        }

        public static NetworkSecurityGroupOperations NetworkSecurityGroups(this ResourceGroupOperations resourceGroup, TrackedResource vnet)
        {
            return new NetworkSecurityGroupOperations(resourceGroup, vnet);
        }

        public static NetworkSecurityGroupOperations NetworkSecurityGroups(this ResourceGroupOperations operations, string vnet)
        {
            return new NetworkSecurityGroupOperations(operations, new ResourceIdentifier($"{operations.Id}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }

        public static Pageable<NetworkSecurityGroupOperations> ListNetworkSecurityGroups(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContext<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(resourceGroup, filter, top, cancellationToken);
        }

        public static AsyncPageable<NetworkSecurityGroupOperations> ListNetworkSecurityGroupsAsync(this ResourceGroupOperations resourceGroup, ArmSubstringFilter filter = null, int? top = null, CancellationToken cancellationToken = default)
        {
            return ResourceListOperations.ListAtContextAsync<NetworkSecurityGroupOperations, PhNetworkSecurityGroup>(resourceGroup, filter, top, cancellationToken);
        }
        #endregion
    }
}
