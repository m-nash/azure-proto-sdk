using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace azure_proto_core
{
    /// <summary>
    /// Structure respresenting a resource type
    /// TODO: Fill in comparison methods and comparison, equality, and coercion operator overloads
    /// </summary>
    public class ResourceType : IEquatable<ResourceType>, IEquatable<string>, IComparable<ResourceType>, IComparable<string>
    {
        public ResourceType(string resourceIdOrType)
        {
            Parse(resourceIdOrType);
        }

        public string Namespace { get; private set; }
        public string Type { get; private set; }

        internal void Parse(string resourceIdOrType)
        {
            resourceIdOrType = resourceIdOrType?.Trim('/');
            if (string.IsNullOrWhiteSpace(resourceIdOrType))
            {
                throw new ArgumentOutOfRangeException(nameof(resourceIdOrType));
            }

            var parts = resourceIdOrType.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
            if (parts.Count < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(resourceIdOrType));
            }
            if (parts.Count == 1)
            {
                // Simple resource type
                Type = parts[0];
                Namespace = "Microsoft.Resources";
            }
            if (string.Equals(parts[0], ResourceIdentifier.KnownKeys.ProviderNamespace, StringComparison.InvariantCultureIgnoreCase))
            {
                // it is a resource id from a provider
                parts.RemoveAt(0);
                if (parts.Count < 3)
                {
                    throw new ArgumentOutOfRangeException(nameof(resourceIdOrType), "Invalid resource id.");
                }

                var type = new List<string>();
                for (int i = 1; i < parts.Count; i+= 2)
                {
                    type.Add(parts[i]);
                }

                Namespace = parts[0];
                Type = string.Join('/', type);
            }
            else if (parts[0].Contains('.'))
            {
                // it is a full type name
                Namespace = parts[0];
                Type = string.Join('/', parts.TakeLast(parts.Count - 1));
            }
            else if (parts.Count %2 == 0)
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

        public override string ToString()
        {
            return $"{Namespace}/{Type}";
        }

        public bool Equals([AllowNull] ResourceType other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] string other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo([AllowNull] ResourceType other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo([AllowNull] string other)
        {
            throw new NotImplementedException();
        }
    }
}
