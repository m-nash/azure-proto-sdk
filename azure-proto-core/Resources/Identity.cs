using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json;
using Azure.Core;
using Azure.ResourceManager.Resources.Models;
using azure_proto_core.Resources;
using UserAssignedIdentity = azure_proto_core.Resources.UserAssignedIdentity;
using SystemAssignedIdentity = azure_proto_core.Resources.SystemAssignedIdentity;

namespace azure_proto_core
{
    /// <summary>
    /// Represents a managed identity
    /// </summary>
    public class Identity : IEquatable<Identity>, IComparable<Identity>, IUtf8JsonSerializable
    {
        private const string SystemAssigned = "SystemAssigned";
        private const string UserAssigned = "UserAssigned";
        private const string SystemAndUserAssigned = "SystemAssigned, UserAssigned";
        public SystemAssignedIdentity SystemAssignedIdentity { get; }  // Need to decide on setter

        public Dictionary<ResourceIdentifier, UserAssignedIdentity> UserAssignedIdentities { get; set; } //maintain structure of {id, (clientid, principal id)} in case of multiple UserIdentities

        public Identity() : this(null, false) { } //not system or user

        public Identity(Dictionary<ResourceIdentifier, UserAssignedIdentity> user, bool useSystemAssigned)
        {
            // check for combination of user and system on the impact to type value
            this.SystemAssignedIdentity = useSystemAssigned ? new SystemAssignedIdentity() : null;
            this.UserAssignedIdentities = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            if (user != null)
            {
                foreach(KeyValuePair<ResourceIdentifier, UserAssignedIdentity> id in user)
                {
                    this.UserAssignedIdentities.Add(id.Key, id.Value);
                }
            }                
        }

        public int CompareTo(Identity other)
        {
            if ((this.SystemAssignedIdentity == null && other.SystemAssignedIdentity == null) &&
                (this.UserAssignedIdentities == null && other.UserAssignedIdentities == null))
                return 0;
            else if ((this.SystemAssignedIdentity != null && other.SystemAssignedIdentity != null) &&
                (this.UserAssignedIdentities == null && other.UserAssignedIdentities == null))
                return this.SystemAssignedIdentity.CompareTo(other.SystemAssignedIdentity);
            else if ((this.SystemAssignedIdentity == null && other.SystemAssignedIdentity == null) &&
                (this.UserAssignedIdentities != null && other.UserAssignedIdentities != null))
                return UserAssignedIdentity.CompareToUserAssignedIdentities(this.UserAssignedIdentities, other.UserAssignedIdentities);
            else
            {
                int systemCompare = this.SystemAssignedIdentity.CompareTo(other.SystemAssignedIdentity);
                int userCompare = UserAssignedIdentity.CompareToUserAssignedIdentities(this.UserAssignedIdentities, other.UserAssignedIdentities);
                if (userCompare == 1 && (systemCompare == 0 || systemCompare == -1))
                    return 1;
                else if (userCompare == 0 && systemCompare == 0)
                    return 0;
                else
                    return -1;
            }
        }

        public bool Equals(Identity other)
        {
            if ((this.SystemAssignedIdentity == null && other.SystemAssignedIdentity == null) &&
                (this.UserAssignedIdentities == null && other.UserAssignedIdentities == null))
                return true;
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
            Optional<Dictionary<ResourceIdentifier, UserAssignedIdentity>> userAssignedIdentities = default;
            foreach (var property in element.EnumerateObject())
            {
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
                    userAssignedIdentities = UserAssignedIdentity.Deserialize(property);
                    continue;
                }
                if (type.Equals(SystemAssigned))
                    systemAssignedIdentity = SystemAssignedIdentity.Deserialize(element);               
            }
            return new Identity(userAssignedIdentities, true);
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
                writer.WriteStringValue("null"); //if identity is null
            writer.WriteEndObject(); //close Identity               
            writer.WriteEndObject(); //outermost }
            writer.Flush();
        }
    }
}
