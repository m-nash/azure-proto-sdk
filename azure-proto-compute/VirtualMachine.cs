using Azure.ResourceManager.Core;

namespace azure_proto_compute
{
    public class VirtualMachine : VirtualMachineOperations
    {
        public VirtualMachine(AzureResourceManagerClientContext context, VirtualMachineData resource)
            : base(context, resource.Id)
        {
            Data = resource;
        }

        public VirtualMachineData Data { get; private set; }
    }
}
