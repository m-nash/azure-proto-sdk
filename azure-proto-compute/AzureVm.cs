using Azure.ResourceManager.Compute.Models;
using azure_proto_core;
using System.Collections.Generic;

namespace azure_proto_compute
{
    public class AzureVm : AzureResource
    {
        public AzureVm(IResource resourceGroup, PhVirtualMachine vm) : base(resourceGroup, vm) { }

        public void Stop()
        {
            var computeClient = Clients.ComputeClient;
            var result = computeClient.VirtualMachines.StartPowerOff(Parent.Name, Model.Name).WaitForCompletionAsync().Result;
        }

        public void Start()
        {
            var computeClient = Clients.ComputeClient;
            var result = computeClient.VirtualMachines.StartStart(Parent.Name, Model.Name).WaitForCompletionAsync().Result;
        }

        public void AddTag(string key, string value)
        {
            var computeClient = Clients.ComputeClient;
            VirtualMachine vmData = Model.Data as VirtualMachine;
            if(vmData.Tags == null)
            {
                vmData.Tags = new Dictionary<string, string>();
            }
            vmData.Tags.Add(key, value);
            var result = computeClient.VirtualMachines.StartCreateOrUpdate(Parent.Name, Name, vmData);
        }
    }
}
