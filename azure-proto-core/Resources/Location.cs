using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace azure_proto_core
{
    /// <summary>
    /// TODO: follow the full guidelines for these immutable types (IComparable, IEquatable, operator overloads, etc.)
    /// </summary>
    public class Location : IEquatable<Location>, IEquatable<string>, IComparable<Location>, IComparable<string>
    {
        private enum NameType
        {
            DisplayName,
            CanonicalName,
            Name
        }

        public static ref readonly Location Default => ref WestUS;
        //public static readonly Location WestUS = new Location { Name = "WestUS", CanonicalName = "west-us", DisplayName = "West US" };
        public string Name { get; internal set; }
        public string CanonicalName { get; internal set; }
        public string DisplayName { get; internal set; }

        internal Location()
        {
        }

        //Public Azure Locations
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

        private Dictionary<string, Location> globalPublicLocationsDict = new Dictionary<string, Location>(){
            {"eastasia", EastAsia},
            {"southeastasia", SoutheastAsia},
            {"centralus", CentralUS},
            {"eastus", EastUS},
            {"eastus2", EastUS2},
            {"westus", WestUS},
            {"northcentralus", NorthCentralUS},
            {"southcentralus", SouthCentralUS},
            {"northeurope", NorthEurope},
            {"westeurope", WestEurope},
            {"japanwest", JapanWest},
            {"japaneast", JapanEast},
            {"brazilsouth", BrazilSouth},
            {"australiaeast", AustraliaEast},
            {"australiasoutheast", AustraliaSoutheast},
            {"southindia", SouthIndia},
            {"centralindia", CentralIndia},
            {"westindia", WestIndia},
            {"canadacentral", CanadaCentral},
            {"canadaeast", CanadaEast},
            {"uksouth", UKSouth},
            {"ukwest", UKWest},
            {"westcentralus", WestCentralUS},
            {"westus2", WestUS2},
            {"koreacentral", KoreaCentral},
            {"koreasouth", KoreaSouth},
            {"francecentral", FranceCentral},
            {"francesouth", FranceSouth},
            {"australiacentral", AustraliaCentral},
            {"australiacentral2", AustraliaCentral2},
            {"uaecentral", UAECentral},
            {"uaenorth", UAENorth},
            {"southafricanorth", SouthAfricaNorth},
            {"southafricawest", SouthAfricaWest},
            {"switzerlandnorth", SwitzerlandNorth},
            {"switzerlandwest", SwitzerlandWest},
            {"germanynorth", GermanyNorth},
            {"germanywestcentral", GermanyWestCentral},
            {"norwaywest", NorwayWest},
            {"brazilsoutheast", BrazilSoutheast},
            {"east-asia", EastAsia},
            {"southeast-asia", SoutheastAsia},
            {"central-us", CentralUS},
            {"east-us", EastUS},
            {"east-us-2", EastUS2},
            {"west-us", WestUS},
            {"north-central-us", NorthCentralUS},
            {"south-central-us", SouthCentralUS},
            {"north-europe", NorthEurope},
            {"west-europe", WestEurope},
            {"japan-west", JapanWest},
            {"japan-east", JapanEast},
            {"brazil-south", BrazilSouth},
            {"australia-east", AustraliaEast},
            {"australia-southeast", AustraliaSoutheast},
            {"south-india", SouthIndia},
            {"central-india", CentralIndia},
            {"west-india", WestIndia},
            {"canada-central", CanadaCentral},
            {"canada-east", CanadaEast},
            {"uk-south", UKSouth},
            {"uk-west", UKWest},
            {"west-central-us", WestCentralUS},
            {"west-us-2", WestUS2},
            {"korea-central", KoreaCentral},
            {"korea-south", KoreaSouth},
            {"france-central", FranceCentral},
            {"france-south", FranceSouth},
            {"australia-central", AustraliaCentral},
            {"australia-central-2", AustraliaCentral2},
            {"uae-central", UAECentral},
            {"uae-north", UAENorth},
            {"south-africa-north", SouthAfricaNorth},
            {"south-africa-west", SouthAfricaWest},
            {"switzerland-north", SwitzerlandNorth},
            {"switzerland-west", SwitzerlandWest},
            {"germany-north", GermanyNorth},
            {"germany-west-central", GermanyWestCentral},
            {"norway-west", NorwayWest},
            {"brazil-southeast", BrazilSoutheast},
            {"East Asia", EastAsia},
            {"Southeast Asia", SoutheastAsia},
            {"Central US", CentralUS},
            {"East US", EastUS},
            {"East US 2", EastUS2},
            {"West US", WestUS},
            {"North Central US", NorthCentralUS},
            {"South Central US", SouthCentralUS},
            {"North Europe", NorthEurope},
            {"West Europe", WestEurope},
            {"Japan West", JapanWest},
            {"Japan East", JapanEast},
            {"Brazil South", BrazilSouth},
            {"Australia East", AustraliaEast},
            {"Australia Southeast", AustraliaSoutheast},
            {"South India", SouthIndia},
            {"Central India", CentralIndia},
            {"West India", WestIndia},
            {"Canada Central", CanadaCentral},
            {"Canada East", CanadaEast},
            {"UK South", UKSouth},
            {"UK West", UKWest},
            {"West Central US", WestCentralUS},
            {"West US 2", WestUS2},
            {"Korea Central", KoreaCentral},
            {"Korea South", KoreaSouth},
            {"France Central", FranceCentral},
            {"France South", FranceSouth},
            {"Australia Central", AustraliaCentral},
            {"Australia Central 2", AustraliaCentral2},
            {"UAE Central", UAECentral},
            {"UAE North", UAENorth},
            {"South Africa North", SouthAfricaNorth},
            {"South Africa West", SouthAfricaWest},
            {"Switzerland North", SwitzerlandNorth},
            {"Switzerland West", SwitzerlandWest},
            {"Germany North", GermanyNorth},
            {"Germany West Central", GermanyWestCentral},
            {"Norway West", NorwayWest},
            {"Brazil Southeast", BrazilSoutheast}
        };

        public Location(string location)
        {
            if (globalPublicLocationsDict.ContainsKey(location)){
                //Normalize then lookup
                Name = globalPublicLocationsDict[location].Name;
                CanonicalName = globalPublicLocationsDict[location].CanonicalName;
                DisplayName = globalPublicLocationsDict[location].DisplayName;
            }
            else
            {
                switch (DetectNameType(location))
                {
                    case -1:
                    case 0:
                        Name = location;
                        CanonicalName = location;
                        DisplayName = location;
                        break;
                    case 1:
                        Name = GetDefaultName(location, 1);
                        CanonicalName = location;
                        DisplayName = GetDisplayName(location, 1);
                        break;
                    case 2:
                        Name = GetDefaultName(location,2);
                        CanonicalName = GetCanonicalName(location,2);
                        DisplayName = location;
                        break;
                }
            }
        }

        /// <summary>
        /// Detects if the strin given matches either the Name, Canonical Name or Display Name.
        /// </summary>
        /// <param name="location"></param>
        /// <returns>0=Name | 1=CanonicalName | 2=DisplayName | -1=Not a match</returns>
        private int DetectNameType(string location) 
        {   
            //private enum

            //string namePattern      = "^[A-Z][a-z]*([A-Z][A-z]*)*[1-9]?$";
            string namePattern      = "^[a-z]*[1-9]?$";
            string canonicalPattern = "^[a-z]+(-[a-z]+)*(-[1-9])?$";
            string displayPattern   = "^[A-Z][a-z]*( [A-Z][A-z]*)*( [1-9])?$";

            if (Regex.IsMatch(location, namePattern))
            {
                return 0;
            }
            else if (Regex.IsMatch(location, canonicalPattern))
            {
                return 1;
            }
            else if (Regex.IsMatch(location, displayPattern))
            {
                return 2;
            }
            else
            {
                return -1;
            }
        }

        public bool Equals(Location other)
        {
            return CanonicalName == other.CanonicalName;
        }

        public bool Equals(string other)
        {
            switch (DetectNameType(other))
            {
                case 0:
                    return Name == other;
                case 1:
                    return CanonicalName == other;
                case 2:
                    return DisplayName == other;
                default:
                    return Name == other;
            }

            //Look at the dict
            //compare the 3 properties
            //if not, use the implicit
            
        }

        public override string ToString()
        {
            return DisplayName;
        }

        static string GetCanonicalName(string name, int patternType)
        {
            switch (patternType)
            {
                case 2:
                    return Regex.Replace(name.ToLower(), @" ", "-");
                default:
                    return name;
            }
        }

        private static string ToTitleCase(string name)
        {
            char[] chName = name.ToCharArray();
            chName[0] = Char.ToUpper(chName[0]);

            for (int i = 0; i < chName.Length; i++)
            {
                if (chName[i] == '-')
                {
                    chName[i + 1] = Char.ToUpper(chName[i + 1]);
                }
            }

            return new string(chName);
        }

        static string GetDisplayName(string name, int patternType)
        {
            switch (patternType)
            {
                case 1:
                    return Regex.Replace(ToTitleCase(name), @"-", " ");
                default:
                    return name;
            }     
        }

        static string GetDefaultName(string name, int patternType)
        {
            switch (patternType)
            {
                case 1:
                    return Regex.Replace(name, @"-", string.Empty);
                case 2:
                    return Regex.Replace(name, @" ", string.Empty).ToLower();
                default:
                    return name;
            }
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

