// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    ///     Generic representation of an ARM resource.  Resources in the ARM RP should extend this resource.
    /// </summary>
    public class ArmResourceData : TrackedResource<GenericResource>, IManagedByResource, ISkuResource
    {
        public ArmResourceData(GenericResource genericResource)
            : base(genericResource.Id, genericResource.Location, genericResource)
        {
            Tags.Clear();
            foreach (var tag in genericResource.Tags)
            {
                Tags.Add(tag);
            }

            Sku = new Sku(Model.Sku);
        }

        public ArmResourceData(ResourceIdentifier id)
            : base(id, Location.Default, null)
        {
        }

        public ArmResourceData(ResourceIdentifier id, Location location)
            : base(id, location, null)
        {
        }

        public override ResourceIdentifier Id { get; protected set; }

        public string ManagedBy { get; set; }

        public Sku Sku { get; set; }

        public Plan Plan { get; set; }

        public string Kind { get; set; }
    }
}