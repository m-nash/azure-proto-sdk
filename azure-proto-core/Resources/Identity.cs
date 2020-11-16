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

        public Dictionary<string, UserClientAndPrincipalIds> UserAssignedIdentities { get; set; }

        public struct UserClientAndPrincipalIds
        {
            string clientId { get; set; }
            string principalId { get; set; }

            public UserClientAndPrincipalIds (string clientId, string principalId)
            {
                this.clientId = clientId;
                this.principalId = principalId;
            }
        }

        internal Identity(Guid tenantId, Guid principalId, Guid? clientId, string resourceId, IdentityKind kind, Optional<Dictionary<string, UserClientAndPrincipalIds>> userAssignedIdentities)
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
            Optional<Dictionary<string, UserClientAndPrincipalIds>> userAssignedIdentities = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("principalId"))
                {
                    principalId = Guid.Parse(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("tenantId"))
                {
                    tenantId = Guid.Parse(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("clientId"))
                {
                    clientId = Guid.Parse(property.Value.GetString());
                    continue;
                }
                if (property.NameEquals("id"))
                {
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
                        property.ThrowNonNullablePropertyIsNull();
                        continue;
                    }
                    Dictionary<string, UserClientAndPrincipalIds> dictionary = new Dictionary<string, UserClientAndPrincipalIds>();
                    string[] ids = new string[2];
                    int count = 0;
                    foreach (var property0 in property.Value.EnumerateObject())
                    {
                        ids[count] = property0.ToString();
                        count++;
                    }
                    UserClientAndPrincipalIds userIds = new UserClientAndPrincipalIds(ids[0], ids[1]);
                    dictionary.Add(resourceId, userIds);
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
    }
}
