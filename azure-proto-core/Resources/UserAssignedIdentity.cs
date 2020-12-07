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

        public static UserAssignedIdentity Deserialize(JsonElement element)
        {
            Optional<Guid> clientId = default;
            Optional<Guid> principalId = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("clientId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        principalId = Guid.Empty;
                        continue;
                    }
                    principalId = Guid.Parse(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("principalId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        principalId = Guid.Empty;
                        continue;
                    }

                    principalId = Guid.Parse(property.Value.GetString());
                    continue;
                }
            }

            return new UserAssignedIdentity(clientId, principalId);
        }

        public static void Serialize(Utf8JsonWriter writer, IDictionary<ResourceIdentifier, UserAssignedIdentity> userAssignedIdentities)
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

        public static bool EqualsUserAssignedIdentities(IDictionary<ResourceIdentifier, UserAssignedIdentity> original, IDictionary<ResourceIdentifier, UserAssignedIdentity> other)
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
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }

                return true;
            }
        }
    }
}
