// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace azure_proto_core
{
    /// <summary>
    ///     Resource managed by another resource
    /// </summary>
    public interface IManagedByResource
    {
        string ManagedBy { get; set; }
    }
}
