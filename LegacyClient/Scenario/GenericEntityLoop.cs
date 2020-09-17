using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Resources;
using System;

namespace client
{
    class GenericEntityLoop : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var rmClient = new ResourcesManagementClient(Context.SubscriptionId, Context.Credential);
            var computeClient = new ComputeManagementClient(Context.SubscriptionId, Context.Credential);
            var rg = rmClient.ResourceGroups.Get(Context.RgName).Value;
            foreach(var entity in rmClient.Resources.ListByResourceGroup(rg.Name, filter: "resourceType eq 'Microsoft.Compute/virtualMachines'"))
            {
                Console.WriteLine($"{entity.Name}");
                var vmUpdate = new VirtualMachineUpdate();
                foreach (var pair in entity?.Tags)
                {
                    vmUpdate.Tags.Add(pair.Key, pair.Value);
                }

                vmUpdate.Tags.Add("name", "value");

                // note that it is also possible to use the generic resource Update command, however, 
                // this requires additional parameters that are difficult to discover, including rp-specific api-version
                computeClient.VirtualMachines.StartUpdate(rg.Name, entity.Name, vmUpdate).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }
    }
}
