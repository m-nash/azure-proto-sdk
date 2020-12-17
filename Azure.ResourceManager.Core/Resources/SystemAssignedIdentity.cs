namespace Azure.ResourceManager.Core
{
    using System;
    using System.Text.Json;
    using Azure.Core;

    public class SystemAssignedIdentity
    {
        public Guid? TenantId { get; private set; }

        public Guid? PrincipalId { get; private set; }

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

            int compareResult = 0;
            if ((compareResult = TenantId.GetValueOrDefault().CompareTo(other.TenantId.GetValueOrDefault())) == 0 &&
                (compareResult = PrincipalId.GetValueOrDefault().CompareTo(other.PrincipalId.GetValueOrDefault())) == 0)
                return 0;

            return compareResult;
        }

        public bool Equals(SystemAssignedIdentity other)
        {
            if (other == null)
                return false;

            return TenantId.Equals(other.TenantId) && PrincipalId.Equals(other.PrincipalId);
        }

        public static SystemAssignedIdentity Deserialize(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Undefined)
            {
                throw new ArgumentException("JsonElement cannot be undefined ", nameof(element));
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
            if (systemAssignedIdentity == null)
                throw new ArgumentNullException(nameof(systemAssignedIdentity));

            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.WritePropertyName("principalId");
            if (!Optional.IsDefined(systemAssignedIdentity.PrincipalId))
            {
                writer.WriteStringValue("null");
            }
            else
            {
                writer.WriteStringValue(systemAssignedIdentity.PrincipalId.ToString());
            }

            writer.WritePropertyName("tenantId");
            if (!Optional.IsDefined(systemAssignedIdentity.TenantId))
            {
                writer.WriteStringValue("null");
            }
            else
            {
                writer.WriteStringValue(systemAssignedIdentity.TenantId.ToString());
            }

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
