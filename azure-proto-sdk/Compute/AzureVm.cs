using Azure.ResourceManager.Compute.Models;
using azure_proto_sdk.Management;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk.Compute
{
    public class AzureVm : AzureResource<AzureResourceGroup, VirtualMachine>
    {
        public string Id { get { return Model.Id; } }

        public AzureVm(AzureResourceGroup resourceGroup, VirtualMachine vm) : base(resourceGroup, vm) { }
    }
}
