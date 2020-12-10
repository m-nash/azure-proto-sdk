using Azure.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;

namespace azure_proto_core.Resources
{
    public class UserAssignedIdentity
    {
        public Guid? ClientId { get; set; }

        public Guid? PrincipalId { get; set; }

        public UserAssignedIdentity() { }

        public UserAssignedIdentity(Guid clientId, Guid principalId)
        {
            ClientId = clientId;
            PrincipalId = principalId;
        }

        public int CompareTo(UserAssignedIdentity other)
        {
            if ((ClientId.HasValue == false && PrincipalId.HasValue == false) && other == null)
            {
                return 0;
            }
            else if (ClientId.HasValue == false && PrincipalId.HasValue == false)
            {
                return -1;
            }
            else if (ClientId.Value.CompareTo(other.ClientId.Value) == 1 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 1)
            {
                return 1;
            }
            else if (ClientId.Value.CompareTo(other.ClientId.Value) == 0 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 0)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }

        public bool Equals(UserAssignedIdentity other)
        {
            if ((ClientId.HasValue == false && PrincipalId.HasValue == false) && other == null)
            {
                return true;
            }
            else if (ClientId.HasValue == false && PrincipalId.HasValue == false)
            {
                return false;
            }
            else
            {
                return (ClientId == other.ClientId) && (PrincipalId == other.PrincipalId.Value);
            }
        }

        public static UserAssignedIdentity Deserialize(JsonElement element)
        {
            Optional<Guid> clientId = default;
            Optional<Guid> principalId = default;
            bool isPrincipalIdNull = false;
            bool isClientIdNull = false;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("clientId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        isClientIdNull = true;
                        continue;
                    }
                    clientId = Guid.Parse(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("principalId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        isPrincipalIdNull = true;
                        continue;
                    }
                    principalId = Guid.Parse(property.Value.GetString());
                    continue;
                }
            }

            if (isPrincipalIdNull && isClientIdNull)
                return null;
            else if ((isPrincipalIdNull && !isClientIdNull) || (!isPrincipalIdNull && isClientIdNull))
                throw new InvalidOperationException("Either ClientId or PrincipalId were null");
            else
                return new UserAssignedIdentity(clientId, principalId);
        }

        public static void Serialize(Utf8JsonWriter writer, UserAssignedIdentity userAssignedIdentity)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("clientId");
            if (!Optional.IsDefined(userAssignedIdentity.ClientId))
            {
                writer.WriteStringValue("null");
            }
            else
            {
                writer.WriteStringValue(userAssignedIdentity.ClientId.ToString());
            }
            writer.WritePropertyName("principalId");
            if (!Optional.IsDefined(userAssignedIdentity.PrincipalId))
            {
                writer.WriteStringValue("null");
            }
            else
            {
                writer.WriteStringValue(userAssignedIdentity.PrincipalId.ToString());
            }

            writer.WriteEndObject();
            writer.Flush();
        }

        public static bool EqualsUserAssignedIdentities(IDictionary<ResourceIdentifier, UserAssignedIdentity> original, IDictionary<ResourceIdentifier, UserAssignedIdentity> other)
        {
            if (original == null && other == null)
            {
                return true;
            }
            else if ((original == null && other != null) || (original != null && other == null))
            {
                return false;
            }
            else if (original.Count != other.Count)
            {
                return false;
            }
            else if (ReferenceEquals(original, other))
            {
                return true;
            }
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
