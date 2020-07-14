using Azure.ResourceManager.Compute.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk
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
            var computeClient = resourceGroup.Location.Subscription.ComputeClient;
            foreach(var vm in computeClient.VirtualMachines.List(resourceGroup.Name))
            {
                this.Add(vm.Name, new AzureVm(resourceGroup, vm));
            }
        }

        internal AzureVm CreateOrUpdateVm(string name, VirtualMachine vm)
        {
            var computeClient = resourceGroup.Location.Subscription.ComputeClient;
            var vmResult = computeClient.VirtualMachines.StartCreateOrUpdate(resourceGroup.Name, name, vm).WaitForCompletionAsync().Result;
            AzureVm avm = new AzureVm(resourceGroup, vmResult.Value);
            Add(vmResult.Value.Name, avm);
            return avm;
        }
    }
}
