using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class PhVirtualMachine : TrackedResource<VirtualMachine>
    {
        public PhVirtualMachine(VirtualMachine vm) : base(vm.Id, vm.Location, vm)
        {
        }

        new public IDictionary<string, string> Tags => Model.Tags;

        public override string Name => Model.Name;
        public VirtualMachineInstanceView InstanceView => Model.InstanceView;
        public string ProvisioningState => Model.ProvisioningState;
        public SubResource Host => Model.Host;
        public BillingProfile BillingProfile => Model.BillingProfile;
        public VirtualMachineEvictionPolicyTypes? EvictionPolicy => Model.EvictionPolicy;
        public VirtualMachinePriorityTypes? Priority => Model.Priority;
        public SubResource ProximityPlacementGroup => Model.ProximityPlacementGroup;
        public SubResource VirtualMachineScaleSet => Model.VirtualMachineScaleSet;
        public SubResource AvailabilitySet => Model.AvailabilitySet;
        public DiagnosticsProfile DiagnosticsProfile => Model.DiagnosticsProfile;
        public NetworkProfile NetworkProfile => Model.NetworkProfile;
        public OSProfile OsProfile => Model.OsProfile;
        public AdditionalCapabilities AdditionalCapabilities => Model.AdditionalCapabilities;
        public StorageProfile StorageProfile => Model.StorageProfile;
        public HardwareProfile HardwareProfile => Model.HardwareProfile;
        public IList<string> Zones => Model.Zones;
        public VirtualMachineIdentity Identity => Model.Identity;
        public IList<VirtualMachineExtension> Resources => Model.Resources;
        public Azure.ResourceManager.Compute.Models.Plan Plan => Model.Plan;
        public string LicenseType => Model.LicenseType;
        public string VmId => Model.VmId;

    }
}
