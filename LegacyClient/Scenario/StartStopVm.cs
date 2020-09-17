using Azure.ResourceManager.Compute;
using Azure.ResourceManager.Resources;
using System;
using System.Linq;

namespace client
{
    class StartStopVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            // note that I must supply a subscription Id before I can list subscriptions
            var rmClient  = new ResourcesManagementClient(Context.SubscriptionId, Context.Credential);
            
            foreach (var sub in rmClient.Subscriptions.List())
            {
                var compute = new ComputeManagementClient(Context.SubscriptionId, Context.Credential);
                // since compute does not provide any filtering service side, filters must be applied client-side
                foreach (var vm in compute.VirtualMachines.ListAll().Where(v => v.Name.Contains("MyFilterString")))
                {
                    Console.WriteLine($"Found VM {vm.Name}");
                    Console.WriteLine("--------Stopping VM--------");
                    // It is somewhat awkward to have to parse the identity of the VM to get the resoource group to make this call
                    var resourceGroupName = GetResourceGroup(vm.Id);
                    compute.VirtualMachines.StartPowerOff(resourceGroupName, vm.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                    Console.WriteLine("--------Starting VM--------");
                    compute.VirtualMachines.StartStart(resourceGroupName, vm.Name).WaitForCompletionAsync().ConfigureAwait(false).GetAwaiter().GetResult();
                }
            }
        }

        // helper function to extract the resource group name from the vm identity
        static string GetResourceGroup(string resourceId)
        {
            var parts = resourceId.Split('/', StringSplitOptions.RemoveEmptyEntries);
            for(int i = 0; i < parts.Length; ++i)
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
