// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    using System;
    using System.Text.Json;
    using Azure.Core;

    public class UserAssignedIdentity
    {
        public Guid ClientId { get; set; }

        public Guid PrincipalId { get; set; }

        public UserAssignedIdentity(Guid clientId, Guid principalId)
        {
            ClientId = clientId;
            PrincipalId = principalId;
        }

        public int CompareTo(UserAssignedIdentity other)
        {
            if (other == null)
                return 1;

            int compareResult = 0;
            if ((compareResult = ClientId.CompareTo(other.ClientId)) == 0 &&
                (compareResult = PrincipalId.CompareTo(other.PrincipalId)) == 0)
            {
                return 0;
            }

            return compareResult;
        }

        public bool Equals(UserAssignedIdentity other)
        {
            if (other == null)
                return false;

            return ClientId.Equals(other.ClientId) && PrincipalId.Equals(other.PrincipalId);
        }

        public static UserAssignedIdentity Deserialize(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Undefined)
            {
                throw new ArgumentException("JsonElement is undefined " + nameof(element));
            }

            Guid principalId = default;
            Guid clientId = default;
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
            if (userAssignedIdentity == null)
                throw new ArgumentNullException(nameof(userAssignedIdentity));

            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            writer.WriteStartObject();

            writer.WritePropertyName("clientId");
            writer.WriteStringValue(userAssignedIdentity.ClientId.ToString());

            writer.WritePropertyName("principalId");
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
