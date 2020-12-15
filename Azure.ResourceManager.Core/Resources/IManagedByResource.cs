// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Resource managed by another resource
    /// </summary>
    public interface IManagedByResource
    {
        string ManagedBy { get; set; }
    }
}
