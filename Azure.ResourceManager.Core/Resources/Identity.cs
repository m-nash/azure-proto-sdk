﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Core;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Represents a managed identity
    /// </summary>
    public class Identity : IEquatable<Identity>
    {
        private const string SystemAssigned = "SystemAssigned";
        private const string UserAssigned = "UserAssigned";
        private const string SystemAndUserAssigned = "SystemAssigned, UserAssigned";

        public SystemAssignedIdentity SystemAssignedIdentity { get; private set; }

        public IDictionary<ResourceIdentifier, UserAssignedIdentity> UserAssignedIdentities { get; private set; } // maintain structure of {id, (clientid, principal id)} in case of multiple UserIdentities

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
    }
}
