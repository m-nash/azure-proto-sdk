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
        IList<Identity> Identity { get; set; }
    }
}
