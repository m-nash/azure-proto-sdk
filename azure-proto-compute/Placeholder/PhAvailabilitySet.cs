using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class PhAvailabilitySet : AvailabilitySet, IModel
    {
        public AvailabilitySet Data { get; private set; }

        public PhAvailabilitySet(AvailabilitySet aset) : base(aset.Location)
        {
            Data = aset;
        }

        new public string Name => Data.Name;
        new public string Id => Data.Id;
        new public string Type => Data.Type;
        new public string Location => Data.Location;
        new public IDictionary<string, string> Tags => Data.Tags;

        new public Sku Sku => Data.Sku;
        new public int? PlatformUpdateDomainCount => Data.PlatformUpdateDomainCount;
        new public int? PlatformFaultDomainCount => Data.PlatformFaultDomainCount;
        new public IList<SubResource> VirtualMachines => Data.VirtualMachines;
        new public SubResource ProximityPlacementGroup => Data.ProximityPlacementGroup;
        new public IList<InstanceViewStatus> Statuses => Data.Statuses;

        object IModel.Data => Data;
    }
}
