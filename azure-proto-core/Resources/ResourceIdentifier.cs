using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace azure_proto_core
{
    /// <summary>
    /// Canonical Representation of a Resource Identity
    /// TODO: Implement operator overloads for equality, comparison, and coercion
    /// TODO: Implement parsing
    /// </summary>
    public class ResourceIdentifier : IEquatable<ResourceIdentifier>, IEquatable<string>, IComparable<string>, IComparable<ResourceIdentifier>
    {
        public static class KnownKeys
        {
            public static string Subscription => "subscriptions";
            public static string Tenant => "tenants";
            public static string ResourceGroup => "resourcegroups";
            public static string Location => "locations";
            public static string ProviderNamespace => "providers";
            public static string UntrackedSubResource => Guid.NewGuid().ToString();
        }

        IDictionary<string, string> _partsDictionary = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public ResourceIdentifier(string id)
        {
            Id = id;
            //TODO: Due to the implicit this is called for blank constructions such as new PhResourceGroup
            if (id != null)
                Parse(id);
        }

        public string Id { get; protected set; }
        public string Name { get; protected set; }
        public ResourceType Type { get; protected set; }

        public string Subscription => _partsDictionary.ContainsKey(KnownKeys.Subscription) ? _partsDictionary[KnownKeys.Subscription] : null;

        public string ResourceGroup => _partsDictionary.ContainsKey(KnownKeys.ResourceGroup) ? _partsDictionary[KnownKeys.ResourceGroup] : null;

        /// <summary>
        /// Currently this will contain the identifier for either the parent resource, the resource group, the location, the subscription, 
        /// or the tenant that is the logical parent of this resource
        /// </summary>
        public ResourceIdentifier Parent { get; protected set; }

        /// <summary>
        /// Allow static, safe comparisons of resource identifier strings or objects
        /// </summary>
        /// <param name="x">A resource id</param>
        /// <param name="y">A resource id</param>
        /// <returns>true if the resource ids are equivalent, otherwise false</returns>
        public static bool Equals([AllowNull] ResourceIdentifier x, [AllowNull]ResourceIdentifier y)
        {
            if (null == x && null == y) return true;
            if (null == x || null == y) return false;
            return x.Equals(y);
        }

        /// <summary>
        /// Allow static null-safe comparisons between resource identifier strings or objects
        /// </summary>
        /// <param name="x">A resource id</param>
        /// <param name="y">A resource id</param>
        /// <returns>-1 if x &lt; y, 0 if x == y, 1 if x &gt; y</returns>
        public static int CompareTo([AllowNull]ResourceIdentifier x, [AllowNull]ResourceIdentifier y)
        {
            if (null == x && null == y) return 0;
            if (null == x) return -1;
            if (null == y) return 1;
            return x.CompareTo(y);
        }

        public virtual int CompareTo(string other)
        {
            return string.Compare(Id?.ToLowerInvariant(), other?.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual int CompareTo(ResourceIdentifier other)
        {
            return string.Compare(Id?.ToLowerInvariant(), other?.Id?.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual bool Equals(ResourceIdentifier other)
        {
            return string.Equals(Id?.ToLowerInvariant(), other?.Id?.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual bool Equals(string other)
        {
            return string.Equals(Id?.ToLowerInvariant(), other?.ToLowerInvariant(), StringComparison.InvariantCultureIgnoreCase);
        }

        public override string ToString()
        {
            return Id;
        }

        /// <summary>
        /// Populate Resource Identity fields from input string
        /// </summary>
        /// <param name="id">A properly formed resource identity</param>
        protected virtual void Parse(string id)
        {
            if (string.IsNullOrWhiteSpace(id) || !id.Contains('/'))
            {
                throw new ArgumentOutOfRangeException($"'{id}' is not a valid resource");
            }

            var parts = id.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
            if (parts.Count < 2)
            {
                throw new ArgumentOutOfRangeException($"'{id}' is not a valid resource");
            }

            if (!(KnownKeys.Subscription.Equals(parts[0], StringComparison.InvariantCultureIgnoreCase)
               || KnownKeys.Tenant.Equals(parts[0], StringComparison.InvariantCultureIgnoreCase)
               || KnownKeys.Location.Equals(parts[0], StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentOutOfRangeException($"'{id}' is not a valid resource");
            }

            Type = new ResourceType(id);


            if (parts.Count % 2 != 0)
            {
                _partsDictionary.Add(KnownKeys.UntrackedSubResource, parts.Last());
                parts.RemoveAt(parts.Count - 1);
            }

            if (id.ToLowerInvariant().Contains("providers"))
            {
                ParseProviderResource(parts);
            }
            else
            {
                ParseGenericResource(parts);
            }
        }

        protected virtual void ParseGenericResource(IList<string> parts)
        {
            Debug.Assert(parts != null);
            Debug.Assert(parts.Count > 1);
            for(int i = 0; i < parts.Count - 1; i += 2)
            {
                _partsDictionary.Add(parts[i], parts[i + 1]);
            }

            Name = parts.Last();
            parts.RemoveAt(parts.Count-1);
            parts.RemoveAt(parts.Count-1);
            Parent = parts.Count > 1 ? new ResourceIdentifier($"/{string.Join('/', parts)}") : null;
        }

        protected virtual void ParseProviderResource(IList<string> parts)
        {
            for (int i = 0; i < parts.Count - 1; i += 2)
            {
                _partsDictionary.Add(parts[i], parts[i + 1]);
            }

            Name = parts.Last();
            parts.RemoveAt(parts.Count-1);
            if (parts.Count % 2 == 1)
            {
                parts.RemoveAt(parts.Count-1);
            }
            if (parts.Count > 2 && string.Equals(parts[parts.Count - 2], KnownKeys.ProviderNamespace))
            {
                parts.RemoveAt(parts.Count-1);
                parts.RemoveAt(parts.Count-1);
            }

            Parent = parts.Count > 1 ? new ResourceIdentifier($"/{string.Join('/', parts)}") : null;
        }

        public static implicit operator string(ResourceIdentifier other) => other.Id;
        public static implicit operator ResourceIdentifier(string other) => new ResourceIdentifier( other);
    }
}
