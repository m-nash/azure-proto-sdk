using System.Threading.Tasks;
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
        /// <param name="options"> The client parameters to use in these operations. </param>
        /// <param name="resource"> The resource that is the target of operations. </param>
        internal VirtualMachine(AzureResourceManagerClientOptions options, VirtualMachineData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the data representing this VirtualMachine.
        /// </summary>
        public VirtualMachineData Data { get; private set; }

        private protected override VirtualMachine GetResource()
        {
            return this;
        }

        private protected override Task<VirtualMachine> GetResourceAsync()
        {
            return Task.FromResult(this);
        }
    }
}
