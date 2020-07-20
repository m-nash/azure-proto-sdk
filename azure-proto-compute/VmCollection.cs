using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public class VmCollection : AzureCollection<AzureVm>
    {
        public VmCollection(IResource resourceGroup) : base(resourceGroup) { }

        public AzureVm CreateOrUpdateVm(string name, AzureVm vm)
        {
            var computeClient = Parent.Clients.ComputeClient;
            var vmResult = computeClient.VirtualMachines.StartCreateOrUpdate(Parent.Name, name, vm.Model.Data as VirtualMachine).WaitForCompletionAsync().Result;
            AzureVm avm = new AzureVm(Parent, new PhVirtualMachine(vmResult.Value));
            return avm;
        }

        public static AzureVm GetVm(string subscriptionId, string rgName, string vmName)
        {
            ClientFactory clients = new ClientFactory(subscriptionId);
            var vmResult = clients.ComputeClient.VirtualMachines.Get(rgName, vmName);
            return new AzureVm(null, new PhVirtualMachine(vmResult.Value));
        }

        protected override AzureVm Get(string vmName)
        {
            var computeClient = Parent.Clients.ComputeClient;
            var vmResult = computeClient.VirtualMachines.Get(Parent.Name, vmName);
            return new AzureVm(Parent, new PhVirtualMachine(vmResult.Value));
        }

        protected override IEnumerable<AzureVm> GetItems()
        {
            var computeClient = Parent.Clients.ComputeClient;
            foreach (var vm in computeClient.VirtualMachines.List(Parent.Name))
            {
                yield return new AzureVm(Parent, new PhVirtualMachine(vm));
            }
        }

        public IEnumerable<AzureVm> GetItemsByTag(string key, string value)
        {
            var computeClient = Parent.Clients.ComputeClient;
            foreach(var vm in computeClient.VirtualMachines.List(Parent.Name))
            {
                string rValue;
                if (vm.Tags != null && vm.Tags.TryGetValue(key, out rValue) && rValue == value)
                    yield return new AzureVm(Parent, new PhVirtualMachine(vm));
            }
        }
    }
}
