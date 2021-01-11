// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

namespace Azure.ResourceManager.Core.Tests
{
    public class FakeRpApiVersions
    {
        internal FakeRpApiVersions()
        {
            FakeResourceVersion = FakeResourceApiVersions.Default;
        }

        public FakeResourceApiVersions FakeResourceVersion { get; set; }
    }
}
