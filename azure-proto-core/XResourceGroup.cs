// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace azure_proto_core
{
    public class XResourceGroup : ResourceGroupOperations
    {
        internal XResourceGroup(ArmClientContext context, PhResourceGroup resource,  ArmClientOptions clientOptions)
            : base(context, resource, clientOptions)
        {
            Model = resource;
        }

        public PhResourceGroup Model { get; }
    }
}
