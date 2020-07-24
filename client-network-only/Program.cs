using azure_proto_management;
using azure_proto_network;
using System;

namespace client_network_only
{
    class Program
    {
        private static string vmName = String.Format("{0}-quickstartvm", Environment.UserName);
        private static string rgName = String.Format("{0}-test-rg", Environment.UserName);
        private static string subscriptionId = Environment.GetEnvironmentVariable("AZURE_SUBSCRIPTION_ID");
        private static string loc = "westus2";
        private static string subnetName = "mySubnet";

        static void Main(string[] args)
        {
            CreateNetworkOnly();
        }

        private static void CreateNetworkOnly()
        {
            AzureClient client = new AzureClient();
            var subscription = client.Subscriptions[subscriptionId];

            // Set Location
            var location = subscription.Locations[loc];

            // Create Resource Group
            Console.WriteLine("--------Start create group--------");
            var resourceGroup = location.ResourceGroups.CreateOrUpdate(rgName);

            // Create IP Address
            Console.WriteLine("--------Start create IP Address--------");
            var ipAddress = resourceGroup.ConstructIPAddress();
            ipAddress = resourceGroup.IpAddresses().CreateOrUpdatePublicIpAddress(vmName + "_ip", ipAddress);

            // Create VNet
            Console.WriteLine("--------Start create VNet--------");
            string vnetName = vmName + "_vnet";
            var vnet = resourceGroup.VNets()[vnetName];
            if (vnet == null)
            {
                vnet = resourceGroup.ConstructVnet("10.0.0.0/16");
                vnet = resourceGroup.VNets().CreateOrUpdateVNet(vmName + "_vnet", vnet);
            }

            //create subnet
            Console.WriteLine("--------Start create Subnet--------");
            var subnet = vnet.Subnets[subnetName];
            if (subnet == null)
            {
                subnet = vnet.ConstructSubnet(subnetName, "10.0.0.0/24");
                subnet = vnet.Subnets.CreateOrUpdateSubnets(subnet);
            }

            // Create Network Interface
            Console.WriteLine("--------Start create Network Interface--------");
            var nic = resourceGroup.ConstructNic(ipAddress, subnet.Model.Id);
            nic = resourceGroup.Nics().CreateOrUpdateNic(vmName + "_nic", nic);
        }
    }
}
