// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core
{
    // Update the signature for new library
    public class LocationData : TrackedResource<Azure.ResourceManager.Resources.Models.Location>
    {
        public LocationData(Azure.ResourceManager.Resources.Models.Location loc)
            : base(loc.Id, loc.Name, loc)
        {
        }

        public override string Name => Model.Name;

        public string SubscriptionId => Model.SubscriptionId;

        public string DisplayName => Model.DisplayName;

        public string Latitude => Model.Metadata.Latitude;

        public string Longitude => Model.Metadata.Longitude;
    }
}
