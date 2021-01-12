// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Represents an Azure geography region where supported resource providers live.
    /// </summary>
    public class Location : IEquatable<Location>, IComparable<Location>
    {
        #region Public Cloud Locations

        /// <summary>
        /// Public cloud location for East Asia.
        /// </summary>
        public static readonly Location EastAsia = new Location { Name = "eastasia", CanonicalName = "east-asia", DisplayName = "East Asia" };

        /// <summary>
        /// Public cloud location for Southeast Asia.
        /// </summary>
        public static readonly Location SoutheastAsia = new Location { Name = "southeastasia", CanonicalName = "southeast-asia", DisplayName = "Southeast Asia" };

        /// <summary>
        /// Public cloud location for Central US.
        /// </summary>
        public static readonly Location CentralUS = new Location { Name = "centralus", CanonicalName = "central-us", DisplayName = "Central US" };

        /// <summary>
        /// Public cloud location for East US.
        /// </summary>
        public static readonly Location EastUS = new Location { Name = "eastus", CanonicalName = "east-us", DisplayName = "East US" };

        /// <summary>
        /// Public cloud location for East US 2.
        /// </summary>
        public static readonly Location EastUS2 = new Location { Name = "eastus2", CanonicalName = "east-us-2", DisplayName = "East US 2" };

        /// <summary>
        /// Public cloud location for West US.
        /// </summary>
        public static readonly Location WestUS = new Location { Name = "westus", CanonicalName = "west-us", DisplayName = "West US" };

        /// <summary>
        /// Public cloud location for North Central US.
        /// </summary>
        public static readonly Location NorthCentralUS = new Location { Name = "northcentralus", CanonicalName = "north-central-us", DisplayName = "North Central US" };

        /// <summary>
        /// Public cloud location for South Central US.
        /// </summary>
        public static readonly Location SouthCentralUS = new Location { Name = "southcentralus", CanonicalName = "south-central-us", DisplayName = "South Central US" };

        /// <summary>
        /// Public cloud location for North Europe.
        /// </summary>
        public static readonly Location NorthEurope = new Location { Name = "northeurope", CanonicalName = "north-europe", DisplayName = "North Europe" };

        /// <summary>
        /// Public cloud location for West Europe.
        /// </summary>
        public static readonly Location WestEurope = new Location { Name = "westeurope", CanonicalName = "west-europe", DisplayName = "West Europe" };

        /// <summary>
        /// Public cloud location for Japan West.
        /// </summary>
        public static readonly Location JapanWest = new Location { Name = "japanwest", CanonicalName = "japan-west", DisplayName = "Japan West" };

        /// <summary>
        /// Public cloud location for Japan East.
        /// </summary>
        public static readonly Location JapanEast = new Location { Name = "japaneast", CanonicalName = "japan-east", DisplayName = "Japan East" };

        /// <summary>
        /// Public cloud location for Brazil South.
        /// </summary>
        public static readonly Location BrazilSouth = new Location { Name = "brazilsouth", CanonicalName = "brazil-south", DisplayName = "Brazil South" };

        /// <summary>
        /// Public cloud location for Australia East.
        /// </summary>
        public static readonly Location AustraliaEast = new Location { Name = "australiaeast", CanonicalName = "australia-east", DisplayName = "Australia East" };

        /// <summary>
        /// Public cloud location for Australia Southeast.
        /// </summary>
        public static readonly Location AustraliaSoutheast = new Location { Name = "australiasoutheast", CanonicalName = "australia-southeast", DisplayName = "Australia Southeast" };

        /// <summary>
        /// Public cloud location for South India.
        /// </summary>
        public static readonly Location SouthIndia = new Location { Name = "southindia", CanonicalName = "south-india", DisplayName = "South India" };

        /// <summary>
        /// Public cloud location for Central India.
        /// </summary>
        public static readonly Location CentralIndia = new Location { Name = "centralindia", CanonicalName = "central-india", DisplayName = "Central India" };

        /// <summary>
        /// Public cloud location for West India.
        /// </summary>
        public static readonly Location WestIndia = new Location { Name = "westindia", CanonicalName = "west-india", DisplayName = "West India" };

        /// <summary>
        /// Public cloud location for Canada Central.
        /// </summary>
        public static readonly Location CanadaCentral = new Location { Name = "canadacentral", CanonicalName = "canada-central", DisplayName = "Canada Central" };

        /// <summary>
        /// Public cloud location for Canada East.
        /// </summary>
        public static readonly Location CanadaEast = new Location { Name = "canadaeast", CanonicalName = "canada-east", DisplayName = "Canada East" };

        /// <summary>
        /// Public cloud location for UK South.
        /// </summary>
        public static readonly Location UKSouth = new Location { Name = "uksouth", CanonicalName = "uk-south", DisplayName = "UK South" };

        /// <summary>
        /// Public cloud location for UK West.
        /// </summary>
        public static readonly Location UKWest = new Location { Name = "ukwest", CanonicalName = "uk-west", DisplayName = "UK West" };

        /// <summary>
        /// Public cloud location for West Central US.
        /// </summary>
        public static readonly Location WestCentralUS = new Location { Name = "westcentralus", CanonicalName = "west-central-us", DisplayName = "West Central US" };

        /// <summary>
        /// Public cloud location for West US 2.
        /// </summary>
        public static readonly Location WestUS2 = new Location { Name = "westus2", CanonicalName = "west-us-2", DisplayName = "West US 2" };

        /// <summary>
        /// Public cloud location for Korea Central.
        /// </summary>
        public static readonly Location KoreaCentral = new Location { Name = "koreacentral", CanonicalName = "korea-central", DisplayName = "Korea Central" };

        /// <summary>
        /// Public cloud location for Korea South.
        /// </summary>
        public static readonly Location KoreaSouth = new Location { Name = "koreasouth", CanonicalName = "korea-south", DisplayName = "Korea South" };

        /// <summary>
        /// Public cloud location for France Central.
        /// </summary>
        public static readonly Location FranceCentral = new Location { Name = "francecentral", CanonicalName = "france-central", DisplayName = "France Central" };

        /// <summary>
        /// Public cloud location for France South.
        /// </summary>
        public static readonly Location FranceSouth = new Location { Name = "francesouth", CanonicalName = "france-south", DisplayName = "France South" };

        /// <summary>
        /// Public cloud location for Australia Central.
        /// </summary>
        public static readonly Location AustraliaCentral = new Location { Name = "australiacentral", CanonicalName = "australia-central", DisplayName = "Australia Central" };

        /// <summary>
        /// Public cloud location for Australia Central 2.
        /// </summary>
        public static readonly Location AustraliaCentral2 = new Location { Name = "australiacentral2", CanonicalName = "australia-central-2", DisplayName = "Australia Central 2" };

        /// <summary>
        /// Public cloud location for UAE Central.
        /// </summary>
        public static readonly Location UAECentral = new Location { Name = "uaecentral", CanonicalName = "uae-central", DisplayName = "UAE Central" };

        /// <summary>
        /// Public cloud location for UAE North.
        /// </summary>
        public static readonly Location UAENorth = new Location { Name = "uaenorth", CanonicalName = "uae-north", DisplayName = "UAE North" };

        /// <summary>
        /// Public cloud location for South Africa North.
        /// </summary>
        public static readonly Location SouthAfricaNorth = new Location { Name = "southafricanorth", CanonicalName = "south-africa-north", DisplayName = "South Africa North" };

        /// <summary>
        /// Public cloud location for South Africa West.
        /// </summary>
        public static readonly Location SouthAfricaWest = new Location { Name = "southafricawest", CanonicalName = "south-africa-west", DisplayName = "South Africa West" };

        /// <summary>
        /// Public cloud location for Switzerland North.
        /// </summary>
        public static readonly Location SwitzerlandNorth = new Location { Name = "switzerlandnorth", CanonicalName = "switzerland-north", DisplayName = "Switzerland North" };

        /// <summary>
        /// Public cloud location for Switzerland West.
        /// </summary>
        public static readonly Location SwitzerlandWest = new Location { Name = "switzerlandwest", CanonicalName = "switzerland-west", DisplayName = "Switzerland West" };

        /// <summary>
        /// Public cloud location for Germany North.
        /// </summary>
        public static readonly Location GermanyNorth = new Location { Name = "germanynorth", CanonicalName = "germany-north", DisplayName = "Germany North" };

        /// <summary>
        /// Public cloud location for Germany West Central.
        /// </summary>
        public static readonly Location GermanyWestCentral = new Location { Name = "germanywestcentral", CanonicalName = "germany-west-central", DisplayName = "Germany West Central" };

        /// <summary>
        /// Public cloud location for Norway West.
        /// </summary>
        public static readonly Location NorwayWest = new Location { Name = "norwaywest", CanonicalName = "norway-west", DisplayName = "Norway West" };

        /// <summary>
        /// Public cloud location for Brazil Southeast.
        /// </summary>
        public static readonly Location BrazilSoutheast = new Location { Name = "brazilsoutheast", CanonicalName = "brazil-southeast", DisplayName = "Brazil Southeast" };

        #endregion
        private static readonly Dictionary<string, Location> PublicCloudLocations = new Dictionary<string, Location>()
        {
            { "EASTASIA", EastAsia },
            { "SOUTHEASTASIA", SoutheastAsia },
            { "CENTRALUS", CentralUS },
            { "EASTUS", EastUS },
            { "EASTUS2", EastUS2 },
            { "WESTUS", WestUS },
            { "NORTHCENTRALUS", NorthCentralUS },
            { "SOUTHCENTRALUS", SouthCentralUS },
            { "NORTHEUROPE", NorthEurope },
            { "WESTEUROPE", WestEurope },
            { "JAPANWEST", JapanWest },
            { "JAPANEAST", JapanEast },
            { "BRAZILSOUTH", BrazilSouth },
            { "AUSTRALIAEAST", AustraliaEast },
            { "AUSTRALIASOUTHEAST", AustraliaSoutheast },
            { "SOUTHINDIA", SouthIndia },
            { "CENTRALINDIA", CentralIndia },
            { "WESTINDIA", WestIndia },
            { "CANADACENTRAL", CanadaCentral },
            { "CANADAEAST", CanadaEast },
            { "UKSOUTH", UKSouth },
            { "UKWEST", UKWest },
            { "WESTCENTRALUS", WestCentralUS },
            { "WESTUS2", WestUS2 },
            { "KOREACENTRAL", KoreaCentral },
            { "KOREASOUTH", KoreaSouth },
            { "FRANCECENTRAL", FranceCentral },
            { "FRANCESOUTH", FranceSouth },
            { "AUSTRALIACENTRAL", AustraliaCentral },
            { "AUSTRALIACENTRAL2", AustraliaCentral2 },
            { "UAECENTRAL", UAECentral },
            { "UAENORTH", UAENorth },
            { "SOUTHAFRICANORTH", SouthAfricaNorth },
            { "SOUTHAFRICAWEST", SouthAfricaWest },
            { "SWITZERLANDNORTH", SwitzerlandNorth },
            { "SWITZERLANDWEST", SwitzerlandWest },
            { "GERMANYNORTH", GermanyNorth },
            { "GERMANYWESTCENTRAL", GermanyWestCentral },
            { "NORWAYWEST", NorwayWest },
            { "BRAZILSOUTHEAST", BrazilSoutheast },
        };

        private static readonly string CanonicalPattern = "^[a-z]+(-[a-z]+)+(-[1-9])?$";
        private static readonly string DisplayPattern = "^[A-Z]+[a-z]*( [A-Z]+[a-z]*)+( [1-9])?$";
        private static readonly string RegexDash = @"-";
        private static readonly string RegexWhitespace = @" ";

        private Location()
        {
        }

        private Location(string location)
        {
            switch (DetectNameType(location))
            {
                case NameType.Name:
                    Name = location;
                    CanonicalName = location;
                    DisplayName = location;
                    break;
                case NameType.CanonicalName:
                    Name = GetDefaultNameFromCanonicalName(location);
                    CanonicalName = location;
                    DisplayName = GetDisplayNameFromCanonicalName(location);
                    break;
                case NameType.DisplayName:
                    Name = GetDefaultNameFromDisplayName(location);
                    CanonicalName = GetCanonicalNameFromDisplayName(location);
                    DisplayName = location;
                    break;
            }
        }

        private enum NameType
        {
            Name,
            CanonicalName,
            DisplayName,
        }

        /// <summary>
        /// Gets default Location object: West US.
        /// </summary>
        public static ref readonly Location Default => ref WestUS;

        /// <summary>
        /// Gets a location name consisting of only lowercase characters without white spaces or any separation character between words, e.g. "westus".
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets a location canonical name consisting of only lowercase chararters with a '-' between words, e.g. "west-us".
        /// </summary>
        public string CanonicalName { get; private set; }

        /// <summary>
        /// Gets a location display name consisting of titlecase words or alphanumeric characters separated by whitespaces, e.g. "West US"
        /// </summary>
        public string DisplayName { get; private set; }

        /// <summary>
        /// Creates a new location implicitly from a string.
        /// </summary>
        /// <param name="other"> String to be assigned in the Name, CanonicalName or DisplayName form. </param>
        public static implicit operator Location(string other)
        {
            if (other == null)
                return null;

            var normalizedName = NormalizationUtility(other);
            Location value;
            if (PublicCloudLocations.TryGetValue(normalizedName, out value))
            {
                return value;
            }

            return new Location(other);
        }

        /// <summary>
        /// Creates a string implicitly from a Location object.
        /// </summary>
        /// <param name="other"> Location object to be assigned. </param>
        public static implicit operator string(Location other)
        {
            if (other == null)
            {
                return null;
            }

            return other.ToString();
        }

        /// <summary>
        /// Detects if a location object is equal to another location instance or a string representing the location name.
        /// </summary>
        /// <param name="other"> Location object or name as a string. </param>
        /// <returns> True or false. </returns>
        public bool Equals(Location other)
        {
            if (other == null)
                return false;

            return Name == other.Name && CanonicalName == other.CanonicalName && DisplayName == other.DisplayName;
        }

        /// <summary>
        /// Gets the display name of a location object.
        /// </summary>
        /// <returns> Display name. </returns>
        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Compares this Location name to another Location to expose if it is greater, less or equal than this one.
        /// </summary>
        /// <param name="other"> Location object or name as a string. </param>
        /// <returns> -1 for less than, 0 for equals, 1 for greater than. </returns>
        public int CompareTo(Location other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }

            return Name.CompareTo(other.Name);
        }

        private static string NormalizationUtility(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }

            var sb = new StringBuilder(value.Length);
            for (var index = 0; index < value.Length; ++index)
            {
                var c = value[index];
                if (char.IsLetterOrDigit(c))
                {
                    sb.Append(c);
                }
            }

            return sb.ToString().ToUpperInvariant();
        }

        private static NameType DetectNameType(string location)
        {
            if (Regex.IsMatch(location, CanonicalPattern))
            {
                return NameType.CanonicalName;
            }
            else if (Regex.IsMatch(location, DisplayPattern))
            {
                return NameType.DisplayName;
            }
            else
            {
                return NameType.Name;
            }
        }

        private static string GetCanonicalNameFromDisplayName(string name)
        {
            return name.Replace(RegexWhitespace, RegexDash).ToLower();
        }

        private static string GetDisplayNameFromCanonicalName(string name)
        {
            char[] chName = name.ToCharArray();
            chName[0] = char.ToUpper(chName[0]);

            for (int i = 0; i < chName.Length - 1; i++)
            {
                if (chName[i] == '-')
                {
                    chName[i] = ' ';
                    chName[i + 1] = char.ToUpper(chName[i + 1]);
                }
            }

            return new string(chName);
        }

        private static string GetDefaultNameFromCanonicalName(string name)
        {
            return name.Replace(RegexDash, string.Empty);
        }

        private static string GetDefaultNameFromDisplayName(string name)
        {
            return name.Replace(RegexWhitespace, string.Empty).ToLower();
        }
    }
}
