using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class PhAvailabilitySet : TrackedResource<AvailabilitySet>
    {
        public PhAvailabilitySet(AvailabilitySet aset) : base(aset.Id, aset.Location, aset)
        {
            Model = aset;
        }

        new public IDictionary<string, string> Tags => Model.Tags;

        public Azure.ResourceManager.Compute.Models.Sku Sku => Model.Sku;
        public int? PlatformUpdateDomainCount => Model.PlatformUpdateDomainCount;
        public int? PlatformFaultDomainCount => Model.PlatformFaultDomainCount;
        public IList<SubResource> VirtualMachines => Model.VirtualMachines;
        public SubResource ProximityPlacementGroup => Model.ProximityPlacementGroup;
        public IList<InstanceViewStatus> Statuses => Model.Statuses;
    }
}
