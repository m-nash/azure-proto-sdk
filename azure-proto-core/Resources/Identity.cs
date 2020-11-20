using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text.Json;
using Azure.Core;

namespace azure_proto_core
{
    /// <summary>
    /// Represents a managed identity
    /// </summary>
    public class Identity : IEquatable<Identity>, IComparable<Identity>, IUtf8JsonSerializable
    {
        public Guid TenantId { get; set; }

        public Guid PrincipalId { get; set; }

        public Guid? ClientId { get; set; }

        public ResourceIdentifier ResourceId { get; set; }

        public IdentityKind Kind { get; set; }

        public Dictionary<string, UserClientAndPrincipalId> UserAssignedIdentities { get; set; } //maintain structure of {id, (clientid, principal id)} in case of multiple UserIdentities

        public struct UserClientAndPrincipalId //make sure userIdentities are always populated
        {
            string clientId { get; set; }
            string principalId { get; set; }

            public UserClientAndPrincipalId(string clientId, string principalId)
            {
                this.clientId = clientId;
                this.principalId = principalId;
            }
        }

        internal Identity(Guid tenantId, Guid principalId, Guid? clientId, string resourceId, IdentityKind kind, Optional<Dictionary<string, UserClientAndPrincipalId>> userAssignedIdentities)
        {
            PrincipalId = principalId;
            TenantId = tenantId;
            ClientId = clientId;
            ResourceId = resourceId;
            Kind = kind;
            UserAssignedIdentities = userAssignedIdentities;
        }

        public Identity()
        {
        }

        public int CompareTo(Identity other)
        {
            if (Object.ReferenceEquals(null, other))
                return 1;
            return this.ResourceId.CompareTo(other.ResourceId);
        }

        public bool Equals(Identity other)
        {
            if (Object.ReferenceEquals(null, other))
                return false;
            else if (this.ResourceId == null && other.ResourceId == null)
                return (this.TenantId.Equals(other.TenantId) &&
                this.PrincipalId.Equals(other.PrincipalId) &&
                this.ClientId.Equals(other.ClientId) &&
                this.Kind.Equals(other.Kind));
            else if (other.ResourceId == null)
                return false;
            else
                return (this.TenantId.Equals(other.TenantId) &&
                this.PrincipalId.Equals(other.PrincipalId) &&
                this.ClientId.Equals(other.ClientId) &&
                this.ResourceId.Equals(other.ResourceId) && //can be null
                this.Kind.Equals(other.Kind));
        }

        public static Identity DeserializeIdentity(JsonElement element)
        {
            Optional<Guid> principalId = default;
            Optional<Guid> tenantId = default;
            Optional<Guid?> clientId = default;
            Optional<IdentityKind> type = default;
            Optional<string> resourceId = default;
            Optional<Dictionary<string, UserClientAndPrincipalId>> userAssignedIdentities = default;
            foreach (var property in element.EnumerateObject())
            {
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
                if (property.NameEquals("tenantId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        tenantId = Guid.Empty;
                        continue;
                    }
                    tenantId = Guid.Parse(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("clientId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        clientId = Guid.Empty;
                        continue;
                    }
                    clientId = Guid.Parse(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("id"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        resourceId = null;
                        continue;
                    }
                    resourceId = property.ToString();
                    continue;
                }
                if (property.NameEquals("type"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    type = new IdentityKind(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("userAssignedIdentities"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        userAssignedIdentities = null;
                        continue;
                    }
                    Dictionary<string, UserClientAndPrincipalId> dictionary = new Dictionary<string, UserClientAndPrincipalId>(); //holds useridentities
                    List<string> ids = new List<string>();
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
                        UserClientAndPrincipalId userIds = new UserClientAndPrincipalId(ids[0], ids[1]);
                        ids.Clear();
                        dictionary.Add(resourceId, userIds); //add resourceids and its corresponding struct each time we read one in 
                    }
                    userAssignedIdentities = dictionary;
                    continue;
                }
            }
            return new Identity(tenantId.Value, principalId.Value, clientId.Value, resourceId.Value, type.Value, userAssignedIdentities);
        }

        public void Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            if (Optional.IsDefined(PrincipalId))
            {
                writer.WritePropertyName("PrincipalId");
                writer.WriteStringValue(PrincipalId.ToString());
            }
            if (Optional.IsDefined(TenantId))
            {
                writer.WritePropertyName("TenantId");
                writer.WriteStringValue(TenantId.ToString());
            }
            if (Optional.IsDefined(ClientId))
            {
                writer.WritePropertyName("ClientId");
                writer.WriteStringValue(ClientId.ToString());
            }
            if (Optional.IsDefined(ResourceId))
            {
                writer.WritePropertyName("ResourceId");
                writer.WriteStringValue(ResourceId.ToString());
            }
            if (Optional.IsDefined(Kind))
            {
                writer.WritePropertyName("Kind");
                writer.WriteStringValue(Kind.ToString());
            }
            if (!Optional.IsDefined(UserAssignedIdentities))
            {
                writer.WritePropertyName("UserAssignedIdentities");
                writer.WriteStringValue("null");
            }
            if (Optional.IsDefined(UserAssignedIdentities))
            {
                if (Optional.IsCollectionDefined(UserAssignedIdentities.AsEnumerable()))
                {
                    writer.WritePropertyName("userAssignedIdentities");
                    writer.WriteStartObject();
                    foreach (var item in UserAssignedIdentities)
                    {
                        writer.WritePropertyName(item.Key);
                        writer.WriteObjectValue(item.Value);
                    }
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();                
            }
            writer.Flush();
            writer.WriteEndObject();
        }
    }
}
