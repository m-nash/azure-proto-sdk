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
            if (other == null)
                return 1;

            if (!TenantId.HasValue && !other.TenantId.HasValue)
                return 0;

            if (!TenantId.HasValue)
                return -1;

            if (!other.TenantId.HasValue)
                return 1;

            if (TenantId.Value.CompareTo(other.TenantId.Value) == 1 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 1)
                return 1;

            if (TenantId.Value.CompareTo(other.TenantId.Value) == 0 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 0)
                return 0;

            return -1;
        }

        public bool Equals(SystemAssignedIdentity other)
        {
            if (other == null)
                return false;

            if (!TenantId.HasValue && !other.TenantId.HasValue)
                return true;

            if (!TenantId.HasValue || !other.TenantId.HasValue)
                return false;

            return TenantId.Value.Equals(other.TenantId.Value) && PrincipalId.Value.Equals(other.PrincipalId.Value);
        }

        public static SystemAssignedIdentity Deserialize(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Undefined)
            {
                throw new ArgumentNullException("JsonElement is undefined");
            }

            Optional<Guid> principalId = default;
            Optional<Guid> tenantId = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("principalId"))
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                        principalId = Guid.Parse(property.Value.GetString());

                }

                if (property.NameEquals("tenantId"))
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                        tenantId = Guid.Parse(property.Value.GetString());

                }
            }

            if (principalId == default(Guid) && tenantId == default(Guid))
                return null;

            if (principalId == default(Guid) || tenantId == default(Guid))
                throw new InvalidOperationException("Either TenantId or PrincipalId were null");

            return new SystemAssignedIdentity(tenantId, principalId);
        }

        public static void Serialize(Utf8JsonWriter writer, SystemAssignedIdentity systemAssignedIdentity)
        {
            if (systemAssignedIdentity == null || writer == null)
                throw new ArgumentNullException("SystemAssignedIdentity or writer is null");

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

        public static bool Equals(SystemAssignedIdentity original, SystemAssignedIdentity other)
        {
            if (original == null)
                return other == null;

            return original.Equals(other);
        }
    }
}
