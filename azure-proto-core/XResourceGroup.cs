using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class XResourceGroup : ResourceGroupOperations
    {
        internal XResourceGroup(ArmClientContext context, PhResourceGroup resource, ArmClientOptions clientOptions): base(context, resource, clientOptions)
        {
            Model = resource;
        }

        public PhResourceGroup Model { get; private set; }
    }
}
