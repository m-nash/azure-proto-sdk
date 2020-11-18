using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_network
{
    public class XPublicIpAddress : PublicIpAddressOperations
    {
        public XPublicIpAddress(ArmClientContext context, PhPublicIPAddress resource) : base(context, resource)
        {
            Model = resource;
        }

        public PhPublicIPAddress Model { get; private set; }
    }
}
