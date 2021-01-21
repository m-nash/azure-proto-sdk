// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using NUnit.Framework;

namespace Azure.ResourceManager.Core.Tests
{
    public class ResourceTypeTests
    {
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.classicStorage/storageAccounts/account2")]
        [TestCase(-1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.DiffSpace/storageAccounts/account2")]
        [TestCase(1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.DiffSpace/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.${?>._`/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.${?>._`/storageAccounts/account2")]
        [TestCase(-1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.${?>._`/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.ClassicStorage/storageAccounts")]
        [TestCase(0, "Microsoft.classicStorage/storageAccounts",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1")]
        [TestCase(-1, "Microsoft.ClassicStorage/storageAccounts", "Microsoft.DiffSpace/storageAccounts")]
        [TestCase(1, "Microsoft.ClassicStorage/storageAccounts", "Microsoft.${?>._`/storageAccounts")]
        public void CompateToNamespace(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resource1);
            ResourceType resourceType2 = new ResourceType(resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }

        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/StorageAccounts/account2")]
        [TestCase(1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/diffaccount/account2")]
        [TestCase(-1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/diffaccount/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/${?>._`/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/${?>._`/account2")]
        [TestCase(-1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/${?>._`/account1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1",
            "Microsoft.ClassicStorage/StorageAccounts")]
        [TestCase(0, "Microsoft.classicStorage/storageAccounts",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/StorageAccounts/account2")]
        [TestCase(-1, "Microsoft.ClassicStorage/diffaccount", "Microsoft.ClassicStorage/storageAccounts")]
        [TestCase(1, "Microsoft.ClassicStorage/storageAccounts", "Microsoft.ClassicStorage/${?>._`")]
        public void CompateToType(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resource1);
            ResourceType resourceType2 = new ResourceType(resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }

        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/subnets/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/subnets/default2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/subnets/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualnetworks/Testvnet/subnets/default2")]
        [TestCase(1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/subnets/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/parentTest/Testvnet/subnets/default2")]
        [TestCase(-1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/parentTest/Testvnet/subnets/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/subnets/default2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/${?>._`/Testvnet/${?>._`/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/${?>._`/Testvnet/${?>._`/default2")]
        [TestCase(-1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/${?>._`/Testvnet/subnets/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/${?>._`/default2")]
        [TestCase(0, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/subnets/default1",
            "Microsoft.Network/virtualNetworks/subnets")]
        [TestCase(0, "Microsoft.Network/virtualNetworks/subnets",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network/virtualNetworks/Testvnet/subnets/default2")]
        [TestCase(-1, "Microsoft.Network/parentTest/subnets", "Microsoft.Network/virtualNetworks/subnets")]
        [TestCase(1, "Microsoft.Network/virtualNetworks/subnets", "Microsoft.Network/${?>._`/subnets")]
        public void CompateToParent(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resource1);
            ResourceType resourceType2 = new ResourceType(resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }

        [Test]
        public void CompateToSameResourceTypes()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1");
            ResourceType resourceType2 = resourceType1;
            Assert.AreEqual(0, resourceType1.CompareTo(resourceType2));
        }

        [Test]
        public void CompateToNull()
        {
            ResourceType resourceType1 = new ResourceType("/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.ClassicStorage/storageAccounts/account1");
            ResourceType resourceType2 = null;
            Assert.AreEqual(1, resourceType1.CompareTo(resourceType2));
        }

        [TestCase(-1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network1/virtualNetworks2/Testvnet/subnets/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.network2/virtualNetworks1/Testvnet/Subnets/default2")]
        [TestCase(1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.network2/VirtualNetworks2/Testvnet/subnets2/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network1/virtualNetworks1/Testvnet/Subnets1/default2")]
        [TestCase(1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.network2/VirtualNetworks2/Testvnet/Subnets2/default1",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.network1/virtualNetworks1/Testvnet/Subnets1/default2")]
        [TestCase(1, "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.network2/VirtualNetworks2/Testvnet/Subnets2/default1",
            "Microsoft.network1/parentTest/subnets1")]
        [TestCase(-1, "Microsoft.Network1/VirtualNetworks1/subnets2",
            "/subscriptions/00000000-0000-0000-0000-000000000000/resourceGroups/testRg/providers/Microsoft.Network1/VirtualNetworks2/Testvnet/subnets1/default2")]
        [TestCase(1, "Microsoft.network1/virtualvetworks1/Subnets2", "Microsoft.Network1/VirtualNetworks2/subnets1")]
        public void CompateToMore(int expected, string resource1, string resource2)
        {
            ResourceType resourceType1 = new ResourceType(resource1);
            ResourceType resourceType2 = new ResourceType(resource2);
            Assert.AreEqual(expected, resourceType1.CompareTo(resourceType2));
        }
    }
}
