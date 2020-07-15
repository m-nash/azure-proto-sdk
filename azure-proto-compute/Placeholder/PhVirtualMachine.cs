using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class PhVirtualMachine : VirtualMachine, IModel
    {
        public VirtualMachine Data { get; private set; }

        public PhVirtualMachine(VirtualMachine vm) : base(vm.Location)
        {
            Data = vm;
        }

        new public string Name => Data.Name;
        new public string Id => Data.Id;
        new public string Type => Data.Type;
        new public string Location => Data.Location;
        new public IDictionary<string, string> Tags => Data.Tags;

        new public VirtualMachineInstanceView InstanceView => Data.InstanceView;
        new public string ProvisioningState => Data.ProvisioningState;
        new public SubResource Host => Data.Host;
        new public BillingProfile BillingProfile => Data.BillingProfile;
        new public VirtualMachineEvictionPolicyTypes? EvictionPolicy => Data.EvictionPolicy;
        new public VirtualMachinePriorityTypes? Priority => Data.Priority;
        new public SubResource ProximityPlacementGroup => Data.ProximityPlacementGroup;
        new public SubResource VirtualMachineScaleSet => Data.VirtualMachineScaleSet;
        new public SubResource AvailabilitySet => Data.AvailabilitySet;
        new public DiagnosticsProfile DiagnosticsProfile => Data.DiagnosticsProfile;
        new public NetworkProfile NetworkProfile => Data.NetworkProfile;
        new public OSProfile OsProfile => Data.OsProfile;
        new public AdditionalCapabilities AdditionalCapabilities => Data.AdditionalCapabilities;
        new public StorageProfile StorageProfile => Data.StorageProfile;
        new public HardwareProfile HardwareProfile => Data.HardwareProfile;
        new public IList<string> Zones => Data.Zones;
        new public VirtualMachineIdentity Identity => Data.Identity;
        new public IList<VirtualMachineExtension> Resources => Data.Resources;
        new public Plan Plan => Data.Plan;
        new public string LicenseType => Data.LicenseType;
        new public string VmId => Data.VmId;

        object IModel.Data => Data;
    }
}
