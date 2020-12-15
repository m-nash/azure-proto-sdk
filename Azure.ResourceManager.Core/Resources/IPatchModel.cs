// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Placeholder for ARM Patch operations
    /// </summary>
    public interface IPatchModel
    {
        IDictionary<string, string> Tags { get; }
    }
}
