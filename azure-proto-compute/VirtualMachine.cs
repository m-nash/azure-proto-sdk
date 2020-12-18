using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    public class VirtualMachine : VirtualMachineOperations
    {
        public VirtualMachine(AzureResourceManagerClientOptions options, VirtualMachineData resource)
            : base(options, resource.Id)
        {
            Data = resource;
        }

        public VirtualMachineData Data { get; private set; }
    }
}
