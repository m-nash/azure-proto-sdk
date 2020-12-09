﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace azure_proto_core
{
    /// <summary>
    ///     Represents a resource that supports entity tags.  We *may* want to make this a separate type
    /// </summary>
    public interface IEntityResource
    {
        string Etag { get; }
    }
}
