using Azure.ResourceManager.Core;
using azure_proto_compute;
using azure_proto_network;
using System;
using System.Runtime.InteropServices.WindowsRuntime;

namespace client
{
    class CheckResourceGroupOpsAsync : Scenario
    {
        public CheckResourceGroupOpsAsync() : base() { }

        public CheckResourceGroupOpsAsync(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            ExecuteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async System.Threading.Tasks.Task ExecuteAsync()
        {
            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            // Create Resource Group
            Console.WriteLine($"--------Start create group {Context.RgName}--------");
            var resourceGroup = subscription.GetResourceGroupContainer().Construct(Context.Loc).CreateOrUpdate(Context.RgName).Value;
            CleanUp.Add(resourceGroup.Id);
            var rgOps = subscription.GetResourceGroupOperations(Context.RgName);

            try 
            {
                rgOps.AddTag("", "");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("AddTag exception caught");
            }

            try
            {
                await rgOps.AddTagAsync(null, null);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("AddTagAsync exception caught");
            }

            try
            {
                rgOps.StartAddTag("", "");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("StartAddTag exception caught");
            }

            try
            {
                await rgOps.StartAddTagAsync(null, null);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("StartAddTagAsync exception caught");
            }

            // Create AvailabilitySet
            Console.WriteLine("--------Create AvailabilitySet async--------");
            var aset = (await (await resourceGroup.GetAvailabilitySetContainer().Construct("Aligned").StartCreateOrUpdateAsync(Context.VmName + "_aSet")).WaitForCompletionAsync()).Value;
            var data = aset.Get().Value.Data;
            try
            {                
                rgOps.CreateResource<AvailabilitySetContainer, AvailabilitySet, AvailabilitySetData>("", data, LocationData.Default);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("CreateResource exception caught");
            }

            try
            {
                await rgOps.CreateResourceAsync<AvailabilitySetContainer, AvailabilitySet, AvailabilitySetData>(" ", data, LocationData.Default);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("CreateResourceAsync exception caught");
            }

            try
            {
                rgOps.SetTags(null);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("SetTags exception caught");
            }

            try
            {
                await rgOps.SetTagsAsync(null);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("SetTagsAsync exception caught");
            }

            try
            {
                rgOps.StartSetTags(null);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("SetTags exception caught");
            }

            try
            {
                await rgOps.StartSetTagsAsync(null);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine("StartSetTagsAsync exception caught");
            }

            try
            {
                rgOps.RemoveTag("");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("RemoveTag exception caught");
            }

            try
            {
                await rgOps.RemoveTagAsync(null);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("RemoveTagAsync exception caught");
            }

            try
            {
                rgOps.StartRemoveTag(" ");
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("StartRemoveTag exception caught");
            }

            try
            {
                await rgOps.StartRemoveTagAsync(null);
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("StartRemoveTagAsync exception caught");
            }

            Console.WriteLine("--------Done--------");            
        }
    }
}
