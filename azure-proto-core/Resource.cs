using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace azure_proto_core
{

    /// <summary>
    /// Represents a resource that supports entity tags.  We *may* want to make this a separate type
    /// </summary>
    public interface IEntityResource
    {
        string ETag { get; }
    }

    /// <summary>
    /// Resource that uses MSI
    /// </summary>
    public interface IManagedIdentity
    {
        IList<Identity> Identity { get; set; }
    }

    /// <summary>
    /// Resource managed by another resource
    /// </summary>
    public interface IManagedByResource
    {
        string ManagedBy { get; set; }
    }


    /// <summary>
    /// Resource that uses SKU
    /// TODO: Note that it is unclear whether we get value from these interfaces, using these as a placeholder, this
    /// could just be generated code with supporting types for entities like 'plan'.  The determinitive factor should be
    /// if the additional type structure enables a desirable generic treatment of resources.  Initial thought is that 
    /// Managed Identity, Private Link and Entity Tags would beneft fromt he extra OM here, but that ManagedBy and Sku do not.
    /// </summary>
    public interface ISkuResource
    {
        Sku Sku { get; set; }
        Plan Plan { get; set; }
        string Kind { get; set; }
    }

    /// <summary>
    /// Terminus of a private link - design TBD
    /// </summary>
    public interface IPrivateEndpointProvider
    {

    }

    /// <summary>
    /// Base resource type: All resources have these properties. Proxy and other untracked resources should extend this class
    /// TODO: Implement comparison, equality, and type coercion operator overloads
    /// TODO: Do we need to reimplement generic comparison and operator overloads for extending types?
    /// </summary>
    public abstract class Resource : IEquatable<Resource>, IEquatable<string>, IComparable<Resource>, IComparable<string>
    {
        public abstract ResourceIdentifier Id { get; protected set; }

        public virtual string Name => Id?.Name;

        public virtual ResourceType Type => Id?.Type;

        public virtual int CompareTo([AllowNull] Resource other)
        {
            return string.Compare(Id?.Id, other?.Id);
        }

        public virtual int CompareTo([AllowNull] string other)
        {
            return string.Compare(Id?.Id, other);
        }


        public virtual bool Equals([AllowNull] Resource other)
        {
            if (Id == null)
            {
                return false;
            }

            return (Id.Equals(other?.Id));
        }

        public virtual bool Equals([AllowNull] string other)
        {
            if (Id == null)
            {
                return false;
            }

            return (Id.Equals(other));
        }
    }

    /// <summary>
    /// Generic representation of a tracked resource.  All tracked resources should extend this class
    /// </summary>
    public abstract class TrackedResource: Resource
    {
        public IDictionary<string, string> Tags => new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);

        public Location Location { get; protected set; }
    }

    /// <summary>
    /// TODO: Remove this class after generation updates
    /// </summary>
    /// <typeparam name="T">The type of the underlying model this class wraps</typeparam>
    public abstract class TrackedResource<T> : TrackedResource where T : class
    {
        protected TrackedResource(ResourceIdentifier id, Location location, T data)
        {
            Id = id;
            Location = location;
            Data = data;
        }

        public override ResourceIdentifier Id { get; protected set; }

        public T Data {get; set;}
    }


    /// <summary>
    /// Generic representation of an ARM resource.  Resources in the ARM RP should extend this resource.
    /// </summary>
    public class ArmResource : TrackedResource, IManagedByResource, ISkuResource
    {
        public ArmResource(ResourceIdentifier id)
        {
            Id = id;
            Location = Location.Default;
        }

        public ArmResource(ResourceIdentifier id, Location location)
        {
            Id = id;
            Location = location;
        }

        public override ResourceIdentifier Id { get; protected set; }
        public string ManagedBy { get; set; }
        public Sku Sku { get; set; }
        public Plan Plan { get; set; }
        public string Kind { get; set; }
    }

    /// <summary>
    /// Representaion of ARM SKU
    /// TODO: Implement comparison methods and operator overloads
    /// </summary>
    public class Sku : IEquatable<Sku>, IComparable<Sku>
    {
        public string Name { get; set; }
        public string Tier { get; set; }
        public string Family { get; set; }
        public string Size { get; set; }
        public long? Capacity { get; set; }

        public int CompareTo([AllowNull] Sku other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] Sku other)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Representation of a publisher plan for marketplace RPs.
    /// TODO: Implement Comparison operators and overloads
    /// </summary>
    public class Plan : IEquatable<Plan>, IComparable<Plan>
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Product { get; set; }
        public string PromotionCode { get; set; }
        public string Version { get; set; }

        public int CompareTo([AllowNull] Plan other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] Plan other)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Represents a managed identity
    /// TODO: fill in properties, implement comparison and equality methods and operator overloads
    /// </summary>
    public class Identity : IEquatable<Identity>, IComparable<Identity>
    {
        public Guid TenantId { get; set; }

        public Guid PrincipalId { get; set; }

        public Guid ClientId { get; set; }

        public ResourceIdentifier ResourceId { get; set; }

        public IdentityKind Kind { get; set; }

        public int CompareTo([AllowNull] Identity other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] Identity other)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Extensible representation of immutable identity kind.
    /// TODO: Implement comparison methods and standard operator overloads for immutable types
    /// </summary>
    public struct IdentityKind : IEquatable<IdentityKind>, IEquatable<string>, IComparable<IdentityKind>, IComparable<string>
    {
        public static IdentityKind SystemAssigned = new IdentityKind("SystemAssigned");
        public static IdentityKind UserAssigned = new IdentityKind("UserAssigned");
        public IdentityKind(string kind)
        {
            Value = kind;
        }

        public string Value { get; private set; }
        public int CompareTo([AllowNull] IdentityKind other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo([AllowNull] string other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] IdentityKind other)
        {
            throw new NotImplementedException();
        }

        public bool Equals([AllowNull] string other)
        {
            throw new NotImplementedException();
        }
    }

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
                return;
            }

            Type = new ResourceType(id);
            var parts = id.Split('/', StringSplitOptions.RemoveEmptyEntries).ToList();
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
            parts.RemoveAt(parts.Count);
            parts.RemoveAt(parts.Count);
            Parent = parts.Count > 1 ? new ResourceIdentifier($"/{string.Join('/', parts)}") : null;
        }

        protected virtual void ParseProviderResource(IList<string> parts)
        {
            for (int i = 0; i < parts.Count - 1; i += 2)
            {
                _partsDictionary.Add(parts[i], parts[i + 1]);
            }

            Name = parts.Last();
            parts.RemoveAt(parts.Count);
            if (parts.Count % 2 == 1)
            {
                parts.RemoveAt(parts.Count);
            }
            if (parts.Count > 2 && string.Equals(parts[parts.Count - 2], KnownKeys.ProviderNamespace))
            {
                parts.RemoveAt(parts.Count);
                parts.RemoveAt(parts.Count);
            }

            Parent = parts.Count > 1 ? new ResourceIdentifier($"/{string.Join('/', parts)}") : null;
        }

        public static implicit operator string(ResourceIdentifier other) => other.Id;
        public static implicit operator ResourceIdentifier(string other) => new ResourceIdentifier( other);
    }


    /// <summary>
    /// TODO: foolow the full guidelines for these immutable types (IComparable, IEquatable, operator overloads, etc.)
    /// </summary>
    public struct Location : IEquatable<Location>, IEquatable<string>, IComparable<Location>, IComparable<string>
    {
        public static Location Default = Location.WestUS;
        public static Location WestUS = new Location { Name = "WestUS", CanonicalName = "west-us", DisplayName = "West US"};
        public string Name { get; internal set; }
        public string CanonicalName { get; internal set; }
        public string DisplayName { get; internal set; }

        public Location(string location)
        {
            Name = GetDefaultName(location);
            CanonicalName = GetCanonicalName(location);
            DisplayName = GetDisplayName(location);
        }

        public bool Equals([AllowNull] Location other)
        {
            return CanonicalName == other.CanonicalName;
        }

        public bool Equals([AllowNull] string other)
        {
            return CanonicalName == GetCanonicalName(other);
        }

        public override string ToString()
        {
            return DisplayName;
        }

        static string GetCanonicalName( string name)
        {
            return name;
        }

        static string GetDisplayName( string name)
        {
            return name;
        }

        static string GetDefaultName(string name)
        {
            return name;
        }

        public int CompareTo([AllowNull] Location other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo([AllowNull] string other)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// TODO => Implement these and standard comparison operators for all of these immutable types
        /// </summary>
        /// <param name="other"></param>
        public static implicit operator string(Location other) => other.DisplayName;
        public static implicit operator Location(string other) => new Location( other);
    }



}
