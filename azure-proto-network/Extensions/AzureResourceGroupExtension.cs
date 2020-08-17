using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Linq;

namespace azure_proto_network
{
    public static class AzureResourceGroupExtension
    {
        public static VnetContainer Vnets(this ResourceGroupContainerOperations operations, ResourceIdentifier resourceGroup)
        {
            return new VnetContainer(operations, resourceGroup);
        }

        public static VnetContainer Vnets(this ResourceGroupContainerOperations operations, TrackedResource resourceGroup)
        {
            return new VnetContainer(operations, resourceGroup);
        }

        public static VnetContainer Vnets(this ResourceGroupContainerOperations operations, string resourceGroupName)
        {
            return new VnetContainer(operations, $"/{operations.Context}/resourceGroups/{resourceGroupName}");
        }

        public static VnetContainer Vnets(this ResourceGroupOperations operations)
        {
            return new VnetContainer(operations, operations.Context);
        }

        public static VnetOperations Vnet(this ResourceGroupOperations operations, TrackedResource vnet)
        {
            return new VnetOperations(operations, vnet);
        }

        public static VnetOperations Vnet(this ResourceGroupOperations operations, string vnet)
        {
            return new VnetOperations(operations, new ResourceIdentifier($"{operations.Context}/providers/Microsoft.Network/virtualNetworks/{vnet}"));
        }



        public static PublicIpContainer PublicIps(this ResourceGroupContainerOperations operations, ResourceIdentifier resourceGroup)
        {
            return new PublicIpContainer(operations, resourceGroup);
        }

        public static PublicIpContainer PublicIps(this ResourceGroupContainerOperations operations, TrackedResource resourceGroup)
        {
            return new PublicIpContainer(operations, resourceGroup);
        }

        public static PublicIpContainer PublicIps(this ResourceGroupContainerOperations operations, string resourceGroupName)
        {
            return new PublicIpContainer(operations, $"/{operations.Context}/resourceGroups/{resourceGroupName}");
        }

        public static PublicIpContainer PublicIps(this ResourceGroupOperations operations)
        {
            return new PublicIpContainer(operations, operations.Context);
        }

        public static NicContainer Nics(this ResourceGroupContainerOperations operations, ResourceIdentifier resourceGroup)
        {
            return new NicContainer(operations, resourceGroup);
        }

        public static NicContainer Nics(this ResourceGroupContainerOperations operations, TrackedResource resourceGroup)
        {
            return new NicContainer(operations, resourceGroup);
        }

        public static NicContainer Nics(this ResourceGroupContainerOperations operations, string resourceGroupName)
        {
            return new NicContainer(operations, $"/{operations.Context}/resourceGroups/{resourceGroupName}");
        }

        public static NicContainer Nics(this ResourceGroupOperations operations)
        {
            return new NicContainer(operations, operations.Context);
        }


        public static NsgContainer Nsgs(this ResourceGroupContainerOperations operations, ResourceIdentifier resourceGroup)
        {
            return new NsgContainer(operations, resourceGroup);
        }

        public static NsgContainer Nsgs(this ResourceGroupContainerOperations operations, TrackedResource resourceGroup)
        {
            return new NsgContainer(operations, resourceGroup);
        }

        public static NsgContainer Nsgs(this ResourceGroupContainerOperations operations, string resourceGroupName)
        {
            return new NsgContainer(operations, $"/{operations.Context}/resourceGroups/{resourceGroupName}");
        }

        public static NsgContainer Nsgs(this ResourceGroupOperations operations)
        {
            return new NsgContainer(operations, operations.Context);
        }



    }
}
