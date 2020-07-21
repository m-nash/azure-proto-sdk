using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public class AzureVm : AzureResource
    {
        public AzureVm(IResource resourceGroup, PhVirtualMachine vm) : base(resourceGroup, vm) { }

        private ComputeManagementClient Client => ClientFactory.Instance.GetComputeClient((Parent as AzureResourceGroupBase).Parent.Id);

        public void Stop()
        {
            var result = Client.VirtualMachines.StartPowerOff(Parent.Name, Model.Name).WaitForCompletionAsync().Result;
        }

        public void Start()
        {
            var result = Client.VirtualMachines.StartStart(Parent.Name, Model.Name).WaitForCompletionAsync().Result;
        }

        public void AddTag(string key, string value)
        {
            VirtualMachine vmData = Model.Data as VirtualMachine;
            if(vmData.Tags == null)
            {
                vmData.Tags = new Dictionary<string, string>();
            }

            string currentValue;
            if(!vmData.Tags.TryGetValue(key, out currentValue))
            {
                vmData.Tags.Add(key, value);
            }

            if(value != currentValue)
            {
                vmData.Tags[key] = value;
                var result = Client.VirtualMachines.StartCreateOrUpdate(Parent.Name, Name, vmData);
            }
        }
    }
}
