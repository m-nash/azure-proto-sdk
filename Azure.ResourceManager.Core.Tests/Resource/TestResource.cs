using System;
using System.Collections.Generic;
using System.Text;

namespace Azure.ResourceManager.Core.Tests
{
    public class ResourceTest : Resource
    {
        public ResourceTest(string id)
        {
            Id = id;
        }

        public override ResourceIdentifier Id { get; protected set; }
    }
}
