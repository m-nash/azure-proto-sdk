// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading.Tasks;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing a generic azure resource along with the instance operations that can be performed on it.
    /// </summary>
    public class ArmResource : ArmResourceOperations
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResource"/> class.
        /// </summary>
        /// <param name="operations"> The operations object to copy the client parameters from. </param>
        /// <param name="resource"> The data model representing the generic azure resource. </param>
        internal ArmResource(ResourceOperationsBase operations, ArmResourceData resource)
            : base(operations, resource.Id)
        {
            Data = resource;
        }

        /// <summary>
        /// Gets the data representing this generic azure resource.
        /// </summary>
        public ArmResourceData Data { get; }

        /// <inheritdoc />
        protected override ArmResource GetResource()
        {
            return this;
        }

        /// <inheritdoc />
        protected override Task<ArmResource> GetResourceAsync()
        {
            return Task.FromResult(this);
        }
    }
}
