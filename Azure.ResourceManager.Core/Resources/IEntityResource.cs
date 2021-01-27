// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Represents a resource that supports entity tags.  We *may* want to make this a separate type
    /// </summary>
    public interface IEntityResource
    {
        /// <summary>
        /// Gets the etag.
        /// </summary>
        string Etag { get; }
    }
}
