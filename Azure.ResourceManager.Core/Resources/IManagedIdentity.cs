// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Resource that uses MSI
    /// </summary>
    public interface IManagedIdentity
    {
        /// <summary>
        /// Gets or sets the identity list.
        /// </summary>
        IList<Identity> Identity { get; set; }
    }
}
