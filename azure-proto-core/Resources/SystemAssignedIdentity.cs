namespace azure_proto_core.Resources
{
    using System;
    using System.Text.Json;
    using Azure.Core;

    public class SystemAssignedIdentity
    {
        public Guid? TenantId { get; set; }

        public Guid? PrincipalId { get; set; }

        public SystemAssignedIdentity() { }

        public SystemAssignedIdentity(Guid tenantId, Guid principalId)
        {
            TenantId = tenantId;
            PrincipalId = principalId;
        }

        public int CompareTo(SystemAssignedIdentity other)
        {
            if ((TenantId.HasValue == false && PrincipalId.HasValue == false) && other == null)
                return 0;
            else if (TenantId.HasValue == false && PrincipalId.HasValue == false)
                return -1;
            else if (TenantId.Value.CompareTo(other.TenantId.Value) == 1 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 1)
                return 1;
            else if (TenantId.Value.CompareTo(other.TenantId.Value) == 0 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 0)
                return 0;
            else
                return -1;
        }

        public bool Equals(SystemAssignedIdentity other)
        {
            if ((TenantId.HasValue == false && PrincipalId.HasValue == false) && other == null)
                return true;
            else if (TenantId.HasValue == false && PrincipalId.HasValue == false)
                return false;
            else
                return (TenantId == other.TenantId) && (PrincipalId == other.PrincipalId.Value);
        }

        public static SystemAssignedIdentity Deserialize(JsonElement element)
        {
            Optional<Guid> principalId = default;
            Optional<Guid> tenantId = default;
            bool isPrincipalIdNull = false;
            bool isTenantIdNull = false;
            foreach (var property in element.EnumerateObject())
            {
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
                if (property.NameEquals("tenantId"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        isTenantIdNull = true;
                        continue;
                    }
                    tenantId = Guid.Parse(property.Value.GetString());
                    continue;
                }
            }

            if (isPrincipalIdNull && isTenantIdNull)
                return null;
            else if ((isPrincipalIdNull && !isTenantIdNull) || (!isPrincipalIdNull && isTenantIdNull))
                throw new InvalidOperationException("Either TenantId or PrincipalId were null");
            else
                return new SystemAssignedIdentity(tenantId, principalId);
        }

        public static void Serialize(Utf8JsonWriter writer, SystemAssignedIdentity systemAssignedIdentity)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("principalId");
            if (!Optional.IsDefined(systemAssignedIdentity.PrincipalId))
                writer.WriteStringValue("null");
            else
                writer.WriteStringValue(systemAssignedIdentity.PrincipalId.ToString());
            writer.WritePropertyName("tenantId");
            if (!Optional.IsDefined(systemAssignedIdentity.TenantId))
                writer.WriteStringValue("null");
            else
                writer.WriteStringValue(systemAssignedIdentity.TenantId.ToString());
            writer.WriteEndObject();
            writer.Flush();
        }
    }
}
