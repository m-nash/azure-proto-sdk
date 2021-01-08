// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Azure.ResourceManager.Resources.Models;

namespace Azure.ResourceManager.Core
{
    public class ResourceGroupData : TrackedResource<Azure.ResourceManager.Resources.Models.ResourceGroup>, IManagedByResource
    {
        public ResourceGroupData(Azure.ResourceManager.Resources.Models.ResourceGroup rg)
            : base(rg.Id, rg.Location, rg)
        {
            if (rg.Tags == null)
            {
                rg.Tags = new Dictionary<string, string>(StringComparer.InvariantCultureIgnoreCase);
            }
        }

        public override IDictionary<string, string> Tags => Model.Tags;

        public override string Name => Model.Name;

        public ResourceGroupProperties Properties
        {
            get => Model.Properties;
            set => Model.Properties = value;
        }

        public string ManagedBy
        {
            get => Model.ManagedBy;
            set => Model.ManagedBy = value;
        }
    }
}
