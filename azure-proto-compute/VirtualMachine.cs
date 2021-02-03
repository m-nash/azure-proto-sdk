using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    /// <summary>
    /// Class representing a VirtualMachine along with the instance operations that can be performed on it.
    /// </summary>
    public class VirtualMachine : VirtualMachineOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VirtualMachine"/> class.
        /// </summary>
        /// <param name="operations"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        internal VirtualMachine(ResourceOperationsBase operations, VirtualMachineData resource)
            : base(operations, resource.Id)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the data representing this VirtualMachine.
        /// </summary>
        public VirtualMachineData Data { get; private set; }
    }
}
