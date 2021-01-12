// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Canonical Representation of a Resource Identity
    ///     TODO: Implement operator overloads for equality, comparison, and coercion
    /// </summary>
    public class ResourceIdentifier : IEquatable<ResourceIdentifier>, IEquatable<string>, IComparable<string>,
        IComparable<ResourceIdentifier>
    {
        public static readonly string ROOT = ".";
        private readonly IDictionary<string, string> _partsDictionary =
            new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public ResourceIdentifier(string id)
        {
            if (id == null)
            {
                throw new ArgumentNullException(nameof(id));
            }
            Id = id;
            Parse(id);
        }

        public string Id { get; protected set; }

        public string Name { get; protected set; }

        public ResourceType Type { get; protected set; }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public string Subscription => _partsDictionary.ContainsKey(KnownKeys.Subscription)
            ? _partsDictionary[KnownKeys.Subscription]
            : null;

        public string ResourceGroup => _partsDictionary.ContainsKey(KnownKeys.ResourceGroup)
            ? _partsDictionary[KnownKeys.ResourceGroup]
            : null;

        /// <summary>
        ///     Currently this will contain the identifier for either the parent resource, the resource group, the location, the
        ///     subscription,
        ///     or the tenant that is the logical parent of this resource
        /// </summary>
        public ResourceIdentifier Parent { get; protected set; }

        public static implicit operator string(ResourceIdentifier other)
        {
            return other.Id;
        }

        public static implicit operator ResourceIdentifier(string other)
        {
            return new ResourceIdentifier(other);
        }

        public virtual int CompareTo(ResourceIdentifier other)
        {
            return string.Compare(
                Id?.ToLowerInvariant(),
                other?.Id?.ToLowerInvariant(),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual int CompareTo(string other)
        {
            return string.Compare(
                Id?.ToLowerInvariant(),
                other?.ToLowerInvariant(),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual bool Equals(ResourceIdentifier other)
        {
            return string.Equals(
                Id?.ToLowerInvariant(),
                other?.Id?.ToLowerInvariant(),
                StringComparison.InvariantCultureIgnoreCase);
        }

        public virtual bool Equals(string other)
        {
            return string.Equals(
                Id?.ToLowerInvariant(),
                other?.ToLowerInvariant(),
                StringComparison.InvariantCultureIgnoreCase);
        }

        /// <summary>
        ///     Allow static, safe comparisons of resource identifier strings or objects
        /// </summary>
        /// <param name="x">A resource id</param>
        /// <param name="y">A resource id</param>
        /// <returns>true if the resource ids are equivalent, otherwise false</returns>
        public static bool Equals(ResourceIdentifier x, ResourceIdentifier y)
        {
            if (null == x && null == y)
                return true;

            if (null == x || null == y)
                return false;

            return x.Equals(y);
        }

        /// <summary>
        ///     Allow static null-safe comparisons between resource identifier strings or objects
        /// </summary>
        /// <param name="x">A resource id</param>
        /// <param name="y">A resource id</param>
        /// <returns>-1 if x &lt; y, 0 if x == y, 1 if x &gt; y</returns>
        public static int CompareTo(ResourceIdentifier x, ResourceIdentifier y)
        {
            if (null == x && null == y)
                return 0;

            if (null == x)
                return -1;

            if (null == y)
                return 1;

            return x.CompareTo(y);
        }

        public override string ToString()
        {
            return Id;
        }

        /// <summary>
        ///     Populate Resource Identity fields from input string
        /// </summary>
        /// <param name="id">A properly formed resource identity</param>
        protected virtual void Parse(string id)
        {
            if(id == ROOT)
            {
                Parent = null;
                return;
            }
            // Throw for null, empty, and string without the correct form
            if (string.IsNullOrWhiteSpace(id) || !id.Contains('/'))
                throw new ArgumentOutOfRangeException($"'{id}' is not a valid resource");

            // Resource ID paths consist mainly of name/value pairs. Split the uri so we have an array of name/value pairs
            var parts = id.Split(new[] { '/' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            // There must be at least one name/value pair for the resource id to be valid
            if (parts.Count < 2)
                throw new ArgumentOutOfRangeException($"'{id}' is not a valid resource");

            //This is asserting that resources must start with '/subscriptions', /tenants, or /locations.
            //TODO: we will need to update this code to accomodate tenant based resources (which start with /providers)
            if (!(KnownKeys.Subscription.Equals(parts[0], StringComparison.InvariantCultureIgnoreCase) ||
                  KnownKeys.Tenant.Equals(parts[0], StringComparison.InvariantCultureIgnoreCase) ||
                  KnownKeys.Location.Equals(parts[0], StringComparison.InvariantCultureIgnoreCase)))
            {
                throw new ArgumentOutOfRangeException($"'{id}' is not a valid resource");
            }

            Type = new ResourceType(id);

            // In the case that this resource is a singleton proxy resource, the number of parts will be odd,
            // where the last part is the type name of the singleton
            if (parts.Count % 2 != 0)
            {
                _partsDictionary.Add(KnownKeys.UntrackedSubResource, parts.Last());
                parts.RemoveAt(parts.Count - 1);
            }

            // This spplits into resource that come from a provider (which have the providers keyword) and
            // resources that are built in to ARM (e.g. /subscriptions/{sub}, /subscriptions/{sub}/resourceGroups/{rg})
            // TODO: This code will need to be updates for extension resources, which have two providers
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

            // The resource consists of well-known name-value pairs.  Make a resource dictionary
            // using the names as keys, and the values as values
            for (var i = 0; i < parts.Count - 1; i += 2)
            {
                _partsDictionary.Add(parts[i], parts[i + 1]);
            }

            // resource name is always the last part
            Name = parts.Last();
            parts.RemoveAt(parts.Count - 1);
            parts.RemoveAt(parts.Count - 1);

            // remove the last key/value pair to arrive at the parent (Count will be zero for /subscriptions/{foo})
            Parent = parts.Count > 1 ? new ResourceIdentifier($"/{string.Join("/", parts)}") : null;
        }

        protected virtual void ParseProviderResource(IList<string> parts)
        {
            // The resource consists of name/value pairs, make a dictionary out of it
            for (var i = 0; i < parts.Count - 1; i += 2)
            {
                _partsDictionary[parts[i]] = parts[i + 1];
            }

            Name = parts.Last();
            parts.RemoveAt(parts.Count - 1);

            // remove the type name (there will be no typename if this is a singleton sub resource)
            if (parts.Count % 2 == 1)
                parts.RemoveAt(parts.Count - 1);

            //If this is a top-level resource, remove the providers/Namespace pair, otherwise continue
            if (parts.Count > 2 && string.Equals(parts[parts.Count - 2], KnownKeys.ProviderNamespace))
            {
                parts.RemoveAt(parts.Count - 1);
                parts.RemoveAt(parts.Count - 1);
            }

            //If this is not a top-level resource, it will have a parent
            Parent = parts.Count > 1 ? new ResourceIdentifier($"/{string.Join("/", parts)}") : null;
        }

        public static class KnownKeys
        {
            public static string Subscription => "subscriptions";

            public static string Tenant => "tenants";

            public static string ResourceGroup => "resourcegroups";

            public static string Location => "locations";

            public static string ProviderNamespace => "providers";

            public static string UntrackedSubResource => Guid.NewGuid().ToString();
        }
    }
}
