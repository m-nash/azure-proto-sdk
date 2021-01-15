// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// Represents a managed identity
    /// </summary>
    public class Identity : IEquatable<Identity>
    {
        private const string SystemAssigned = "SystemAssigned";
        private const string UserAssigned = "UserAssigned";
        private const string SystemAndUserAssigned = "SystemAssigned, UserAssigned";

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class with no <see cref="SystemAssignedIdentity"/>  or <see cref="UserAssignedIdentity"/>.
        /// </summary>
        public Identity()
            : this(null, false)
        {
        } // not system or user

        /// <summary>
        /// Initializes a new instance of the <see cref="Identity"/> class.
        /// </summary>
        /// <param name="user"> Dictionary with Resource Identifier key and a <see cref="UserAssignedIdentity"/> object value. </param>
        /// <param name="useSystemAssigned"> Flag for using <see cref="SystemAssignedIdentity"/> or not. </param>
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

        public Identity(SystemAssignedIdentity systemAssigned, IDictionary<ResourceIdentifier, UserAssignedIdentity> user) // TODO: remove this constructor later
        {
            SystemAssignedIdentity = systemAssigned;
            if (user == null)
            {
                UserAssignedIdentities = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            }
            else
            {
                UserAssignedIdentities = user;
            }
        }

        /// <summary>
        /// Gets the System Assigned Identity.
        /// </summary>
        public SystemAssignedIdentity SystemAssignedIdentity { get; private set; }

        /// <summary>
        /// Gets a dictionary of the User Assigned Identities.<para/>
        /// Maintain structure of {id, (clientid, principal id)} in case of multiple UserIdentities.
        /// </summary>
        public IDictionary<ResourceIdentifier, UserAssignedIdentity> UserAssignedIdentities { get; private set; }
                    if (other.UserAssignedIdentities.TryGetValue(identity.Key, out value))
                    {
                        if (!UserAssignedIdentity.Equals(identity.Value, value))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return SystemAssignedIdentity.Equals(SystemAssignedIdentity, other.SystemAssignedIdentity);
        }

        /// <summary>
        /// Converts a <see cref="JsonElement"/> into an <see cref="Identity"/> object.
        /// </summary>
        /// <param name="element"> A JSON containing an identity. </param>
        /// <returns> New Identity object with JSON values. </returns>
        public static Identity Deserialize(JsonElement element)
        {
            if (element.ValueKind == JsonValueKind.Undefined)
            {
                throw new ArgumentException("JsonElement cannot be undefined ", nameof(element));
            }

            Optional<SystemAssignedIdentity> systemAssignedIdentity = default;
            IDictionary<ResourceIdentifier, UserAssignedIdentity> userAssignedIdentities = new Dictionary<ResourceIdentifier, UserAssignedIdentity>();
            string type = string.Empty;
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
                        throw new ArgumentNullException(nameof(property));
                    }

                    type = property.Value.GetString();
                }

                if (type.Equals(SystemAssigned))
                {
                    systemAssignedIdentity = SystemAssignedIdentity.Deserialize(element);
                    continue;
                }

                if (type.Equals(SystemAndUserAssigned))
                {
                    systemAssignedIdentity = SystemAssignedIdentity.Deserialize(element);
                    continue;
                }
            }

            return new Identity(systemAssignedIdentity, userAssignedIdentities);
        }

        /// <summary>
        /// Converts an <see cref="Identity"/> object into a <see cref="JsonElement"/>.
        /// </summary>
        /// <param name="writer"> Utf8JsonWriter object to which the output is going to be written. </param>
        /// <param name="identity"> Identity object to be converted. </param>
        public static void Serialize(Utf8JsonWriter writer, Identity identity)
        {
            if (writer == null)
                throw new ArgumentNullException(nameof(writer));

            if (identity == null)
                throw new ArgumentNullException(nameof(identity));

            writer.WriteStartObject();
            writer.WritePropertyName("identity");

            if (identity.SystemAssignedIdentity == null && identity.UserAssignedIdentities.Count == 0)
            {
                writer.WriteStringValue("null");
                writer.WriteEndObject();
                writer.Flush();
                return;
            }

            writer.WriteStartObject();
            if (identity.SystemAssignedIdentity != null && identity.UserAssignedIdentities.Count != 0)
            {
                SystemAssignedIdentity.Serialize(writer, identity.SystemAssignedIdentity);
                writer.WritePropertyName("kind");
                writer.WriteStringValue(SystemAndUserAssigned);
                writer.WritePropertyName("userAssignedIdentities");
                writer.WriteStartObject();
                foreach (var keyValuePair in identity.UserAssignedIdentities)
                {
                    writer.WritePropertyName(keyValuePair.Key);
                    UserAssignedIdentity.Serialize(writer, keyValuePair.Value);
                }

                writer.WriteEndObject();
            }
            else if (identity.SystemAssignedIdentity != null)
            {
                SystemAssignedIdentity.Serialize(writer, identity.SystemAssignedIdentity);
                writer.WritePropertyName("kind");
                writer.WriteStringValue(SystemAssigned);
            }
            else if (identity.UserAssignedIdentities.Count != 0)
            {
                writer.WritePropertyName("kind");
                writer.WriteStringValue(UserAssigned);
                writer.WritePropertyName("userAssignedIdentities");
                writer.WriteStartObject();
                foreach (var keyValuePair in identity.UserAssignedIdentities)
                {
                    writer.WritePropertyName(keyValuePair.Key);
                    UserAssignedIdentity.Serialize(writer, keyValuePair.Value);
                }

                writer.WriteEndObject();
            }

            writer.WriteEndObject();
            writer.WriteEndObject();
            writer.Flush();
        }

        /// <summary>
        /// Detects if this Identity is equals to antoher Identity instance.
        /// </summary>
        /// <param name="other"> Identity object to compare. </param>
        /// <returns> True or False depending if they are equals. </returns>
        public bool Equals(Identity other)
        {
            if (other == null)
                return false;

            if (UserAssignedIdentities.Count == other.UserAssignedIdentities.Count)
            {
                foreach (var identity in UserAssignedIdentities)
                {
                    UserAssignedIdentity value;
                    if (other.UserAssignedIdentities.TryGetValue(identity.Key, out value))
                    {
                        if (!UserAssignedIdentity.Equals(identity.Value, value))
                        {
                            return false;
                        }
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return SystemAssignedIdentity.Equals(SystemAssignedIdentity, other.SystemAssignedIdentity);
        }
    }
}
