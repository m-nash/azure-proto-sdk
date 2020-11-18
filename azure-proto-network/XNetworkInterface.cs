using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace azure_proto_network
{
    public class XNetworkInterface : NetworkInterfaceOperations
    {
        public XNetworkInterface(ArmClientContext context, PhNetworkInterface resource) : base(context, resource)
        {
            Model = resource;
        }

        public PhNetworkInterface Model { get; private set; }
    }
}
