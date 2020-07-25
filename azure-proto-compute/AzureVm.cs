using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public class AzureVm : AzureEntity<VirtualMachine>
    {
        public AzureVm(TrackedResource resourceGroup, PhVirtualMachine vm) : base(vm.Id, vm.Location) 
        {
            Data = vm.Data;
        }

        private ComputeManagementClient Client => ClientFactory.Instance.GetComputeClient(Id.Subscription);

        public override VirtualMachine Data { get; protected set; }

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
            VirtualMachine vmData = Data;
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
                var result = Client.VirtualMachines.StartCreateOrUpdate(Id.ResourceGroup, Name, vmData);
            }
        }
    }
}
