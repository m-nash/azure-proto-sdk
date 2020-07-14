using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_sdk
{
    public abstract class AzureResource<P, T>
    {
        public T Model { get; private set; }

        public P Parent { get; private set; }

        public AzureResource(P parent, T model)
        {
            Parent = parent;
            Model = model;
        }
    }
}
