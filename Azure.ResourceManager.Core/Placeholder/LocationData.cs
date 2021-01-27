// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    /// <summary>
    /// A class representing the Location data model.
    /// </summary>
    public class LocationData : TrackedResource<Azure.ResourceManager.Resources.Models.Location>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LocationData"/> class.
        /// </summary>
        /// <param name="loc"> The existing location model to copy from. </param>
        public LocationData(ResourceManager.Resources.Models.Location loc)
            : base(loc.Id, loc.Name, loc)
        {
        }

        /// <inheritdoc/>
        public override string Name => Model.Name;

        /// <summary>
        /// Gets the subscription id
        /// </summary>
        public string SubscriptionId => Model.SubscriptionId;

        /// <summary>
        /// Gets the display name
        /// </summary>
        public string DisplayName => Model.DisplayName;

        /// <summary>
        /// Gets the latitude
        /// </summary>
        public string Latitude => Model.Metadata.Latitude;

        /// <summary>
        /// Gets the longitude
        /// </summary>
        public string Longitude => Model.Metadata.Longitude;
    }
}
