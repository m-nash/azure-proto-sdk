using Azure.ResourceManager.Core;
using Azure.ResourceManager.Network;
using azure_proto_compute;
using azure_proto_network;
using System;
using System.Linq;

namespace client
{
    class GetVMTaskExamples : Scenario
    {
        public GetVMTaskExamples() : base() { }

        public GetVMTaskExamples(ScenarioContext context) : base(context) { }

        public override void Execute()
        {
            var client = new AzureResourceManagerClient();

            string vmID = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Compute/virtualMachines/nibhati";
            var vmResourceId = new ResourceIdentifier(vmID);
            string subnetID = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Network/virtualNetworks/nibhati_vnet/subnets/nibhati-subnet";
            var subnetResourceId = new ResourceIdentifier(subnetID);

            // OPTION 1
            var vmOps = client.GetVirtualMachineOperations(vmResourceId);
            vmOps.PowerOff();
            Console.WriteLine("\nOPTION 1: client.GetVirtualMachineOperations(vmResourceId)");
            Console.WriteLine("Option 1 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            vmOps.PowerOn();
            Console.WriteLine("Option 1 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            var subnetOps = client.GetSubnetOperations(subnetResourceId);
            Console.WriteLine("Option 1 subnet is " + subnetOps.Id);

            // OPTION 2
            vmOps = VirtualMachineOperations.FromId(vmResourceId, client);
            vmOps.PowerOff();
            Console.WriteLine("\nOPTION 2: VirtualMachineOperations.FromId(vmResourceId, client)");
            Console.WriteLine("Option 2 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            vmOps.PowerOn();
            Console.WriteLine("Option 2 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            subnetOps = SubnetOperations.FromId(subnetResourceId, client);
            Console.WriteLine("Option 2 subnet is " + subnetOps.Id);

            // OPTION 3
            vmOps = client.GetResourceOperations<VirtualMachineOperations>(vmResourceId);
            vmOps.PowerOff();
            Console.WriteLine("\nOPTION 3: client.GetResourceOperations<VirtualMachineOperations>(vmResourceId)");
            Console.WriteLine("Option 3 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            vmOps.PowerOn();
            Console.WriteLine("Option 3 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            subnetOps = client.GetResourceOperations<SubnetOperations>(subnetResourceId);
            Console.WriteLine("Option 3 subnet is " + subnetOps.Id);

            // OPTION 4
            vmOps = new VirtualMachineOperations(resourceId, client);
            vmOps.PowerOff();
            Console.WriteLine("\nOPTION 4: new VirtualMachineOperations(resourceId, client)");
            Console.WriteLine("Option 4 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            vmOps.PowerOn();
            Console.WriteLine("Option 4 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            subnetOps = new SubnetOperations(subnetResourceId, client);
            Console.WriteLine("Option 4 subnet is " + subnetOps.Id);

            Console.WriteLine("Demo complete");
        }
    }
}
