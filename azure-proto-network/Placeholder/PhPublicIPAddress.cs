using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_network
{
    public class PhPublicIPAddress : TrackedResource<PublicIPAddress>
    {
        public static ResourceType ResourceType => "Microsoft.Network/publicIpAddresses";

        public PhPublicIPAddress(PublicIPAddress ip) : base(ip.Id, ip.Location, ip)
        {
            if (null == ip.Tags)
            {
                ip.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public override IDictionary<string, string> Tags => Model.Tags;

        public override string Name => Model.Name;
        public PublicIPAddressSku Sku
        {
            get => Model.Sku;
            set => Model.Sku = value;
        }
        public string Etag => Model.Etag;
        public IList<string> Zones
        {
            get => Model.Zones;
            set => Model.Zones = value;
        }
        public IPAllocationMethod? PublicIPAllocationMethod
        {
            get => Model.PublicIPAllocationMethod;
            set => Model.PublicIPAllocationMethod = value;
        }
        public IPVersion? PublicIPAddressVersion
        {
            get => Model.PublicIPAddressVersion;
            set => Model.PublicIPAddressVersion = value;
        }
        public IPConfiguration IpConfiguration => Model.IpConfiguration;
        public PublicIPAddressDnsSettings DnsSettings
        {
            get => Model.DnsSettings;
            set => Model.DnsSettings = value;
        }
        public DdosSettings DdosSettings
        {
            get => Model.DdosSettings;
            set => Model.DdosSettings = value;
        }
        public IList<IpTag> IpTags
        {
            get => Model.IpTags;
            set => Model.IpTags = value;
        }
        public string IpAddress
        {
            get => Model.IpAddress;
            set => Model.IpAddress = value;
        }
        public SubResource PublicIPPrefix
        {
            get => Model.PublicIPPrefix;
            set => Model.PublicIPPrefix = value;
        }
        public int? IdleTimeoutInMinutes
        {
            get => Model.IdleTimeoutInMinutes;
            set => Model.IdleTimeoutInMinutes = value;
        }
        public string ResourceGuid => Model.ResourceGuid;
        public ProvisioningState? ProvisioningState => Model.ProvisioningState;
    }
}
