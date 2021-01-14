// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    public class ArmResource : ArmResourceOperations
    {
        public ArmResource(ArmResourceOperations operations, ArmResourceData resource)
            : base(operations, resource.Id)
        {
            Data = resource;
        }

        public ArmResourceData Data { get; }

        private protected override ArmResource GetResource()
        {
            return this;
        }
    }
}
