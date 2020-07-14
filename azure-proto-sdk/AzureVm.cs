using Azure.ResourceManager.Compute.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk
{
    public class AzureVm
    {
        public AzureResourceGroup ResourceGroup { get; private set; }

        private VirtualMachine vm;

        public string Id { get { return vm.Id; } }

        public AzureVm(AzureResourceGroup resourceGroup, VirtualMachine vm)
        {
            ResourceGroup = resourceGroup;
            this.vm = vm;
        }
    }
}
