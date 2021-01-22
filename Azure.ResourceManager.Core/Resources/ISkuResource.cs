// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// An interface representing a resource that has a <see cref="Sku"/>.
    /// </summary>
    public interface ISkuResource
    {
        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        Sku Sku { get; set; }

        /// <summary>
        /// Gets or sets the plan.
        /// </summary>
        Plan Plan { get; set; }

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        string Kind { get; set; }
    }
}
