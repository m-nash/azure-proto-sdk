using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class ResourceGroupCollectionBase : AzureCollection<AzureResourceGroupBase>
    {
        public ResourceGroupCollectionBase(IResource parent) : base(parent) { }

        protected override void LoadValues()
        {
            throw new NotImplementedException();
        }
    }
}
