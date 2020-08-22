using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public abstract class VmModelBuilderBase
    {
        public abstract VmModelBuilderBase UseWindowsImage(string adminUser, string password);

        public abstract VmModelBuilderBase UseLinuxImage(string adminUser, string password);

        public abstract VmModelBuilderBase RequiredNetworkInterface(ResourceIdentifier nicResourceId);

        public abstract VmModelBuilderBase RequiredAvalabilitySet(ResourceIdentifier asetResourceId);

        public abstract PhVirtualMachine ToModel();
    }
}
