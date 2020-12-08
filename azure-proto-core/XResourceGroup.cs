// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace azure_proto_core
{
    public class XResourceGroup : ResourceGroupOperations
    {
        public XResourceGroup(ArmClientContext context, PhResourceGroup resource)
            : base(context, resource)
        {
            Model = resource;
        }

        public PhResourceGroup Model { get; }
    }
}
