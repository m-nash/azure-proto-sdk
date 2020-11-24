using System;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: follow the full guidelines for these immutable types (IComparable, IEquatable, operator overloads, etc.)
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

        /* TO DO:
         * Detect the type of name using an aux method
         * Set the other names depending on the name given
         * 
         */


        public Location(string location)
        {
            DetectNameType(location);
        }

        /// <summary>
        /// Detects if the strin given matches either the Name, Canonical Name or Display Name.
        /// </summary>
        /// <param name="other"></param>
        private void DetectNameType(string location) 
        {   
            string namePattern      = "^[A-Z][a-z]*([A-Z][A-z]*)*[1-9]?$";
            string canonicalPattern = "^[a-z]+(-[a-z]+)*(-[1-9])?$";
            string displayPattern   = "^[A-Z][a-z]*( [A-Z][A-z]*)*( [1-9])?$";

            if (Regex.IsMatch(location, namePattern))
            {
                Name = location;
                CanonicalName = GetCanonicalName(location);
                DisplayName = GetDisplayName(location);
            }
            else if (Regex.IsMatch(location, canonicalPattern))
            {
                Name = GetDefaultName(location);
                CanonicalName = location;
                DisplayName = GetDisplayName(location);
            }
            else if (Regex.IsMatch(location, displayPattern))
            {
                Name = GetDefaultName(location);
                CanonicalName = GetCanonicalName(location);
                DisplayName = location;
            }
            else
            {
                // Default?
                // TROW AN ARGUMENT EX
                Name = GetDefaultName(location);
                CanonicalName = GetCanonicalName(location);
                DisplayName = GetDisplayName(location);
            }
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

        public int CompareTo(Location other)
        {
            if (ReferenceEquals(other,null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }

        public int CompareTo(string other)
        {
            if (ReferenceEquals(other,null))
            {
                return 1;
            }
            return Name.CompareTo(other);
        }

        /// <summary>
        /// </summary>
        /// <param name="other"></param>
        public static implicit operator string(Location other) => other.DisplayName;
        public static implicit operator Location(string other) => new Location( other);
    }
}

