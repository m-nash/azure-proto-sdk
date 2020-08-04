using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Azure.ResourceManager.Resources.Models;
using azure_proto_compute;
using azure_proto_management;
using azure_proto_network;

namespace client
{
    class VMBuilderPattern
    {
        private static Task<AzureVm> CreateVmWithBuilderAsync()
        {
            AzureResourceGroup resourceGroup;
            AzureAvailabilitySet aset;
            AzureSubnet subnet;
            Program.SetupVmHost(out resourceGroup, out aset, out subnet);

            AzureNic nic = CreateNic(resourceGroup, subnet, 0);

            // Create VM
            string name = String.Format("{0}-{1}-z", vmName, 0);
            Console.WriteLine("--------Start create VM {0}--------", 0);
            var ip = new AzurePublicIpAddress(null, null);

            // TODO: Open questions
            // 0. Builder is an convenience feature. Simpler model would just use new xxx()
            // 1. Wish we can do compile time check for required properties. And now, ToModel() will do validation. 
            // 2. Is there a risk that the referenced model has not been created in ARM yet resource id is populated?
            // More directed and flavored builder to support specific scenarios imagebuilder/windowsbuilder
            // True hero scenario Scope of CORE
            var vm = AzureVm.ModelBuilder("vmname")
                .Location(new Location("uswest2"))
                .ConfigureNetwork(aset.id)
                .ConfigureWith(aset)
                .ConfigureWith(ip)  // here it should throw since PublicIP is not associated with VM
                .UseWindowsImage("admin-user", "!@#$%asdfA")
                .ToModel();

            vm = resourceGroup.Vms().CreateOrUpdateVm(name, vm);

            return Task.FromResult(vm);
        }
    }
}
