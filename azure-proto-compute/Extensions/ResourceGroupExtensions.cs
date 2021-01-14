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
        /// Gets an object representing the operations that can be performed over a specific VirtualMachine.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="vmName"> The name of the <see cref="[VirtualMachine]" />. </param>
        /// <returns> Returns an object representing the operations that can be performed over a specific <see cref="[VirtualMachine]" />.</returns>
        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, string vmName)
        {
            return new VirtualMachineOperations(resourceGroup.ClientOptions, $"{resourceGroup.Id}/providers/Microsoft.Compute/virtualMachines/{vmName}");
        }

        /// <summary>
        /// Gets an object representing the operations that can be performed over a specific VirtualMachine.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="vmId"> The identifier of the resource that is the target of operations. </param>
        /// <returns> Returns an object representing the operations that can be performed over a specific <see cref="[VirtualMachine]" />.</returns>
        public static VirtualMachineOperations VirtualMachine(this ResourceGroupOperations resourceGroup, ResourceIdentifier vmId)
        {
            return new VirtualMachineOperations(resourceGroup.ClientOptions, vmId);
        }

        /// <summary>
        /// Gets a <see cref="[VirtualMachine]" />.
        /// </summary>
        /// <param> The <see cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <param name="vmData"> The <see cref="[VirtualMachineData]" /> of the resource that is the target of operations. </param>
        /// <returns> Returns a <see cref="[VirtualMachine]" /> object. </returns>
        public static VirtualMachine VirtualMachine(this ResourceGroup resourceGroup, VirtualMachineData vmData)
        {
            return new VirtualMachine(resourceGroup.ClientOptions, vmData);
        }

        /// <summary>
        /// Gets the <see cref="[VirtualMachineContainer]" /> for a <see cref="[VirtualMachine]" />.
        /// </summary>
        /// <param> The <see cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <returns> Returns a <see cref="[VirtualMachineContainer]" /> object. </returns>
        public static VirtualMachineContainer VirtualMachines(this ResourceGroup resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets the <see cref="[VirtualMachineContainer]" /> for a <see cref="[VirtualMachine]" />.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <returns> Returns a <see cref="[VirtualMachineContainer]" /> object. </returns>
        public static VirtualMachineContainer VirtualMachines(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion

        #region AvailabilitySets
        /// <summary>
        /// Gets an object representing the operations that can be performed over a specific AvailabilitySet.
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="availabilitySetName"> The name of the <see cref="[AvailabilitySet]" />. </param>
        /// <returns> Returns an object representing the operations that can be performed over a specific <see cref="[AvailabilitySet]" />. </returns>
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, string availabilitySetName)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientOptions, $"{resourceGroup.Id}/providers/Microsoft.Compute/availabilitySets/{availabilitySetName}");
        }

        /// <summary>
        /// Gets an object representing the operations that can be performed over a specific AvailabilitySet.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="availabilitySetId"> The identifier of the resource that is the target of operations. </param>
        /// <returns> Returns an object representing the operations that can be performed over a specific <see cref="[AvailabilitySet]" />. </returns>
        public static AvailabilitySetOperations AvailabilitySet(this ResourceGroupOperations resourceGroup, ResourceIdentifier availabilitySetId)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientOptions, availabilitySetId);
        }

        /// <summary>
        /// Gets an <see cref="[AvailabilitySet]" />.
        /// </summary>
        /// <param> The <see cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <param name="availabilitySetData"> The <see cref="[AvailabilitySetData]" /> of the resource that is the target of operations. </param>
        /// <returns> Returns an <see cref="[AvailabilitySet]" /> object. </returns>
        public static AvailabilitySet AvailabilitySet(this ResourceGroup resourceGroup, AvailabilitySetData availabilitySetData)
        {
            return new AvailabilitySet(resourceGroup.ClientOptions, availabilitySetData);
        }

        /// <summary>
        /// Gets the <see cref="[AvailabilitySetContainer]" /> for an <see cref="[AvailabilitySet]" />.
        /// </summary>
        /// <param> The <see cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <returns> Returns an <see cref="[AvailabilitySetContainer]" /> object. </returns>
        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroup resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets the <see cref="[AvailabilitySetContainer]" /> for an <see cref="[AvailabilitySet]" />.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <returns> Returns an <see cref="[AvailabilitySetContainer]" /> object. </returns>
        public static AvailabilitySetContainer AvailabilitySets(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion
    }
}
