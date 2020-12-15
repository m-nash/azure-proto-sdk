// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace azure_proto_core
{
    /// <summary>
    ///     Represents a managed identity
    ///     TODO: fill in properties, implement comparison and equality methods and operator overloads
    /// </summary>
    public class Identity : IEquatable<Identity>, IComparable<Identity>
    {
        public Guid TenantId { get; set; }

        public Guid PrincipalId { get; set; }

        public Guid ClientId { get; set; }

        public ResourceIdentifier ResourceId { get; set; }

        public int CompareTo(Identity other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(Identity other)
        {
            throw new NotImplementedException();
        }
    }
}
