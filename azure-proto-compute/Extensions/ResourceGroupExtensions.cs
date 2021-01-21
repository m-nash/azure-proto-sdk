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
        /// <param name="vmId"> The identifier of the resource that is the target of operations. </param>
        /// <returns> Returns an object representing the operations that can be performed over a specific <see cref="[VirtualMachine]" />.</returns>
        public static VirtualMachineOperations GetVirtualMachineOperations(this ResourceGroupOperations resourceGroup, ResourceIdentifier vmId)
        {
            return new VirtualMachineOperations(resourceGroup.ClientOptions, vmId);
        }

        /// <summary>
        /// Gets an object representing a VirtualMachineContainer along with the instance operations that can be performed on it.
        /// </summary>
        /// <param> The <see cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <returns> Returns a <see cref="[VirtualMachineContainer]" /> object. </returns>
        public static VirtualMachineContainer GetVirtualMachineContainer(this ResourceGroup resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets an object representing a VirtualMachineContainer along with the instance operations that can be performed on it.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <returns> Returns a <see cref="[VirtualMachineContainer]" /> object. </returns>
        public static VirtualMachineContainer GetVirtualMachineContainer(this ResourceGroupOperations resourceGroup)
        {
            return new VirtualMachineContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion

        #region AvailabilitySets

        /// <summary>
        /// Gets an object representing the operations that can be performed over a specific AvailabilitySet.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <param name="availabilitySetId"> The identifier of the resource that is the target of operations. </param>
        /// <returns> Returns an object representing the operations that can be performed over a specific <see cref="[AvailabilitySet]" />. </returns>
        public static AvailabilitySetOperations GetAvailabilitySetOperations(this ResourceGroupOperations resourceGroup, ResourceIdentifier availabilitySetId)
        {
            return new AvailabilitySetOperations(resourceGroup.ClientOptions, availabilitySetId);
        }

        /// <summary>
        /// Gets an object representing a AvailabilitySetContainer along with the instance operations that can be performed on it.
        /// </summary>
        /// <param> The <see cref="[ResourceGroup]" /> instance the method will execute against. </param>
        /// <returns> Returns an <see cref="[AvailabilitySetContainer]" /> object. </returns>
        public static AvailabilitySetContainer GetAvailabilitySetContainer(this ResourceGroup resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Data);
        }

        /// <summary>
        /// Gets an object representing a AvailabilitySetContainer along with the instance operations that can be performed on it.
        /// </summary>
        /// <param> The <see cref="[ResourceGroupOperations]" /> instance the method will execute against. </param>
        /// <returns> Returns an <see cref="[AvailabilitySetContainer]" /> object. </returns>
        public static AvailabilitySetContainer GetAvailabilitySetContainer(this ResourceGroupOperations resourceGroup)
        {
            return new AvailabilitySetContainer(resourceGroup.ClientOptions, resourceGroup.Id);
        }
        #endregion
    }
}
