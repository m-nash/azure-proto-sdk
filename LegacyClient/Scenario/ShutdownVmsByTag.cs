using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Compute.Models;
using Azure.ResourceManager.Resources;
using System;
using System.Linq;

namespace client
{
    class ShutdownVmsByTag : Scenario
    {
        public override void Execute()
        {
            var createMultipleVms = new CreateMultipleVms(Context);
            createMultipleVms.Execute();

            var rmClient = new ResourcesManagementClient(Context.SubscriptionId, Context.Credential);
            var computeClient = new ComputeManagementClient(Context.SubscriptionId, Context.Credential);

            //set tags on random vms
            Random rand = new Random(Environment.TickCount);
            foreach (var vm in computeClient.VirtualMachines.ListAll())
            {
                if (rand.NextDouble() > 0.5)
                {
                    Console.WriteLine("adding tag to {0}", vm.Name);
                    var vmUpdate = new VirtualMachineUpdate();
                    vmUpdate.Tags.Add("tagKey", "tagValue");
                    // note that, when using a subscription list, I need to parse the resource group from the resource ID in order to execute any operations
                    computeClient.VirtualMachines.StartUpdate(GetResourceGroup(vm.Id), vm.Name, vmUpdate).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                }
            }

            // note that we could also accomplish this by using the rp-specific list, but this would require client filtering by tag name and value
            foreach (var vm in rmClient.Resources.ListByResourceGroup(Context.RgName, filter:$"tagName eq 'tagKey' and tagValue eq 'tagValue'").Where(r => string.Equals(r.Type, "Microsoft.Compute/virtualMachines", StringComparison.InvariantCultureIgnoreCase)))
            {
                Console.WriteLine("--------Stopping VM {0}--------", vm.Name);
                computeClient.VirtualMachines.StartPowerOff(Context.RgName, vm.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                Console.WriteLine("--------Starting VM {0}--------", vm.Name);
                computeClient.VirtualMachines.StartStart(Context.RgName, vm.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
            }
        }

        // helper function to extract the resource group name from the vm identity
        static string GetResourceGroup(string resourceId)
        {
            var parts = resourceId.Split('/', StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0; i < parts.Length; ++i)
            {
                if (i < parts.Length - 1 && string.Equals("resourceGroups", parts[i], StringComparison.InvariantCultureIgnoreCase))
                {
                    return parts[i + 1];
                }
            }

            return null;
        }

    }
}
