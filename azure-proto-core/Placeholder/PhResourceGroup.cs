// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Collections.Generic;
using Azure.ResourceManager.Resources.Models;

namespace azure_proto_core
{
    public class PhResourceGroup : TrackedResource<ResourceGroup>, IManagedByResource
    {
        public PhResourceGroup(ResourceGroup rg)
            : base(rg.Id, rg.Location, rg)
        {
            if (null == rg.Tags)
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
