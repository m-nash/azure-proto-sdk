using azure_proto_core;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_compute
{
    public class XAvailabilitySet : AvailabilitySetOperations
    {
        public XAvailabilitySet(ArmClientContext context, PhAvailabilitySet resource):base(context, resource)
        {
            Model = resource;
        }

        public PhAvailabilitySet Model { get; private set; }
    }
}
