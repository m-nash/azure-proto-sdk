﻿using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class ArmResource : ArmResourceOperations
    {
        public ArmResource(ArmClientContext context, ArmResourceData resource, ArmClientOptions clientOption)
            : base(context, resource.Id, clientOption)
        {
            Data = resource;
        }

        public ArmResourceData Data { get; }
    }
}
