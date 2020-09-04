using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using azure_proto_core;

namespace azure_proto_compute.Convenience
{
    public abstract class VirtualMachineModelBuilderBase
    {
        public abstract VirtualMachineModelBuilderBase UseWindowsImage(string adminUser, string password);

        public abstract VirtualMachineModelBuilderBase UseLinuxImage(string adminUser, string password);

        public abstract VirtualMachineModelBuilderBase RequiredNetworkInterface(ResourceIdentifier nicResourceId);

        public abstract VirtualMachineModelBuilderBase RequiredAvalabilitySet(ResourceIdentifier asetResourceId);

        public abstract PhVirtualMachine ToModel();
    }
}
