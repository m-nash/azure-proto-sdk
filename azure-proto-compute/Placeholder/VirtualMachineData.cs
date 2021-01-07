using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Core;
using System;
using System.Collections.Generic;

namespace azure_proto_compute
{
    /// <summary>
    /// Describes a VirtualMachine.
    /// </summary>
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

        public Azure.ResourceManager.Core.Identity Identity
        {
            get => VmIdentityToIdentity(Model.Identity);
        }

        private Azure.ResourceManager.Core.Identity VmIdentityToIdentity(VirtualMachineIdentity vmIdentity)
        {
            SystemAssignedIdentity systemAssignedIdentity = new SystemAssignedIdentity(new Guid(vmIdentity.TenantId), new Guid(vmIdentity.PrincipalId));
            var userAssignedIdentities = new Dictionary<ResourceIdentifier, Azure.ResourceManager.Core.UserAssignedIdentity>();
            if (vmIdentity.UserAssignedIdentities != null)
            {
                foreach (var entry in vmIdentity.UserAssignedIdentities)
                {
                    ResourceIdentifier resourceId = new ResourceIdentifier(entry.Key);
                    var userAssignedIdentity = new Azure.ResourceManager.Core.UserAssignedIdentity(new Guid(entry.Value.ClientId), new Guid(entry.Value.PrincipalId));
                    userAssignedIdentities[resourceId] = userAssignedIdentity;
                }
            }

            return new Azure.ResourceManager.Core.Identity(systemAssignedIdentity, userAssignedIdentities);
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
