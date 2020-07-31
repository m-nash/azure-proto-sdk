using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;
using azure_proto_compute.Convenience;

namespace azure_proto_compute
{
    public class AzureVm : AzureEntity<PhVirtualMachine>
    {
        public AzureVm(TrackedResource resourceGroup, PhVirtualMachine vm) : base(resourceGroup, vm) 
        {
        }

        private ComputeManagementClient Client => ClientFactory.Instance.GetComputeClient(Id.Subscription);

        public void Stop()
        {
            var result = Client.VirtualMachines.StartPowerOff(Id.ResourceGroup, Name).WaitForCompletionAsync().Result;
        }

        public void Start()
        {
            var result = Client.VirtualMachines.StartStart(Id.ResourceGroup, Name).WaitForCompletionAsync().Result;
        }

        public void AddTag(string key, string value)
        {
            var vmData = Model;
            string currentValue;
            if(!vmData.Tags.TryGetValue(key, out currentValue))
            {
                vmData.Tags.Add(key, value);
            }

            if(value != currentValue)
            {
                vmData.Tags[key] = value;
                var result = Client.VirtualMachines.StartCreateOrUpdate(Id.ResourceGroup, Name, vmData.Model);
            }
        }
    }
}
