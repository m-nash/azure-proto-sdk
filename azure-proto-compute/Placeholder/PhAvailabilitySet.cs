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
            if (null == aset.Tags)
            {
                aset.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public override IDictionary<string, string> Tags => Model.Tags;
        public override string Name => Model.Name;
        public Azure.ResourceManager.Compute.Models.Sku Sku
        {
            get => Model.Sku;
            set => Model.Sku = value;
        }
        public int? PlatformUpdateDomainCount
        {
            get => Model.PlatformUpdateDomainCount;
            set => Model.PlatformUpdateDomainCount = value;
        }
        public int? PlatformFaultDomainCount
        {
            get => Model.PlatformFaultDomainCount;
            set => Model.PlatformFaultDomainCount = value;
        }
        public IList<SubResource> VirtualMachines
        {
            get => Model.VirtualMachines;
            set => Model.VirtualMachines = value;
        }
        public SubResource ProximityPlacementGroup
        {
            get => Model.ProximityPlacementGroup;
            set => Model.ProximityPlacementGroup = value;
        }
        public IList<InstanceViewStatus> Statuses => Model.Statuses;
    }
}
