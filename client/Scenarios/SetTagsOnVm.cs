using azure_proto_compute;
using Azure.ResourceManager.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace client
{
    class SetTagsOnVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            ExecuteAsync().ConfigureAwait(false).GetAwaiter().GetResult();
        }

        private async Task ExecuteAsync()
        {
            // Update Tag for a known resource
            var rgOp = new AzureResourceManagerClient().ResourceGroup(Context.SubscriptionId, Context.RgName);
            var vmOp = rgOp.VirtualMachine(Context.VmName);

            Console.WriteLine($"Adding tags to {vmOp.Id.Name}");

            var vm = (await vmOp.StartAddTag("key1", "value1").WaitForCompletionAsync()).Value;
            DumpDictionary(vm.Data.Tags);

            vm = (await vm.StartAddTag("key2", "value2").WaitForCompletionAsync()).Value;
            DumpDictionary(vm.Data.Tags);

            vm = (await (await vmOp.StartAddTagAsync("key3", "value3")).WaitForCompletionAsync()).Value;
            DumpDictionary(vm.Data.Tags);

            vm = (await vm.StartAddTagAsync("key4", "value4")).WaitForCompletionAsync().Result.Value;
            DumpDictionary(vm.Data.Tags);
        }

        private void DumpDictionary(IDictionary<string, string> dic)
        {
            Console.WriteLine(string.Join(
                ", ",
                dic.Select(kvp => kvp.Key + ":" + kvp.Value)));
        }
    }
}
