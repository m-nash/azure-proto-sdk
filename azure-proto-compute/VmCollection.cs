using azure_proto_core;

namespace azure_proto_compute
{
    public class VmCollection : AzureCollection<AzureVm>
    {
        public VmCollection(IResource resourceGroup) : base(resourceGroup) { }

        protected override void LoadValues()
        {
            var computeClient = Parent.Clients.ComputeClient;
            foreach(var vm in computeClient.VirtualMachines.List(Parent.Name))
            {
                this.Add(vm.Name, new AzureVm(Parent, vm));
            }
        }

        public AzureVm CreateOrUpdateVm(string name, AzureVm vm)
        {
            var computeClient = Parent.Clients.ComputeClient;
            var vmResult = computeClient.VirtualMachines.StartCreateOrUpdate(Parent.Name, name, vm.Model).WaitForCompletionAsync().Result;
            AzureVm avm = new AzureVm(Parent, vmResult.Value);
            Add(vmResult.Value.Name, avm);
            return avm;
        }
    }
}
