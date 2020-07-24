using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public abstract class ResourceGroupCollectionBase : AzureCollection<AzureResourceGroupBase>
    {
        public ResourceGroupCollectionBase(TrackedResource parent) : base(parent) { }
    }
}
