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

            string vmId = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Compute/virtualMachines/nibhati";
            string subnetId = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Network/virtualNetworks/nibhati_vnet/subnets/nibhati-subnet";
            string asId = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Compute/availabilitySets/nibhati_aSet";
            string nsgId = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Network/networkSecurityGroups/nibhati-test-nsg";
            string vnId = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Network/virtualNetworks/nibhati_vnet";
            string niId = "/subscriptions/db1ab6f0-4769-4b27-930e-01e2ef9c123c/resourceGroups/nbhatia-test/providers/Microsoft.Network/networkInterfaces/nibhati_nic";

            // OPTION 1
            var vmOps = client.GetVirtualMachineOperations(vmId);
            Console.WriteLine("\nclient.GetVirtualMachineOperations(vmResourceId)");
            vmOps.PowerOff();            
            Console.WriteLine("Option 1 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);
            vmOps.PowerOn();
            Console.WriteLine("Option 1 vm is " + vmOps.Get().Value.Data.InstanceView.Statuses.Last().Code);

            var subnetOps = client.GetSubnetOperations(subnetId);
            Console.WriteLine("Option 1 subnet is " + subnetOps.Id);

            var vnOps = client.GetVirtualNetworkOperations(vnId);            
            var nsgOps = client.GetNetworkSecurityGroupOperations(nsgId);
            var niOps = client.GetNetworkInterfaceOperations(niId);
            var asOps = client.GetAvailabilitySetOperations(asId);

            Console.WriteLine(vnOps.Id);
            Console.WriteLine(nsgOps.Id);
            Console.WriteLine(niOps.Id);
            Console.WriteLine(asOps.Id);

            Console.WriteLine("Demo complete");
        }
    }
}
