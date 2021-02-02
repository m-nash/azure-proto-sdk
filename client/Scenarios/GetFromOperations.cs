using Azure.ResourceManager.Core;
using azure_proto_compute;
using azure_proto_network;

namespace client
{
    class GetFromOperations : Scenario
    {
        public override System.Threading.Tasks.Task Execute()
        {
            var createVm = new CreateSingleVmExample(Context);
            createVm.Execute();

            var client = new AzureResourceManagerClient();
            var subscription = client.GetSubscriptionOperations(Context.SubscriptionId);

            var resourceGroup = subscription.GetResourceGroupOperations(Context.RgName).Get().Value;
            _ = resourceGroup.GetAvailabilitySetOperations(Context.VmName + "_aSet").Get().Value;
            var vnet = resourceGroup.GetVirtualNetworkOperations(Context.VmName + "_vnet").Get().Value;
            _ = vnet.GetSubnetOperations(Context.SubnetName).Get().Value;
            _ = resourceGroup.GetNetworkSecurityGroupOperations(Context.NsgName).Get().Value;
            _ = resourceGroup.GetPublicIpAddressOperations($"{Context.VmName}_ip").Get().Value;
            _ = resourceGroup.GetNetworkInterfaceOperations($"{Context.VmName}_nic").Get().Value;
            _ = resourceGroup.GetVirtualMachineOperations(Context.VmName).Get().Value;

            return System.Threading.Tasks.Task.FromResult<object>(null);
        }
    }
}
