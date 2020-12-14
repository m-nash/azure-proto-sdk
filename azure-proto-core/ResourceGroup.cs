﻿namespace azure_proto_core
{
    public class ResourceGroup : ResourceGroupOperations
    {
        internal ResourceGroup(ArmClientContext context, ResourceGroupData resource, ArmClientOptions options)
            : base(context, resource, options)
        {
            Data = resource;
        }

        public ResourceGroupData Data { get; private set; }
    }
}