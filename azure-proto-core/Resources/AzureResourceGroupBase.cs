using Microsoft.Azure.Management.Subscription.Models;
using Microsoft.Rest.Azure;
using System;
using System.Collections.Generic;
using System.Text;

namespace azure_proto_core
{
    public class AzureResourceGroupBase : AzureResource
    {
        public AzureResourceGroupBase(IResource parent, IModel model) : base(parent, model) { }

        public override string Name => throw new NotImplementedException();

        public override string Id => throw new NotImplementedException();
    }
}
