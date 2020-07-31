using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public interface IAzureVmModelBuilder<TBase> where TBase : AzureEntity
    {
        IAzureVmModelBuilder<TBase> UseWindowsImage(string adminUser, string password);

        IAzureVmModelBuilder<TBase> UseLinuxImage(string adminUser, string password);

        // TODO: While using generics would avoid compile time package reference, it is
        // less clear. And we can not do compile type check. I think adding an enum flag
        // param as constraint signal to user may make sense.
        IAzureVmModelBuilder<TBase> ConfigureWith(AzureEntity azureEntity);

        // TODO: Cann't do compile type check
        IAzureVmModelBuilder<TBase> AttachDataDisk(AzureEntity azureEntity);

        IAzureVmModelBuilder<TBase> Location(Location location);

        TBase ToModel();
    }
}
