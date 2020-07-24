using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class PhVirtualMachine : AzureResource<VirtualMachine>
    {
        public override VirtualMachine Data { get; protected set; }

        public PhVirtualMachine(VirtualMachine vm) : base(vm.Id, vm.Location)
        {
            Data = vm;
        }

        new public IDictionary<string, string> Tags => Data.Tags;

        public VirtualMachineInstanceView InstanceView => Data.InstanceView;
        public string ProvisioningState => Data.ProvisioningState;
        public SubResource Host => Data.Host;
        public BillingProfile BillingProfile => Data.BillingProfile;
        public VirtualMachineEvictionPolicyTypes? EvictionPolicy => Data.EvictionPolicy;
        public VirtualMachinePriorityTypes? Priority => Data.Priority;
        public SubResource ProximityPlacementGroup => Data.ProximityPlacementGroup;
        public SubResource VirtualMachineScaleSet => Data.VirtualMachineScaleSet;
        public SubResource AvailabilitySet => Data.AvailabilitySet;
        public DiagnosticsProfile DiagnosticsProfile => Data.DiagnosticsProfile;
        public NetworkProfile NetworkProfile => Data.NetworkProfile;
        public OSProfile OsProfile => Data.OsProfile;
        public AdditionalCapabilities AdditionalCapabilities => Data.AdditionalCapabilities;
        public StorageProfile StorageProfile => Data.StorageProfile;
        public HardwareProfile HardwareProfile => Data.HardwareProfile;
        public IList<string> Zones => Data.Zones;
        public VirtualMachineIdentity Identity => Data.Identity;
        public IList<VirtualMachineExtension> Resources => Data.Resources;
        public Azure.ResourceManager.Compute.Models.Plan Plan => Data.Plan;
        public string LicenseType => Data.LicenseType;
        public string VmId => Data.VmId;

    }
}
