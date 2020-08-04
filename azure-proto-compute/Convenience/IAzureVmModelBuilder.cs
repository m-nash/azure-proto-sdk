using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public interface IAzureVmModelBuilder : IAzureModelBuilderBase<AzureVm>
    {
        // TODO: While using generics would avoid compile time package reference, it is
        // less clear. And we can not do compile type check. I think adding an enum flag
        // param as constraint signal to user may make sense.
        IAzureVmModelBuilder ConfigureNetworkInterface(ResourceIdentifier networkInterfaceId);

        // TODO: Cann't do compile type check
        IAzureVmModelBuilder AttachDataDisk(AzureEntity azureEntity);
    }
}
