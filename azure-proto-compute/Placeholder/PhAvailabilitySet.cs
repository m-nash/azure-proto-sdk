using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class PhAvailabilitySet : AzureResource<AvailabilitySet>
    {
        public override AvailabilitySet Data { get; protected set; }

        public PhAvailabilitySet(AvailabilitySet aset) : base(aset.Id, aset.Location)
        {
            Data = aset;
        }

        new public IDictionary<string, string> Tags => Data.Tags;

        public Azure.ResourceManager.Compute.Models.Sku Sku => Data.Sku;
        public int? PlatformUpdateDomainCount => Data.PlatformUpdateDomainCount;
        public int? PlatformFaultDomainCount => Data.PlatformFaultDomainCount;
        public IList<SubResource> VirtualMachines => Data.VirtualMachines;
        public SubResource ProximityPlacementGroup => Data.ProximityPlacementGroup;
        public IList<InstanceViewStatus> Statuses => Data.Statuses;
    }
}
