// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace azure_proto_core.Resources
{
    using System;
    using System.Text.Json;
    using Azure.Core;

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
            if (other == null)
                return 1;

            if (!ClientId.HasValue && !other.ClientId.HasValue)
                return 0;

            if (!ClientId.HasValue)
                return -1;

            if (!other.ClientId.HasValue)
                return 1;

            if (ClientId.Value.CompareTo(other.ClientId.Value) == 1 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 1)
                return 1;

            if (ClientId.Value.CompareTo(other.ClientId.Value) == 0 && PrincipalId.Value.CompareTo(other.PrincipalId.Value) == 0)
                return 0;

            return -1;
        }

        public bool Equals(UserAssignedIdentity other)
        {
            if (other == null)
                return false;

            if (!ClientId.HasValue && !other.ClientId.HasValue)
                return true;

            if (!ClientId.HasValue || !other.ClientId.HasValue)
                return false;

            return ClientId.Value.Equals(other.ClientId.Value) && PrincipalId.Value.Equals(other.PrincipalId.Value);
        }

        public static UserAssignedIdentity Deserialize(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Undefined)
            {
                throw new ArgumentNullException("JsonElement is undefined");
            }

            Optional<Guid> principalId = default;
            Optional<Guid> clientId = default;
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("principalId"))
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                        principalId = Guid.Parse(property.Value.GetString());

                }

                if (property.NameEquals("clientId"))
                {
                    if (property.Value.ValueKind != JsonValueKind.Null)
                        clientId = Guid.Parse(property.Value.GetString());

                }
            }

            if (principalId == default(Guid) && clientId == default(Guid))
                return null;

            if (principalId == default(Guid) || clientId == default(Guid))
                throw new InvalidOperationException("Either ClientId or PrincipalId were null");

            return new UserAssignedIdentity(clientId, principalId);
        }

        public static void Serialize(Utf8JsonWriter writer, UserAssignedIdentity userAssignedIdentity)
        {
            if (userAssignedIdentity == null || writer == null)
                throw new ArgumentNullException("UserAssignedIdentity or writer is null");

            writer.WriteStartObject();

            writer.WritePropertyName("clientId");
            if (!Optional.IsDefined(userAssignedIdentity.ClientId))
                writer.WriteStringValue("null");

            else
                writer.WriteStringValue(userAssignedIdentity.ClientId.ToString());

            writer.WritePropertyName("principalId");
            if (!Optional.IsDefined(userAssignedIdentity.PrincipalId))
                writer.WriteStringValue("null");

            else
                writer.WriteStringValue(userAssignedIdentity.PrincipalId.ToString());

            writer.WriteEndObject();
            writer.Flush();
        }

        public static bool Equals(UserAssignedIdentity original, UserAssignedIdentity other)
        {
            if (original == null)
                return other == null;

            return original.Equals(other);
        }
    }
}
