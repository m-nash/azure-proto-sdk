using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System;
using System.Collections.Generic;
using azure_proto_core.Resources;

namespace azure_proto_compute
{
    public class PhVirtualMachine : TrackedResource<VirtualMachine>
    {
        public static ResourceType ResourceType => "Microsoft.Compute/virtualMachines";

        static PhVirtualMachine()
        {
            ArmClient.Registry.Register(
               new azure_proto_core.Internal.ArmResourceRegistration<VirtualMachineContainer, PhResourceGroup, VirtualMachineOperations, PhVirtualMachine>(
                   new ResourceType("Microsoft.Compute/virtualMachines"),
                    (o, r) => new VirtualMachineContainer(o, r as PhResourceGroup),
                    (o, r) => new VirtualMachineOperations(o, r as TrackedResource)));
        }

        public PhVirtualMachine(VirtualMachine vm) : base(vm.Id, vm.Location, vm)
        {
            if (null == vm.Tags)
            {
                vm.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public PhVirtualMachine(Azure.ResourceManager.Resources.Models.Resource vm) : base(
            vm.Id,
            vm.Location,
            new VirtualMachine(vm.Location)
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

        public Identity Identity {
            get => PhVmToIdentity(Model.Identity);
        }

        private Identity PhVmToIdentity(VirtualMachineIdentity vmIdentity)
        {
            if (vmIdentity.Type == ResourceIdentityType.None)
                return new Identity();
            else if (vmIdentity.Type == ResourceIdentityType.SystemAssigned)
            {
                Identity identity = new Identity
                {
                    SystemAssignedIdentity = new SystemAssignedIdentity(new Guid(vmIdentity.TenantId), new Guid(vmIdentity.PrincipalId)),
                    UserAssignedIdentities = null,
                };
                return identity;
            }              
            else if (vmIdentity.Type == ResourceIdentityType.UserAssigned)
            {
                var userAssignedIdentities = new Dictionary<ResourceIdentifier, UserAssignedIdentity>(); //holds useridentities
                foreach (var identity in vmIdentity.UserAssignedIdentities)
                {
                    ResourceIdentifier resourceId = new ResourceIdentifier(identity.Key);
                    UserAssignedIdentity userAssignedIdentity = new UserAssignedIdentity(new Guid(identity.Value.ClientId), new Guid(identity.Value.PrincipalId));
                    userAssignedIdentities.Add(resourceId, userAssignedIdentity);
                }
                return new Identity(null, userAssignedIdentities);
            }                
            else
            {
                return new Identity();
            }
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
