// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;

namespace azure_proto_core
{
    /// <summary>
    ///     Generic representation of a tracked resource.  All tracked resources should extend this class
    /// </summary>
    public abstract class TrackedResource : Resource
    {
        public virtual IDictionary<string, string> Tags =>
            new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public virtual Location Location { get; protected set; }

        public override ResourceIdentifier Id { get; protected set; }
    }
}
