using Azure;
using Azure.ResourceManager.Core;
using azure_proto_compute;
using System;

namespace client
{
    class DeleteGeneric : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var rgOp = new AzureResourceManagerClient().ResourceGroup(Context.SubscriptionId, Context.RgName);
            foreach(var genericOp in rgOp.VirtualMachines().ListByName(Context.VmName))
            {
                Console.WriteLine($"Deleting {genericOp.Id}");
                genericOp.Delete();
            }

            try
            {
                var vmOp = rgOp.VirtualMachine(Context.VmName);
                Console.WriteLine($"Trying to get {vmOp.Id}");
                var response = vmOp.Get();
            }
            catch(RequestFailedException e) when (e.Status == 404)
            {
                Console.WriteLine("Got 404 returned as expected");
                return;
            }

            throw new InvalidOperationException("Failed");
        }
    }
}
