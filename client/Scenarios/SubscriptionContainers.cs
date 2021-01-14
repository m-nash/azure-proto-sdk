using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;

namespace client
{
    class SubscriptionContainers : Scenario
    {
        public override void Execute()
        {
            var sandboxId = "db1ab6f0-4769-4b27-930e-01e2ef9c123c";
            var client = new AzureResourceManagerClient(); 
            var subscriptionContainer = client.Subscription(sandboxId);
            var rg = subscriptionContainer.ResourceGroup("TestRg");
            var vm = rg.VirtualMachine("A-vm");
            
            ResourceIdentifier currentRg = vm.Id;

            do
            {
                Console.WriteLine(currentRg.Type);
                Console.WriteLine(currentRg.Name);
                Console.WriteLine(currentRg.Subscription);
                currentRg = currentRg.Parent;
            }while(!currentRg.IsRoot());

        }
    }
}
