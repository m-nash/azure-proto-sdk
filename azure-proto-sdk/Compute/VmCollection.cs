using azure_proto_sdk.Management;

namespace azure_proto_sdk.Compute
{
    public class VmCollection : AzureCollection<AzureVm>
    {
        private AzureResourceGroup resourceGroup;

        public VmCollection(AzureResourceGroup resourceGroup)
        {
            this.resourceGroup = resourceGroup;
        }

        protected override void LoadValues()
        {
            var computeClient = resourceGroup.Parent.Parent.ComputeClient;
            foreach(var vm in computeClient.VirtualMachines.List(resourceGroup.Name))
            {
                this.Add(vm.Name, new AzureVm(resourceGroup, vm));
            }
        }

        internal AzureVm CreateOrUpdateVm(string name, AzureVm vm)
        {
            var computeClient = resourceGroup.Parent.Parent.ComputeClient;
            var vmResult = computeClient.VirtualMachines.StartCreateOrUpdate(resourceGroup.Name, name, vm.Model).WaitForCompletionAsync().Result;
            AzureVm avm = new AzureVm(resourceGroup, vmResult.Value);
            Add(vmResult.Value.Name, avm);
            return avm;
        }
    }
}
