// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace azure_proto_core
{
    /// <summary>
    ///     TODO: follow the full guidelines for these immutable types (IComparable, IEquatable, operator overloads, etc.)
    /// </summary>
    public class Location : IEquatable<Location>, IComparable<Location>
    {

        public static ref readonly Location Default => ref WestUS;

        public string Name { get; private set; }

        public string CanonicalName { get; private set; }

        public string DisplayName { get; private set; }

        // Public Azure Locations
        public static readonly Location EastAsia = new Location { Name = "eastasia", CanonicalName = "east-asia", DisplayName = "East Asia" };
        public static readonly Location SoutheastAsia = new Location { Name = "southeastasia", CanonicalName = "southeast-asia", DisplayName = "Southeast Asia" };
        public static readonly Location CentralUS = new Location { Name = "centralus", CanonicalName = "central-us", DisplayName = "Central US" };
        public static readonly Location EastUS = new Location { Name = "eastus", CanonicalName = "east-us", DisplayName = "East US" };
        public static readonly Location EastUS2 = new Location { Name = "eastus2", CanonicalName = "east-us-2", DisplayName = "East US 2" };
        public static readonly Location WestUS = new Location { Name = "westus", CanonicalName = "west-us", DisplayName = "West US" };
        public static readonly Location NorthCentralUS = new Location { Name = "northcentralus", CanonicalName = "north-central-us", DisplayName = "North Central US" };
        public static readonly Location SouthCentralUS = new Location { Name = "southcentralus", CanonicalName = "south-central-us", DisplayName = "South Central US" };
        public static readonly Location NorthEurope = new Location { Name = "northeurope", CanonicalName = "north-europe", DisplayName = "North Europe" };
        public static readonly Location WestEurope = new Location { Name = "westeurope", CanonicalName = "west-europe", DisplayName = "West Europe" };
        public static readonly Location JapanWest = new Location { Name = "japanwest", CanonicalName = "japan-west", DisplayName = "Japan West" };
        public static readonly Location JapanEast = new Location { Name = "japaneast", CanonicalName = "japan-east", DisplayName = "Japan East" };
        public static readonly Location BrazilSouth = new Location { Name = "brazilsouth", CanonicalName = "brazil-south", DisplayName = "Brazil South" };
        public static readonly Location AustraliaEast = new Location { Name = "australiaeast", CanonicalName = "australia-east", DisplayName = "Australia East" };
        public static readonly Location AustraliaSoutheast = new Location { Name = "australiasoutheast", CanonicalName = "australia-southeast", DisplayName = "Australia Southeast" };
        public static readonly Location SouthIndia = new Location { Name = "southindia", CanonicalName = "south-india", DisplayName = "South India" };
        public static readonly Location CentralIndia = new Location { Name = "centralindia", CanonicalName = "central-india", DisplayName = "Central India" };
        public static readonly Location WestIndia = new Location { Name = "westindia", CanonicalName = "west-india", DisplayName = "West India" };
        public static readonly Location CanadaCentral = new Location { Name = "canadacentral", CanonicalName = "canada-central", DisplayName = "Canada Central" };
        public static readonly Location CanadaEast = new Location { Name = "canadaeast", CanonicalName = "canada-east", DisplayName = "Canada East" };
        public static readonly Location UKSouth = new Location { Name = "uksouth", CanonicalName = "uk-south", DisplayName = "UK South" };
        public static readonly Location UKWest = new Location { Name = "ukwest", CanonicalName = "uk-west", DisplayName = "UK West" };
        public static readonly Location WestCentralUS = new Location { Name = "westcentralus", CanonicalName = "west-central-us", DisplayName = "West Central US" };
        public static readonly Location WestUS2 = new Location { Name = "westus2", CanonicalName = "west-us-2", DisplayName = "West US 2" };
        public static readonly Location KoreaCentral = new Location { Name = "koreacentral", CanonicalName = "korea-central", DisplayName = "Korea Central" };
        public static readonly Location KoreaSouth = new Location { Name = "koreasouth", CanonicalName = "korea-south", DisplayName = "Korea South" };
        public static readonly Location FranceCentral = new Location { Name = "francecentral", CanonicalName = "france-central", DisplayName = "France Central" };
        public static readonly Location FranceSouth = new Location { Name = "francesouth", CanonicalName = "france-south", DisplayName = "France South" };
        public static readonly Location AustraliaCentral = new Location { Name = "australiacentral", CanonicalName = "australia-central", DisplayName = "Australia Central" };
        public static readonly Location AustraliaCentral2 = new Location { Name = "australiacentral2", CanonicalName = "australia-central-2", DisplayName = "Australia Central 2" };
        public static readonly Location UAECentral = new Location { Name = "uaecentral", CanonicalName = "uae-central", DisplayName = "UAE Central" };
        public static readonly Location UAENorth = new Location { Name = "uaenorth", CanonicalName = "uae-north", DisplayName = "UAE North" };
        public static readonly Location SouthAfricaNorth = new Location { Name = "southafricanorth", CanonicalName = "south-africa-north", DisplayName = "South Africa North" };
        public static readonly Location SouthAfricaWest = new Location { Name = "southafricawest", CanonicalName = "south-africa-west", DisplayName = "South Africa West" };
        public static readonly Location SwitzerlandNorth = new Location { Name = "switzerlandnorth", CanonicalName = "switzerland-north", DisplayName = "Switzerland North" };
        public static readonly Location SwitzerlandWest = new Location { Name = "switzerlandwest", CanonicalName = "switzerland-west", DisplayName = "Switzerland West" };
        public static readonly Location GermanyNorth = new Location { Name = "germanynorth", CanonicalName = "germany-north", DisplayName = "Germany North" };
        public static readonly Location GermanyWestCentral = new Location { Name = "germanywestcentral", CanonicalName = "germany-west-central", DisplayName = "Germany West Central" };
        public static readonly Location NorwayWest = new Location { Name = "norwaywest", CanonicalName = "norway-west", DisplayName = "Norway West" };
        public static readonly Location BrazilSoutheast = new Location { Name = "brazilsoutheast", CanonicalName = "brazil-southeast", DisplayName = "Brazil Southeast" };

        private static Dictionary<string, Location> PublicCloudLocations = new Dictionary<string, Location>()
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

        private Location()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="location">Plain, cannonical or display name of the Location.</param>
        private Location(string location)
        {
            if (location == null)
            {
                throw new ArgumentNullException(nameof(location));
            }

            string normalizedLocation = NormalizationUtility(location);

            Location value;

            if (PublicCloudLocations.TryGetValue(normalizedLocation, out value))
            {
                Name = value.Name;
                CanonicalName = value.CanonicalName;
                DisplayName = value.DisplayName;
            }
            else
            {
                switch (DetectNameType(location))
                {
                    case NameType.Name:
                        Name = location;
                        CanonicalName = location;
                        DisplayName = location;
                        break;
                    case NameType.CanonicalName:
                        Name = GetDefaultName(location, NameType.CanonicalName);
                        CanonicalName = location;
                        DisplayName = GetDisplayName(location, NameType.CanonicalName);
                        break;
                    case NameType.DisplayName:
                        Name = GetDefaultName(location, NameType.DisplayName);
                        CanonicalName = GetCanonicalName(location, NameType.DisplayName);
                        DisplayName = location;
                        break;
                }
            }
        }

        private enum NameType
        {
            Name,
            CanonicalName,
            DisplayName,
        }

        /// <summary>
        /// </summary>
        /// <param name="other">Location object to be assigned.</param>
        public static implicit operator Location(string other)
        {
            var normalizedName = NormalizationUtility(other);
            Location value;
            if (PublicCloudLocations.TryGetValue(normalizedName, out value))
            {
                return value;
            }

            return new Location(other);
        }

        /// <summary>
        /// </summary>
        /// <param name="other">Location object to be assigned.</param>
        public static implicit operator string(Location other) => other.DisplayName;

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
            string canonicalPattern = "^[a-z]+(-[a-z]+)+(-[1-9])?$";
            string displayPattern = "^[A-Z]+[a-z]*( [A-Z]+[a-z]*)+( [1-9])?$";

            if (Regex.IsMatch(location, canonicalPattern))
            {
                return NameType.CanonicalName;
            }
            else if (Regex.IsMatch(location, displayPattern))
            {
                return NameType.DisplayName;
            }
            else
            {
                return NameType.Name;
            }
        }

        public bool Equals(Location other)
        {
            if (Name == other.Name && CanonicalName == other.CanonicalName && DisplayName == other.DisplayName)
                {
                    return true;
                }
                else
                {
                    return false;
                }
        }

        public override string ToString()
        {
            return DisplayName;
        }

        static string GetCanonicalName(string name, NameType patternType)
        {
            switch (patternType)
            {
                case NameType.DisplayName:
                    return Regex.Replace(name.ToLower(), @" ", "-");
                default:
                    return name;
            }
        }

        static string GetDisplayName(string name, NameType patternType)
        {
            switch (patternType)
            {
                case NameType.CanonicalName:
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

                    return chName.ToString();
                default:
                    return name;
            }
        }

        static string GetDefaultName(string name, NameType patternType)
        {
            switch (patternType)
            {
                case NameType.CanonicalName:
                    return Regex.Replace(name, @"-", string.Empty);
                case NameType.DisplayName:
                    return Regex.Replace(name, @" ", string.Empty).ToLower();
                default:
                    return name;
            }
        }

        public int CompareTo(Location other)
        {
            if (ReferenceEquals(other, null))
            {
                return 1;
            }
            return Name.CompareTo(other.Name);
        }
    }
}