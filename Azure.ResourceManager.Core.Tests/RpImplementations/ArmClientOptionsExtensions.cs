// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core.Tests
{
    public static class AzureResourceManagerClientOptionsExtensions
    {
        public static FakeRpApiVersions FakeRpApiVersions(this AzureResourceManagerClientOptions AzureResourceManagerClientOptions)
        {
            return AzureResourceManagerClientOptions.GetOverrideObject<FakeRpApiVersions>(() => new FakeRpApiVersions()) as FakeRpApiVersions;
        }
    }
}
