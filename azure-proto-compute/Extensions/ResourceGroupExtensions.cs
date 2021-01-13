using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    /// <summary>
    /// A class to add extension methods to ResourceGroup.
    /// </summary>
    public static class ResourceGroupExtensions
    {
        #region VirtualMachines
        /// <summary>
        /// Gets the VirtualMachineOperations for a VirtualMachine.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="vmName"> The name of the VirtualMachine. </param>
        /// <returns> Returns a response with the <see cref="VirtualMachineOperations"/> operation for this resource. </returns>
        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, string vmName)
        {
            return new VirtualMachineOperations(resourceGroup.ClientOptions, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{vmName}");
        }

        /// <summary>
        /// Gets the VirtualMachineOperations for a VirtualMachine.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="vmId"> The identifier of the resource that is the target of operations. </param>
        /// <returns> Returns a response with the <see cref="VirtualMachineOperations"/> operation for this resource. </returns>
        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, ResourceIdentifier vmId)
        {
            return new VirtualMachineOperations(resourceGroup.ClientOptions, vmId);
        }

        /// <summary>
        /// Gets the VirtualMachineOperations for a VirtualMachine.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <param name="vmData"> The VirtualMachineData of the resource that is the target of operations. </param>
        /// <returns> Returns a response with the <see cref="VirtualMachineOperations"/> operation for this resource. </returns>
        public static VirtualMachine VirtualMachine(this ResourceGroup resourceGroup, VirtualMachineData vmData)
        {
            return new VirtualMachine(resourceGroup.ClientOptions, vmData);
        }

        /// <summary>
        /// Gets the VirtualMachineContainer for a VirtualMachine.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <returns> Returns a response with the <see cref="VirtualMachineContainer"/> operation for this resource. </returns>
        public static VirtualMachineContainer VirtualMachines(this ResourceGroup resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets the VirtualMachineContainer for a VirtualMachine.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <returns> Returns a response with the <see cref="VirtualMachineContainer"/> operation for this resource. </returns>
        public static VirtualMachineContainer VirtualMachines(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion

        #region AvailabilitySets
        /// <summary>
        /// Gets the AvailabilitySetOperations for an AvailabilitySet.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="availabilitySetName"> The name of the AvailabilitySet. </param>
        /// <returns> Returns a response with the <see cref="AvailabilitySetOperations"/> operation for this resource. </returns>
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, string availabilitySetName)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientOptions, $"{resourceGroup.Id}/providers/Microsoft.Compute/availabilitySets/{availabilitySetName}");
        }

        /// <summary>
        /// Gets the AvailabilitySetOperations for an AvailabilitySet.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="availabilitySetId"> The identifier of the resource that is the target of operations. </param>
        /// <returns> Returns a response with the <see cref="AvailabilitySetOperations"/> operation for this resource. </returns>
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, ResourceIdentifier availabilitySetId)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientOptions, availabilitySetId);
        }

        /// <summary>
        /// Gets the AvailabilitySetOperations for an AvailabilitySet.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <param name="availabilitySetData"> The AvailabilitySetData of the resource that is the target of operations. </param>
        /// <returns> Returns a response with the <see cref="AvailabilitySet"/> operation for this resource. </returns>
        public static AvailabilitySet AvailabilitySet(this ResourceGroup resourceGroup, AvailabilitySetData availabilitySetData)
        {
            return new AvailabilitySet(resourceGroup.ClientOptions, availabilitySetData);
        }

        /// <summary>
        /// Gets the AvailabilitySetContainer for an AvailabilitySet.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <returns> Returns a response with the <see cref="AvailabilitySetContainer"/> operation for this resource. </returns>
        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroup resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets the AvailabilitySetContainer for an AvailabilitySet.
        /// </summary>
        /// <param> The <see  cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <returns> Returns a response with the <see cref="AvailabilitySetContainer"/> operation for this resource. </returns>
        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion
    }
}
