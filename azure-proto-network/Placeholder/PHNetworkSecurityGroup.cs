using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Schema;

namespace azure_proto_network
{
    public class PhNetworkSecurityGroup : TrackedResource<NetworkSecurityGroup>, IEntityResource

    {
        public PhNetworkSecurityGroup(NetworkSecurityGroup nsg) : base(nsg.Id, nsg.Location, nsg) 
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
        public IList<NetworkInterface> NetworkInterfaces => Model.NetworkInterfaces;
        public IList<Subnet> Subnets => Model.Subnets;
        public IList<FlowLog> FlowLogs => Model.FlowLogs;
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;

        public static implicit operator PhNetworkSecurityGroup(NetworkSecurityGroup nsg)
        {
            return new PhNetworkSecurityGroup(nsg);
        }
    }
}
