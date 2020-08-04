using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using azure_proto_compute;
using azure_proto_management;
using azure_proto_network;
using azure_proto_core;

namespace client.Scenarios
{
    public class VmModelBuilder : Scenario
    {
        public override void Execute()
        {
            throw new NotImplementedException();
        }

        private Task<AzureVm> CreateVmWithBuilderAsync()
        {
            AzureResourceGroup resourceGroup;
            AzureAvailabilitySet aset;
            AzureSubnet subnet;
            
            //SetupVmHost(out resourceGroup, out aset, out subnet);
            AzureNic nic = CreateNic(resourceGroup, subnet, 0);

            // Create VM

            // TODO:
            // 0. Builder is an convenience feature. Simpler model would just use new xxx()
            // 1. Wish we can do compile time check for required properties. And now, ToModel() will do validation. 
            // 2. Is there a risk that the referenced model has not been created in ARM yet resource id is populated?

            // Options: required parameters on in the constructor
            var vm = AzureVm.Builder(resourceGroup, Context.VmName, Context.Loc)
                .UseWindowsImage("admin-user", "!@#$%asdfA")
                .RequiredNetworkInterface(nic.Id)
                .RequiredAvalabilitySet(aset.Id)
                .ToModel();

            vm = resourceGroup.Vms().CreateOrUpdateVm(vm);

            return Task.FromResult(vm);
        }
    }
}
