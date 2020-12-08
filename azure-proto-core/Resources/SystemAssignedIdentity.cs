namespace azure_proto_core.Resources
{
    using System;
    using System.Text.Json;
    using Azure.Core;

    public class SystemAssignedIdentity
    {
        public Guid? TenantId { get; set; }

        public Guid? PrincipalId { get; set; }

        public SystemAssignedIdentity() : this(Guid.Empty, Guid.Empty) { }

        public SystemAssignedIdentity(Guid tenantId, Guid principalId)
        {
            this.TenantId = tenantId;
            this.PrincipalId = principalId;
        }

        public int CompareTo(SystemAssignedIdentity other)
        {
            if ((this.TenantId.HasValue == false && this.PrincipalId.HasValue == false) && other == null)
                return 0;
            else if (this.TenantId.HasValue == false && this.PrincipalId.HasValue == false)
                return -1;
            else if (this.TenantId.Value.CompareTo(other.TenantId.Value) == 1 && this.PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 1)
                return 1;
            else if (this.TenantId.Value.CompareTo(other.TenantId.Value) == 0 && this.PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 0)
                return 0;
            else
                return -1;
        }

        public bool Equals(SystemAssignedIdentity other)
        {
            if ((this.TenantId.HasValue == false && this.PrincipalId.HasValue == false) && other == null)
                return true;
            else if (this.TenantId.HasValue == false && this.PrincipalId.HasValue == false)
                return false;
            else
                return (this.TenantId == other.TenantId) && (this.PrincipalId == other.PrincipalId.Value);
        }

        public static SystemAssignedIdentity Deserialize(JsonElement element)
        {
            Optional<Guid> principalId = default;
            Optional<Guid> tenantId = default;
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
            }
            return new SystemAssignedIdentity(principalId, tenantId);
        }

        public static void Serialize(Utf8JsonWriter writer, SystemAssignedIdentity systemAssignedIdentity)
        {
            writer.WritePropertyName("principalId");
            if (!Optional.IsDefined(systemAssignedIdentity.PrincipalId))
                writer.WriteStringValue("null");
            else
                writer.WriteStringValue(systemAssignedIdentity.ToString());
            writer.WritePropertyName("tenantId");
            if (!Optional.IsDefined(systemAssignedIdentity.TenantId))
                writer.WriteStringValue("null");
            else
                writer.WriteStringValue(systemAssignedIdentity.TenantId.ToString());
        }
    }
}
