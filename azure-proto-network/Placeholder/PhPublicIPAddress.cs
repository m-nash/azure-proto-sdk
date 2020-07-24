using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhPublicIPAddress : AzureResource<PublicIPAddress>
    {
        public override PublicIPAddress Data { get; protected set; }

        public PhPublicIPAddress(PublicIPAddress ip) : base(ip.Id, ip.Location)
        {
            Data = ip;
        }

        new public IDictionary<string, string> Tags => Data.Tags;

        public PublicIPAddressSku Sku => Data.Sku;
        public string Etag => Data.Etag;
        public IList<string> Zones => Data.Zones;
        public IPAllocationMethod? PublicIPAllocationMethod => Data.PublicIPAllocationMethod;
        public IPVersion? PublicIPAddressVersion => Data.PublicIPAddressVersion;
        public IPConfiguration IpConfiguration => Data.IpConfiguration;
        public PublicIPAddressDnsSettings DnsSettings => Data.DnsSettings;
        public DdosSettings DdosSettings => Data.DdosSettings;
        public IList<IpTag> IpTags => Data.IpTags;
        public string IpAddress => Data.IpAddress;
        public SubResource PublicIPPrefix => Data.PublicIPPrefix;
        public int? IdleTimeoutInMinutes => Data.IdleTimeoutInMinutes;
        public string ResourceGuid => Data.ResourceGuid;
        public ProvisioningState? ProvisioningState => Data.ProvisioningState;
    }
}
