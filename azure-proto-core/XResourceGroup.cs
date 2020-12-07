using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class XResourceGroup : ResourceGroupOperations
    {
        public XResourceGroup(ArmClientContext context, PhResourceGroup resource): base(context, resource)
        {
            Model = resource;
        }

        public PhResourceGroup Model { get; private set; }
    }
}
