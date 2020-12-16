using Azure.ResourceManager.Network.Models;
using Azure.ResourceManager.Core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class NetworkSecurityGroupData : TrackedResource<Azure.ResourceManager.Network.Models.NetworkSecurityGroup>, IEntityResource

    {
        public static ResourceType ResourceType => "Microsoft.Network/networkSecurityGroups";

        public NetworkSecurityGroupData(Azure.ResourceManager.Network.Models.NetworkSecurityGroup nsg) : base(nsg.Id, nsg.Location, nsg) 
        {
            if (null == nsg.Tags)
            {
                nsg.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public override IDictionary<string, string> Tags => Model.Tags;
        public override string Name => Model.Name;
        public string Etag => Model.Etag;
        public IList<SecurityRule> SecurityRules
        {
            get => Model.SecurityRules;
            set => Model.SecurityRules = value;
        }
        public IList<SecurityRule> DefaultSecurityRules => Model.DefaultSecurityRules;
        public IList<Azure.ResourceManager.Network.Models.NetworkInterface> NetworkInterfaces => Model.NetworkInterfaces;
        public IList<Azure.ResourceManager.Network.Models.Subnet> Subnets => Model.Subnets;
        public IList<FlowLog> FlowLogs => Model.FlowLogs;
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;

        public static implicit operator NetworkSecurityGroupData(Azure.ResourceManager.Network.Models.NetworkSecurityGroup nsg)
        {
            return new NetworkSecurityGroupData(nsg);
        }
    }
}
