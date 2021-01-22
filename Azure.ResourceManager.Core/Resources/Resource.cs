// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the base resource used by all azure resources.
    /// </summary>
    public abstract class Resource : IEquatable<Resource>, IEquatable<string>, IComparable<Resource>,
        IComparable<string>
    {
        /// <summary>
        /// Gets or sets the resource identifier.
        /// </summary>
        public abstract ResourceIdentifier Id { get; protected set; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public virtual string Name => Id?.Name;

        /// <summary>
        /// Gets the resource type.
        /// </summary>
        public virtual ResourceType Type => Id?.Type;

        /// <inheritdoc/>
        public virtual int CompareTo(Resource other)
        {
            return string.Compare(Id?.Id, other?.Id);
        }

        /// <inheritdoc/>
        public virtual int CompareTo(string other)
        {
            return string.Compare(Id?.Id, other);
        }

        /// <inheritdoc/>
        public virtual bool Equals(Resource other)
        {
            if (Id == null)
                return false;

            return Id.Equals(other?.Id);
        }

        /// <inheritdoc/>
        public virtual bool Equals(string other)
        {
            if (Id == null)
                return false;

            return Id.Equals(other);
        }
    }
}
