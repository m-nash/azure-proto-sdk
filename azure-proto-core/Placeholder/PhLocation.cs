﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace azure_proto_core
{
    // Update the signature for new library
    public class PhLocation : TrackedResource<Azure.ResourceManager.Resources.Models.Location>
    {
        public PhLocation(Azure.ResourceManager.Resources.Models.Location loc)
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
