using System;
using System.Collections.Generic;
using System.Text.Json;
using Azure.Core;
using SystemAssignedIdentity = azure_proto_core.Resources.SystemAssignedIdentity;
using UserAssignedIdentity = azure_proto_core.Resources.UserAssignedIdentity;

namespace azure_proto_core
{
    /// <summary>
    /// Represents a managed identity
    /// TODO: fill in properties, implement comparison and equality methods and operator overloads
    /// </summary>
    public class Identity : IEquatable<Identity>, IUtf8JsonSerializable
    {
        private const string SystemAssigned = "SystemAssigned";
        private const string UserAssigned = "UserAssigned";
        private const string SystemAndUserAssigned = "SystemAssigned, UserAssigned";

        public SystemAssignedIdentity SystemAssignedIdentity { get; set; } // Need to decide on setter

        public IDictionary<ResourceIdentifier, UserAssignedIdentity> UserAssignedIdentities { get; set; } // maintain structure of {id, (clientid, principal id)} in case of multiple UserIdentities

        public Identity() : this(null, false) { } //not system or user

        public Identity(Dictionary<ResourceIdentifier, UserAssignedIdentity> user, bool useSystemAssigned)
        {
            // check for combination of user and system on the impact to type value
            SystemAssignedIdentity = useSystemAssigned ? new SystemAssignedIdentity() : null;
            UserAssignedIdentities = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            if (user != null)
            {
                foreach (KeyValuePair<ResourceIdentifier, UserAssignedIdentity> id in user)
                {
                    UserAssignedIdentities.Add(id.Key, id.Value);
                }
            }
        }

        public Identity (SystemAssignedIdentity systemAssigned, IDictionary<ResourceIdentifier, UserAssignedIdentity> user)
        {
            SystemAssignedIdentity = systemAssigned;
            UserAssignedIdentities = user;
        }

        public bool Equals(Identity other)
        {
            if ((SystemAssignedIdentity == null && other.SystemAssignedIdentity == null) &&
                (UserAssignedIdentities == null && other.UserAssignedIdentities == null))
            {
                return true;
            }
            else if ((SystemAssignedIdentity != null && other.SystemAssignedIdentity != null) &&
                (UserAssignedIdentities == null && other.UserAssignedIdentities == null))
                return SystemAssignedIdentity.Equals(other.SystemAssignedIdentity);
            else if ((SystemAssignedIdentity == null && other.SystemAssignedIdentity == null) &&
                (UserAssignedIdentities != null && other.UserAssignedIdentities != null))
                return UserAssignedIdentity.EqualsUserAssignedIdentities(UserAssignedIdentities, other.UserAssignedIdentities);
            else
                return SystemAssignedIdentity.Equals(other.SystemAssignedIdentity) && UserAssignedIdentity.EqualsUserAssignedIdentities(UserAssignedIdentities, other.UserAssignedIdentities);
        }

        public static Identity DeserializeIdentity(JsonElement element)
        {
            Optional<SystemAssignedIdentity> systemAssignedIdentity = default;
            IdentityKind type = default;
            IDictionary<ResourceIdentifier, UserAssignedIdentity> userAssignedIdentities = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            foreach (var property in element.EnumerateObject())
            {
                if (property.NameEquals("userAssignedIdentities"))
                {
                    if (property.Value.ValueKind == JsonValueKind.Null)
                    {
                        userAssignedIdentities = null;
                        continue;
                    }

                    string resourceId = string.Empty;
                    foreach (var keyValuePair in property.Value.EnumerateObject())
                    {
                        resourceId = keyValuePair.Name;
                        var userAssignedIdentity = UserAssignedIdentity.Deserialize(keyValuePair.Value);
                        userAssignedIdentities.Add(resourceId, userAssignedIdentity);
                    }

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

                if (type.Equals(SystemAssigned))
                {
                    systemAssignedIdentity = SystemAssignedIdentity.Deserialize(element);
                }
            }

            return new Identity(systemAssignedIdentity, userAssignedIdentities);
        }

        public void Write(Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WritePropertyName("identity");
            writer.WriteStartObject();
            if (SystemAssignedIdentity != null && UserAssignedIdentities != null)
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(SystemAndUserAssigned);
                SystemAssignedIdentity.Serialize(writer, SystemAssignedIdentity);
                writer.WritePropertyName("userAssignedIdentities");
                writer.WriteStartObject();
                foreach (var keyValuePair in UserAssignedIdentities)
                {
                    writer.WriteStringValue(keyValuePair.Key);
                    writer.WriteStartObject();
                    UserAssignedIdentity.Serialize(writer, keyValuePair.Value);
                    writer.WriteEndObject();
                }

                writer.WriteEndObject();
            }
            else if (SystemAssignedIdentity != null)
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(SystemAssigned);
                SystemAssignedIdentity.Serialize(writer, SystemAssignedIdentity);
            }
            else if (UserAssignedIdentities != null)
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(UserAssigned);
                writer.WritePropertyName("userAssignedIdentities");
                writer.WriteStartObject();
                foreach (var keyValuePair in UserAssignedIdentities)
                {
                    writer.WriteStringValue(keyValuePair.Key);
                    writer.WriteStartObject();
                    UserAssignedIdentity.Serialize(writer, keyValuePair.Value);
                    writer.WriteEndObject();
                }

                writer.WriteEndObject();
            }
            else
            {
                writer.WriteStringValue("null"); // if identity is null
            }

            writer.WriteEndObject(); // close Identity
            writer.WriteEndObject(); // outermost }
            writer.Flush();
        }
    }
}
