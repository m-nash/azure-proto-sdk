// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Linq;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Structure respresenting a resource type
    ///     TODO: Fill in comparison methods and comparison, equality, and coercion operator overloads
    /// </summary>
    public class ResourceType : IEquatable<ResourceType>, IEquatable<string>, IComparable<ResourceType>,
        IComparable<string>
    {
        /// <summary>
        ///     The "none" resource type
        /// </summary>
        public static readonly ResourceType None = new ResourceType { Namespace = string.Empty, Type = string.Empty };

        public ResourceType(string resourceIdOrType)
        {
            Parse(resourceIdOrType);
        }

        private ResourceType()
        {
        }

        public string Namespace { get; private set; }

        public string Type { get; private set; }

        public ResourceType Parent
        {
            get
            {
                var parts = Type.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                if (parts.Length < 2)
                    return None;

                var list = new List<string>(parts);
                list.RemoveAt(list.Count - 1);

                return new ResourceType($"{Namespace}/{string.Join("/", list.ToArray())}");
            }
        }

        public static implicit operator ResourceType(string other)
        {
            return new ResourceType(other);
        }

        public static bool operator ==(ResourceType source, string target)
        {
            return source.Equals(target);
        }

        public static bool operator ==(string source, ResourceType target)
        {
            return target.Equals(source);
        }

        public static bool operator ==(ResourceType source, ResourceType target)
        {
            return source.Equals(target);
        }

        public static bool operator !=(ResourceType source, string target)
        {
            return !source.Equals(target);
        }

        public static bool operator !=(string source, ResourceType target)
        {
            return !target.Equals(source);
        }

        public static bool operator !=(ResourceType source, ResourceType target)
        {
            if (object.ReferenceEquals(source, null))
                return false;
            return !source.Equals(target);
        }

        public int CompareTo(ResourceType other)
        {
            if (other == null)
                return 1;

            if (ReferenceEquals(this, other))
                return 0;

            int compareResult = 0;
            if ((compareResult = string.Compare(Namespace, other.Namespace, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (compareResult = string.Compare(Type, other.Type, StringComparison.InvariantCultureIgnoreCase)) == 0 &&
                (other.Parent != null))
            {
                return Parent.CompareTo(other.Parent);
            }

            return compareResult;
        }

        public int CompareTo(string other)
        {
            return CompareTo(new ResourceType(other));
        }

        public bool Equals(ResourceType other)
        {
            return string.Equals(ToString(), other.ToString(), StringComparison.InvariantCultureIgnoreCase);
        }

        public bool Equals(string other)
        {
            return string.Equals(ToString(), other, StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return $"{Namespace}/{Type}";
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
                return false;

            var resourceObj = obj as ResourceType;

            if (resourceObj != null)
                return Equals(resourceObj);

            var stringObj = obj as string;

            if (stringObj != null)
                return Equals(stringObj);

            return base.Equals(obj);
        }

        public override int GetHashCode()
        {
            return ToString().GetHashCode();
        }

        internal void Parse(string resourceIdOrType)
        {
            // Note that this code will either parse a resource id to find the type, or a resource type
            resourceIdOrType = resourceIdOrType?.Trim('/');

            // exclude null or empty strings
            if (string.IsNullOrWhiteSpace(resourceIdOrType))
                throw new ArgumentOutOfRangeException(nameof(resourceIdOrType));

            // split the path into segments
            var parts = resourceIdOrType.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // There must be at least a namespace and type name
            if (parts.Count < 1)
                throw new ArgumentOutOfRangeException(nameof(resourceIdOrType));

            // if the type is just subscriptions, it is a built-in type in the Microsoft.Resources namespace
            if (parts.Count == 1)
            {
                // Simple resource type
                Type = parts[0];
                Namespace = "Microsoft.Resources";
            }

            // Handle resource identifiers from RPs (they have the /providers path segment)
            if (parts.Contains(ResourceIdentifier.KnownKeys.ProviderNamespace))
            {
                // it is a resource id from a provider
                var index = parts.LastIndexOf(ResourceIdentifier.KnownKeys.ProviderNamespace);
                for (var i = index; i >= 0; --i)
                {
                    parts.RemoveAt(i);
                }

                if (parts.Count < 3)
                    throw new ArgumentOutOfRangeException(nameof(resourceIdOrType), "Invalid resource id.");

                var type = new List<string>();
                for (var i = 1; i < parts.Count; i += 2)
                {
                    type.Add(parts[i]);
                }

                Namespace = parts[0];
                Type = string.Join("/", type);
            }

            // Handle resource types (Micsrsoft.Compute/virtualMachines, Microsoft.Network/virtualNetworks/subnets)
            else if (parts[0].Contains('.'))
            {
                // it is a full type name
                Namespace = parts[0];
                Type = string.Join("/", parts.Skip(Math.Max(0, 1)).Take(parts.Count() - 1));
            }

            // Handle built-in resource ids (e.g. /subscriptions/{sub}, /subscriptions/{sub}/resourceGroups/{rg})
            else if (parts.Count % 2 == 0)
            {
                // primitive resource manager resource id
                Namespace = "Microsoft.Resources";
                Type = parts[parts.Count - 2];
            }
            else
            {
                throw new ArgumentOutOfRangeException(nameof(resourceIdOrType));
            }
        }
    }
}
