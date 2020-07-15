using System;
using System.Collections.Generic;

namespace NetSDKProto
{

    public interface IEntityResource
    {
        string ETag { get; set; }
    }

    public abstract class ResourceReference : IEquatable<ResourceReference>, IEquatable<string>
    {
        public abstract ResourceIdentifier Id { get; set; }

        public bool Equals(ResourceReference other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(string other)
        {
            throw new NotImplementedException();
        }
    }

    public abstract class TrackedResource: ResourceReference
    {
        public abstract IDictionary<string, string> Tags { get; set; }

        public abstract Location Location { get; set; }
    }

    public abstract class Resource : TrackedResource
    {
        public abstract Sku Sku { get; set; }

        public abstract Plan Plan { get; set; }

        public abstract string Kind { get; set; }

        public abstract string ManagedBy { get; set; }

        public abstract IList<Identity> Identity { get; set; }
    }

    public class Sku : IEquatable<Sku>
    {
        public string Name { get; set; }
        public string Tier { get; set; }
        public string Family { get; set; }
        public string Size { get; set; }
        public int? Capacity { get; set; }

        public bool Equals(Sku other)
        {
            throw new NotImplementedException();
        }
    }

    public class Plan : IEquatable<Plan>
    {
        public string Name { get; set; }
        public string Publisher { get; set; }
        public string Product { get; set; }
        public string PromotionCode { get; set; }
        public string Version { get; set; }

        public bool Equals(Plan other)
        {
            throw new NotImplementedException();
        }
    }

    public class Identity : IEquatable<Identity>
    {
        public string TenantId { get; set; }

        public string PrincipalId { get; set; }

        public string ClientId { get; set; }

        public string ResourceId { get; set; }

        public string Kind { get; set; }

        public bool Equals(Identity other)
        {
            throw new NotImplementedException();
        }
    }



    public abstract class ResourceIdentifier : IEquatable<ResourceIdentifier>, IEquatable<string>, IComparable<string>, IComparable<ResourceIdentifier>
    {
        public abstract string Id { get; set; }
        public abstract string Name { get; set; }
        public abstract string Type { get; set; }
        public abstract ResourceIdentifier Parent { get; set; }
        public abstract string Tenant { get; set; }
        public abstract string Subscription { get; set; }
        public abstract string ResourceGroup { get; set; }

        public int CompareTo(string other)
        {
            throw new NotImplementedException();
        }

        public int CompareTo(ResourceIdentifier other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(ResourceIdentifier other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return Id;
        }
    }

    public struct Location : IEquatable<Location>, IEquatable<string>
    {
        public static Location WestUS = new Location { Name = "WestUS", CanonicalName = "west-us", DisplayName = "West US"};
        public string Name { get; set; }
        public string CanonicalName { get; set; }
        public string DisplayName { get; set; }

        public bool Equals(Location other)
        {
            throw new NotImplementedException();
        }

        public bool Equals(string other)
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return CanonicalName;
        }

        static string GetCanonicalName(string name)
        {
            return name;
        }

        static string GetDisplayName( string name)
        {
            return name;
        }
    }



}
