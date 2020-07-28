using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhPublicIPAddress : TrackedResource<PublicIPAddress>
    {
        public PhPublicIPAddress(PublicIPAddress ip) : base(ip.Id, ip.Location, ip)
        {
            Model = ip;
        }

        new public IDictionary<string, string> Tags => Model.Tags;

        public PublicIPAddressSku Sku => Model.Sku;
        public string Etag => Model.Etag;
        public IList<string> Zones => Model.Zones;
        public IPAllocationMethod? PublicIPAllocationMethod => Model.PublicIPAllocationMethod;
        public IPVersion? PublicIPAddressVersion => Model.PublicIPAddressVersion;
        public IPConfiguration IpConfiguration => Model.IpConfiguration;
        public PublicIPAddressDnsSettings DnsSettings => Model.DnsSettings;
        public DdosSettings DdosSettings => Model.DdosSettings;
        public IList<IpTag> IpTags => Model.IpTags;
        public string IpAddress => Model.IpAddress;
        public SubResource PublicIPPrefix => Model.PublicIPPrefix;
        public int? IdleTimeoutInMinutes => Model.IdleTimeoutInMinutes;
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
    }
}
