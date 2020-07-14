using Azure.ResourceManager.Compute.Models;
using azure_proto_sdk.Management;

namespace azure_proto_sdk.Compute
{
    public class AzureVm : AzureResource<AzureResourceGroup, VirtualMachine>
    {
        public string Id { get { return Model.Id; } }

        public AzureVm(AzureResourceGroup resourceGroup, VirtualMachine vm) : base(resourceGroup, vm) { }

        public void Stop()
        {
            var computeClient = Parent.Parent.Parent.ComputeClient;
            var result = computeClient.VirtualMachines.StartPowerOff(Parent.Name, Model.Name).WaitForCompletionAsync().Result;
        }

        public void Start()
        {
            var computeClient = Parent.Parent.Parent.ComputeClient;
            var result = computeClient.VirtualMachines.StartStart(Parent.Name, Model.Name).WaitForCompletionAsync().Result;
        }
    }
}
