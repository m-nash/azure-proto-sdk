using System;
using System.Diagnostics.CodeAnalysis;

namespace azure_proto_core
{
    using System;
    using System.Collections.Generic;
    using System.Text.Json;
    using Azure.Core;
    using Azure.ResourceManager.Resources.Models;
    using SystemAssignedIdentity = azure_proto_core.Resources.SystemAssignedIdentity;
    using UserAssignedIdentity = azure_proto_core.Resources.UserAssignedIdentity;

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
            this.SystemAssignedIdentity = useSystemAssigned ? new SystemAssignedIdentity() : null;
            this.UserAssignedIdentities = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            if (user != null)
            {
                foreach (KeyValuePair<ResourceIdentifier, UserAssignedIdentity> id in user)
                {
                    this.UserAssignedIdentities.Add(id.Key, id.Value);
                }
            }
        }

        public Identity (SystemAssignedIdentity systemAssigned, IDictionary<ResourceIdentifier, UserAssignedIdentity> user)
        {
            this.SystemAssignedIdentity = systemAssigned;
            this.UserAssignedIdentities = user;
        }

        public bool Equals(Identity other)
        {
            if ((this.SystemAssignedIdentity == null && other.SystemAssignedIdentity == null) &&
                (this.UserAssignedIdentities == null && other.UserAssignedIdentities == null))
            {
                return true;
            }
            else if ((this.SystemAssignedIdentity != null && other.SystemAssignedIdentity != null) &&
                (this.UserAssignedIdentities == null && other.UserAssignedIdentities == null))
                return this.SystemAssignedIdentity.Equals(other.SystemAssignedIdentity);
            else if ((this.SystemAssignedIdentity == null && other.SystemAssignedIdentity == null) &&
                (this.UserAssignedIdentities != null && other.UserAssignedIdentities != null))
                return UserAssignedIdentity.EqualsUserAssignedIdentities(this.UserAssignedIdentities, other.UserAssignedIdentities);
            else
                return this.SystemAssignedIdentity.Equals(other.SystemAssignedIdentity) && UserAssignedIdentity.EqualsUserAssignedIdentities(this.UserAssignedIdentities, other.UserAssignedIdentities);
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
            if (this.SystemAssignedIdentity != null && this.UserAssignedIdentities != null)
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(SystemAndUserAssigned);
                SystemAssignedIdentity.Serialize(writer, this.SystemAssignedIdentity);
                UserAssignedIdentity.Serialize(writer, this.UserAssignedIdentities);
            }
            else if (this.SystemAssignedIdentity != null)
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(SystemAssigned);
                SystemAssignedIdentity.Serialize(writer, this.SystemAssignedIdentity);
            }
            else if (this.UserAssignedIdentities != null)
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(UserAssigned);
                UserAssignedIdentity.Serialize(writer, this.UserAssignedIdentities);
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
