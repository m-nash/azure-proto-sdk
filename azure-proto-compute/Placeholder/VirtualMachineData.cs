using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public class VirtualMachineData : TrackedResource<Azure.ResourceManager.Compute.Models.VirtualMachine>
    {
        public static ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        public VirtualMachineData(Azure.ResourceManager.Compute.Models.VirtualMachine vm) : base(vm.Id, vm.Location, vm)
        {
            if (null == vm.Tags)
            {
                vm.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public VirtualMachineData(Azure.ResourceManager.Resources.Models.Resource vm) : base(
            vm.Id,
            vm.Location,
            new Azure.ResourceManager.Compute.Models.VirtualMachine(vm.Location)
            {
                Tags = vm.Tags == null ? new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase) : vm.Tags
            })
        {
        }

        public override IDictionary<string, string> Tags => Model.Tags;
        public override string Name => Model.Name;
        public VirtualMachineInstanceView InstanceView => Model.InstanceView;
        public string ProvisioningState => Model.ProvisioningState;
        public SubResource Host
        {
            get => Model.Host;
            set => Model.Host = value;
        }

        public BillingProfile BillingProfile
        {
            get => Model.BillingProfile;
            set => Model.BillingProfile = value;
        }

        public VirtualMachineEvictionPolicyTypes? EvictionPolicy
        {
            get => Model.EvictionPolicy;
            set => Model.EvictionPolicy = value;
        }

        public VirtualMachinePriorityTypes? Priority
        {
            get => Model.Priority;
            set => Model.Priority = value;
        }

        public SubResource ProximityPlacementGroup
        {
            get => Model.ProximityPlacementGroup;
            set => Model.ProximityPlacementGroup = value;
        }

        public SubResource VirtualMachineScaleSet
        {
            get => Model.VirtualMachineScaleSet;
            set => Model.VirtualMachineScaleSet = value;
        }

        public SubResource AvailabilitySet
        {
            get => Model.AvailabilitySet;
            set => Model.AvailabilitySet = value;
        }

        public DiagnosticsProfile DiagnosticsProfile
        {
            get => Model.DiagnosticsProfile;
            set => Model.DiagnosticsProfile = value;
        }

        public NetworkProfile NetworkProfile
        {
            get => Model.NetworkProfile;
            set => Model.NetworkProfile = value;
        }
        public OSProfile OsProfile
        {
            get => Model.OsProfile;
            set => Model.OsProfile = value;
        }
        public AdditionalCapabilities AdditionalCapabilities
        {
            get => Model.AdditionalCapabilities;
            set => Model.AdditionalCapabilities = value;
        }
        public StorageProfile StorageProfile
        {
            get => Model.StorageProfile;
            set => Model.StorageProfile = value;
        }
        public HardwareProfile HardwareProfile
        {
            get => Model.HardwareProfile;
            set => Model.HardwareProfile = value;
        }
        public IList<string> Zones
        {
            get => Model.Zones;
            set => Model.Zones = value;
        }
        public VirtualMachineIdentity Identity
        {
            get => Model.Identity;
            set => Model.Identity = value;
        }

        public IList<VirtualMachineExtension> Resources => Model.Resources;
        public Azure.ResourceManager.Compute.Models.Plan Plan
        {
            get => Model.Plan;
            set => Model.Plan = value;
        }
        public string LicenseType
        {
            get => Model.LicenseType;
            set => Model.LicenseType = value;
        }
        public string VmId => Model.VmId;
    }
}