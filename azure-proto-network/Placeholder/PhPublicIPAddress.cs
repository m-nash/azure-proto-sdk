using Azure.ResourceManager.Network.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class PhPublicIPAddress : PublicIPAddress, IModel
    {
        public PublicIPAddress Data { get; private set; }

        public PhPublicIPAddress(PublicIPAddress ip)
        {
            Data = ip;
        }

        new public string Name => Data.Name;
        new public string Id => Data.Id;
        new public string Type => Data.Type;
        new public string Location => Data.Location;
        new public IDictionary<string, string> Tags => Data.Tags;

        new public PublicIPAddressSku Sku => Data.Sku;
        new public string Etag => Data.Etag;
        new public IList<string> Zones => Data.Zones;
        new public IPAllocationMethod? PublicIPAllocationMethod => Data.PublicIPAllocationMethod;
        new public IPVersion? PublicIPAddressVersion => Data.PublicIPAddressVersion;
        new public IPConfiguration IpConfiguration => Data.IpConfiguration;
        new public PublicIPAddressDnsSettings DnsSettings => Data.DnsSettings;
        new public DdosSettings DdosSettings => Data.DdosSettings;
        new public IList<IpTag> IpTags => Data.IpTags;
        new public string IpAddress => Data.IpAddress;
        new public SubResource PublicIPPrefix => Data.PublicIPPrefix;
        new public int? IdleTimeoutInMinutes => Data.IdleTimeoutInMinutes;
        new public string ResourceGuid => Data.ResourceGuid;
        new public ProvisioningState? ProvisioningState => Data.ProvisioningState;

        object IModel.Data => Data;
    }
}
