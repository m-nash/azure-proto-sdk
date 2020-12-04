using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace azure_proto_core.Resources
{
    public class UserAssignedIdentity
    {
        public Guid ClientId { get; set; }
        public Guid PrincipalId { get; set; }

        public UserAssignedIdentity (Guid clientId, Guid principalId)
        {
            this.ClientId = clientId;
            this.PrincipalId = principalId;
        }

        public int CompareTo (UserAssignedIdentity other)
        {
            if ((this.ClientId == Guid.Empty && this.PrincipalId == Guid.Empty) && other == null)
                return 0;
            else if (this.ClientId == Guid.Empty && this.PrincipalId == Guid.Empty)
                return -1;
            else if (this.ClientId.CompareTo(other.ClientId) == 1 && this.PrincipalId.CompareTo(other.PrincipalId) == 1)
                return 1;
            else if (this.ClientId.CompareTo(other.ClientId) == 0 && this.PrincipalId.CompareTo(other.PrincipalId) == 0)
                return 0;
            else
                return -1;
        }

        public bool Equals(UserAssignedIdentity other)
        {
            if ((this.ClientId == Guid.Empty && this.PrincipalId == Guid.Empty) && other == null)
                return true;
            else if (this.ClientId == Guid.Empty && this.PrincipalId == Guid.Empty)
                return false;
            else
                return this.ClientId.Equals(other.ClientId) && this.PrincipalId.Equals(other.PrincipalId);
        }

        public static Dictionary<ResourceIdentifier, UserAssignedIdentity> Deserialize(JsonProperty property)
        {
            Dictionary<ResourceIdentifier, UserAssignedIdentity> dictionary = new Dictionary<ResourceIdentifier, UserAssignedIdentity>(); //holds useridentities
            List<string> ids = new List<string>();
            string resourceId = "";
            foreach (var property0 in property.Value.EnumerateObject())
            {
                resourceId = property0.Name;
                foreach (var property1 in property0.Value.EnumerateObject())
                {
                    if (property1.NameEquals("clientId"))
                    {
                        ids.Add(Guid.Parse(property1.Value.GetString()).ToString());
                        continue;
                    }
                    if (property1.NameEquals("principalId"))
                    {
                        ids.Add(Guid.Parse(property1.Value.GetString()).ToString());
                        continue;
                    }
                }
                UserAssignedIdentity userIds = new UserAssignedIdentity(new Guid(ids[0]), new Guid(ids[1]));
                ids.Clear();
                dictionary.Add(resourceId, userIds); //add resourceids and its corresponding struct each time we read one in 
            }
            return dictionary;
        }

        public static void Serialize(Utf8JsonWriter writer, Dictionary<ResourceIdentifier, UserAssignedIdentity> userAssignedIdentities)
        {
            if (Optional.IsCollectionDefined(userAssignedIdentities.AsEnumerable()))
            {
                writer.WritePropertyName("userAssignedIdentities");
                writer.WriteStartObject();
                foreach (var item in userAssignedIdentities)
                {
                    writer.WritePropertyName(item.Key);
                    writer.WriteStartObject();
                    UserAssignedIdentity clientAndPrincipalId = item.Value;
                    writer.WritePropertyName("clientId");
                    writer.WriteStringValue(clientAndPrincipalId.ClientId);
                    writer.WritePropertyName("principalId");
                    writer.WriteStringValue(clientAndPrincipalId.PrincipalId);
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
            }
        }

        public static bool EqualsUserAssignedIdentities(Dictionary<ResourceIdentifier, UserAssignedIdentity> original, Dictionary<ResourceIdentifier, UserAssignedIdentity> other)
        {
            if (original == null && other == null)
                return true;
            else if ((original == null && other != null) || (original != null && other == null))
                return false;
            else if (original.Count != other.Count)
                return false;
            else if (Object.ReferenceEquals(original, other))
                return true;
            else
            {
                foreach (KeyValuePair<ResourceIdentifier, UserAssignedIdentity> id in original)
                {
                    UserAssignedIdentity value;
                    if (other.TryGetValue(id.Key, out value))
                    {
                        if (!id.Value.Equals(value))
                            return false;
                    }
                    else
                        return false;
                }
                return true;
            }
        }        
    }
}
