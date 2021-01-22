﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Base resource type: All resources have these properties. Proxy and other untracked resources should extend this
    ///     class
    ///     TODO: Implement comparison, equality, and type coercion operator overloads
    ///     TODO: What to do with properties derived from ResourceId when object is created? Should we have a special factory
    ///     for each?
    /// </summary>
    public abstract class Resource : IEquatable<Resource>, IEquatable<string>, IComparable<Resource>,
        IComparable<string>
    {
        public abstract ResourceIdentifier Id { get; protected set; }

        public virtual string Name => Id?.Name;

        public virtual ResourceType Type => Id?.Type;

        public virtual int CompareTo(Resource other)
        {
            if (other == null)
                return 1;

            if (ReferenceEquals(this, other))
                return 0;

            int compareResult = 0;
            if ((compareResult = string.Compare(Id, other.Id, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Name, other.Name, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = Type.CompareTo(other.Type)) == 0)
                return 0;

            return compareResult;
        }

        public virtual int CompareTo(string other)
        {
            return string.Compare(Id?.Id, other);
        }

        public virtual bool Equals(Resource other)
        {
            if (Id == null)
                return false;

            return Id.Equals(other?.Id);
        }

        public virtual bool Equals(string other)
        {
            if (Id == null)
                return false;

            return Id.Equals(other);
        }
    }
}
