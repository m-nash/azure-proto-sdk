// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;

namespace azure_proto_core
{
    /// <summary>
    ///     TODO: follow the full guidelines for these immutable types (IComparable, IEquatable, operator overloads, etc.)
    /// </summary>
    public class Location : IEquatable<Location>, IEquatable<string>, IComparable<Location>, IComparable<string>
    {
        public static readonly Location WestUS = new Location
            { Name = "WestUS", CanonicalName = "west-us", DisplayName = "West US" };

        public Location(string location)
        {
            Name = GetDefaultName(location);
            CanonicalName = GetCanonicalName(location);
            DisplayName = GetDisplayName(location);
        }

        internal Location()
        {
        }

        public static ref readonly Location Default => ref WestUS;

        public string Name { get; internal set; }

        public string CanonicalName { get; internal set; }

        public string DisplayName { get; internal set; }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        public static implicit operator string(Location other)
        {
            return other.DisplayName;
        }

        public static implicit operator Location(string other)
        {
            return new Location(other);
        }

        /// <inheritdoc cref="IComparable{Location}" />
        public int CompareTo(Location other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return string.Compare(Name, other.Name);
        }

        public int CompareTo(string other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return Name.CompareTo(other);
        }

        public bool Equals(Location other)
        {
            return CanonicalName == other.CanonicalName;
        }

        public bool Equals(string other)
        {
            return CanonicalName == GetCanonicalName(other);
        }

        public override string ToString()
        {
            return DisplayName;
        }

        private static string GetCanonicalName(string name)
        {
            return name;
        }

        private static string GetDisplayName(string name)
        {
            return name;
        }

        private static string GetDefaultName(string name)
        {
            return name;
        }
    }
}
