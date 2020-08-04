﻿using azure_proto_compute;
using azure_proto_management;
using System;

namespace client
{
    class StartFromVm : Scenario
    {
        public override void Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            //retrieve from lowest level, doesn't give ability to walk up and down the container structure
            AzureVm vm = VmCollection.GetVm(Context.SubscriptionId, Context.RgName, Context.VmName);
            Console.WriteLine($"Found VM {vm.Id}");

            //retrieve from lowest level inside management package gives ability to walk up and down
            AzureResourceGroup rg = AzureClient.GetResourceGroup(Context.SubscriptionId, Context.RgName);
            AzureVm vm2 = rg.Vms()[Context.VmName];
            Console.WriteLine($"Found VM {vm2.Id}");
        }
    }
}