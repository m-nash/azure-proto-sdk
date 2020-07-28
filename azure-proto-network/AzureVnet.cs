using Azure.ResourceManager.Network.Models;
using azure_proto_core;

namespace azure_proto_network
{
    public class AzureVnet : AzureEntity<PhVirtualNetwork>
    {
        public SubnetCollection Subnets { get; private set; }

        public NetworkSecurityGroupCollection Nsgs { get; private set; }

        public AzureVnet(TrackedResource resourceGroup, PhVirtualNetwork vnet) : base(resourceGroup, vnet)
        {
        }

        public AzureNetworkSecurityGroup ConstructNsg(int openPort)
        {
            var rule = new SecurityRule
            {
                Priority = 101,
                Protocol = SecurityRuleProtocol.Tcp,
                DestinationPortRange = $"{openPort}",
                SourcePortRange = $"{openPort}",
                Access = SecurityRuleAccess.Allow,
                Description = $"Port_{openPort}"
            };

            var defaultRule = new SecurityRule
            {
                Priority = 9000,
                Protocol = SecurityRuleProtocol.Asterisk,
                SourcePortRange = "*",
                DestinationPortRange = "*",
                Access = SecurityRuleAccess.Deny,
                Description = "Tcp_Deny"
            };

            var nsg = new NetworkSecurityGroup();
            nsg.DefaultSecurityRules.Add(defaultRule);
            nsg.SecurityRules.Add(rule);

            return new AzureNetworkSecurityGroup(Parent, nsg);
        }

        public AzureSubnet ConstructSubnet(string name, string cidr, AzureNetworkSecurityGroup group = null)
        {
            var subnet = new Subnet()
            {
                Name = name,
                AddressPrefix = cidr,
            };

            if (null != group)
            {
                subnet.NetworkSecurityGroup = group.Model;
            }

            return new AzureSubnet(this, new PhSubnet(subnet, Location));
        }
    }
}
