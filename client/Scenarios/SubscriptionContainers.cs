using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;
using System.Collections.Generic;

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

            ResourceIdentifier currentRi = vm.Id; // currentRi must be base type since .parent can be any child if just traversing the tree below
            // Walk up tree
            do
            {
                //Try{
                    //ResourceGroupIdentifier z = id.convert<ResourceGroupIdentifier>(); // do something with name, sub, and rg
                    //RgIdentiferAndNameFunc(z);// do something with name, sub, and rg
                //}
                currentRi = currentRi.Parent; // Because setting parent here, needs to be base type  
            } while (!currentRi.IsRoot());

            var identifiers = new List<ResourceIdentifier>();

            var identifier = vm.Id;
            while (!identifier.IsRoot())
            {
                identifiers.Add(identifier);
                identifier = identifier.Parent;
            }

            foreach(var id in identifiers)
            {
                //do something with RG and Name and Subscription (this would require cast)
                //Try
                //{
                    //ResourceGroupIdentifier z = id.convert<ResourceGroupIdentifier>();
                    //RgIdentiferFunc(z);
                //}
            }


        }
    }
}
