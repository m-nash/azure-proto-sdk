using azure_proto_compute;
using azure_proto_network;
using Azure.ResourceManager.Core;
using System;
using System.Diagnostics;

namespace client
{
    class GetByContainers: Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var sub = new AzureResourceManagerClient().GetSubscriptionOperations(Context.SubscriptionId);
            var rg = sub.GetResourceGroupOperations(Context.RgName);
            var virtualMachineContainer = rg.GetVirtualMachineContainer();
            foreach (var response in virtualMachineContainer.List())
            {
                var virtualMachine = virtualMachineContainer.Get(response.Data.Id.Name);
                Debug.Assert(virtualMachine.Value.Data.Name.Equals(response.Data.Id.Name));
            }
            var virtualNetworkContainer = rg.GetVirtualNetworkContainer();
            foreach (var response in virtualNetworkContainer.List())
            {
                var virtualNetwork = virtualNetworkContainer.Get(response.Data.Id.Name);
                Debug.Assert(virtualNetwork.Value.Data.Name.Equals(response.Data.Id.Name));
                foreach (var subnetResponse in response.GetSubnetContainer().List())
                {
                    var actualSubnet = subnetResponse.Get();
                    var subnets = response.GetSubnetContainer().Get(actualSubnet.Value.Data.Name);
                    Debug.Assert(subnets.Value.Data.Name.Equals(actualSubnet.Value.Data.Name));
                }
            }
            Console.WriteLine("\nDone all asserts passed ...");
            
        }
    }
}
