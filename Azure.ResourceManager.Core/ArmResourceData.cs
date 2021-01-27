// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the generic azure resource data model.
    /// </summary>
    public class ArmResourceData : TrackedResource<GenericResource>, IManagedByResource, ISkuResource
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceData"/> class.
        /// </summary>
        /// <param name="genericResource"> The existing resource model to copy from. </param>
        public ArmResourceData(GenericResource genericResource)
            : base(genericResource.Id, genericResource.Location, genericResource)
        {
            Tags.Clear();
            foreach (var tag in genericResource.Tags)
            {
                Tags.Add(tag);
            }

            if (Model.Sku != null)
                Sku = new Sku(Model.Sku);

            if (Model.Plan != null)
                Plan = new Plan(Model.Plan);

            Kind = Model.Kind;
            ManagedBy = Model.ManagedBy;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceData"/> class.
        /// </summary>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        public ArmResourceData(ResourceIdentifier id)
            : base(id, LocationData.Default, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ArmResourceData"/> class.
        /// </summary>
        /// <param name="id"> The identifier of the resource that is the target of operations. </param>
        /// <param name="location"> The location of the resource. </param>
        public ArmResourceData(ResourceIdentifier id, LocationData location)
            : base(id, location, null)
        {
        }

        /// <inheritdoc/>
        public override ResourceIdentifier Id { get; protected set; }

        /// <summary>
        /// Gets or sets who this resource is managed by.
        /// </summary>
        public string ManagedBy { get; set; }

        /// <summary>
        /// Gets or sets the sku.
        /// </summary>
        public Sku Sku { get; set; }

        /// <summary>
        /// Gets or sets the plan.
        /// </summary>
        public Plan Plan { get; set; }

        /// <summary>
        /// Gets or sets the kind.
        /// </summary>
        public string Kind { get; set; }
    }
}