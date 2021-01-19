// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;

namespace Azure.ResourceManager.Core.Tests
{
    public class ResourceTypeTests
    {
        const string resourcePrefix = "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/";

        [TestCase(0, "Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.classicStorage/storageAccounts/account2")]
        [TestCase(-1, "Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.DiffSpace/storageAccounts/account2")]
        [TestCase(1, "Microsoft.DiffSpace/storageAccounts/account1",
            "Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "Microsoft.${?>._`/storageAccounts/account1",
            "Microsoft.${?>._`/storageAccounts/account2")]
        [TestCase(-1, "Microsoft.${?>._`/storageAccounts/account1",
            "Microsoft.ClassicStorage/storageAccounts/account2")]
        public void CompateToNamespace(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resourcePrefix + resource1);
            ResourceType resourceType2 = new ResourceType(resourcePrefix + resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }

        [TestCase(0, "Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.ClassicStorage/StorageAccounts/account2")]
        [TestCase(1, "Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.ClassicStorage/diffaccount/account2")]
        [TestCase(-1, "Microsoft.ClassicStorage/diffaccount/account1",
            "Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "Microsoft.ClassicStorage/${?>._`/account1",
            "Microsoft.ClassicStorage/${?>._`/account2")]
        [TestCase(-1, "Microsoft.ClassicStorage/${?>._`/account1",
            "Microsoft.ClassicStorage/storageAccounts/account2")]
        public void CompateToType(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resourcePrefix + resource1);
            ResourceType resourceType2 = new ResourceType(resourcePrefix + resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }

        [TestCase(0, "Microsoft.Network/virtualNetworks/Testvnet/subnets/default1",
            "Microsoft.Network/virtualNetworks/Testvnet/subnets/default2")]
        [TestCase(0, "Microsoft.Network/virtualNetworks/Testvnet/subnets/default1",
            "Microsoft.Network/virtualnetworks/Testvnet/subnets/default2")]
        [TestCase(1, "Microsoft.Network/virtualNetworks/Testvnet/subnets/default1",
            "Microsoft.Network/parentTest/Testvnet/subnets/default2")]
        [TestCase(-1, "Microsoft.Network/parentTest/Testvnet/subnets/default1",
            "Microsoft.Network/virtualNetworks/Testvnet/subnets/default2")]
        [TestCase(0, "Microsoft.Network/${?>._`/Testvnet/${?>._`/default1",
            "Microsoft.Network/${?>._`/Testvnet/${?>._`/default2")]
        [TestCase(-1, "Microsoft.Network/${?>._`/Testvnet/subnets/default1",
            "Microsoft.Network/virtualNetworks/Testvnet/${?>._`/default2")]
        public void CompateToParent(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resourcePrefix + resource1);
            ResourceType resourceType2 = new ResourceType(resourcePrefix + resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }

        [Test]
        public void CompateToSameResourceTypes()
        {
            ResourceType resourceType1 = new ResourceType(resourcePrefix + "Microsoft.ClassicStorage/storageAccounts/account1");
            ResourceType resourceType2 = resourceType1;
            Assert.AreEqual(0, resourceType1.CompareTo(resourceType2));
        }

        [TestCase(-1, "Microsoft.Network1/virtualNetworks2/Testvnet/subnets/default1",
            "Microsoft.network2/virtualNetworks1/Testvnet/Subnets/default2")]
        [TestCase(1, "Microsoft.network2/VirtualNetworks2/Testvnet/subnets2/default1",
            "Microsoft.Network1/virtualNetworks1/Testvnet/Subnets1/default2")]
        [TestCase(1, "Microsoft.network2/VirtualNetworks2/Testvnet/Subnets2/default1",
            "Microsoft.network1/virtualNetworks1/Testvnet/Subnets1/default2")]
        public void CompateToMore(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resource1);
            ResourceType resourceType2 = new ResourceType(resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }
    }
}
