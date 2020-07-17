using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public abstract class ResourceGroupCollectionBase : AzureCollection<AzureResourceGroupBase>
    {
        public ResourceGroupCollectionBase(IResource parent) : base(parent) { }
    }
}
