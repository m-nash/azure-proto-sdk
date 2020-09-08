using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: foolow the full guidelines for these immutable types (IComparable, IEquatable, operator overloads, etc.)
    /// </summary>
    public class Location : IEquatable<Location>, IEquatable<string>, IComparable<Location>, IComparable<string>
    {
        public static ref readonly Location Default => ref WestUS;
        public static readonly Location WestUS = new Location { Name = "WestUS", CanonicalName = "west-us", DisplayName = "West US"};
        public string Name { get; internal set; }
        public string CanonicalName { get; internal set; }
        public string DisplayName { get; internal set; }

        internal Location()
        {
        }

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
