using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public interface IAzureVmModelBuilder
    {
        IAzureVmModelBuilder UseWindowsImage(string adminUser, string password);

        IAzureVmModelBuilder UseLinuxImage(string adminUser, string password);

        IAzureVmModelBuilder RequiredNetworkInterface(ResourceIdentifier nicResourceId);

        IAzureVmModelBuilder RequiredAvalabilitySet(ResourceIdentifier asetResourceId);

        AzureVm ToModel();
    }
}
