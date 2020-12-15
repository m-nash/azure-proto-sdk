using Azure.ResourceManager.Core;
using NUnit.Framework;
using System;

namespace Azure.ResourceManager.Core.Tests
{
    public class ResourceIdentifierTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [TestCase("0c2f6471-1bf0-4dda-aec3-cb9272f09575", "myRg", "Microsoft.Compute", "virtualMachines", "myVM")]
        [TestCase("0c2f6471-1bf0-4dda-aec3-cb9272f09575", "!@#$%^&*()-_+=;:'\",<.>/?", "Microsoft.Network", "virtualNetworks", "MvVM_vnet")]
        [TestCase("0c2f6471-1bf0-4dda-aec3-cb9272f09575", "myRg", "Microsoft.Network", "publicIpAddresses", "!@#$%^&*()-_+=;:'\",<.>/?")]
        public void CanParseRPIds(string subscription, string resourceGroup, string provider, string type, string name)
        {
            var resourceId = $"/subscriptions/{subscription}/resourceGroups/{Uri.EscapeDataString(resourceGroup)}/providers/{provider}/{type}/{Uri.EscapeDataString(name)}";
            ResourceIdentifier subject = resourceId;
            Assert.AreEqual(subject.ToString(), resourceId);
            Assert.AreEqual(subject.Subscription, subscription);
            Assert.AreEqual(Uri.UnescapeDataString(subject.ResourceGroup), resourceGroup);
            Assert.AreEqual(subject.Type.Namespace, provider);
            Assert.AreEqual(subject.Type.Type, type);
            Assert.AreEqual(Uri.UnescapeDataString(subject.Name), name);
        }

        [Test]
        public void CanParseSubscriptions()
        {
            ResourceIdentifier subject = "/subscriptions/0c2f6471-1bf0-4dda-aec3-cb9272f09575";
            Assert.AreEqual(subject.ToString(), "/subscriptions/0c2f6471-1bf0-4dda-aec3-cb9272f09575");
            Assert.AreEqual(subject.Subscription, "0c2f6471-1bf0-4dda-aec3-cb9272f09575");
            Assert.AreEqual(subject.Type.Namespace, "Microsoft.Resources");
            Assert.AreEqual(subject.Type.Type, "subscriptions");
        }

        [Test]
        public void CanParseResourceGroups()
        {
            ResourceIdentifier subject = "/subscriptions/0c2f6471-1bf0-4dda-aec3-cb9272f09575/resourceGroups/myRg";
            Assert.AreEqual(subject.ToString(), "/subscriptions/0c2f6471-1bf0-4dda-aec3-cb9272f09575/resourceGroups/myRg");
            Assert.AreEqual(subject.Subscription, "0c2f6471-1bf0-4dda-aec3-cb9272f09575");
            Assert.AreEqual(subject.ResourceGroup, "myRg");
            Assert.AreEqual(subject.Type.Namespace, "Microsoft.Resources");
            Assert.AreEqual(subject.Type.Type, "resourceGroups");
        }

        [TestCase("MyVnet", "MySubnet")]
        [TestCase("!@#$%^&*()-_+=;:'\",<.>/?", "MySubnet")]
        [TestCase("MyVnet", "!@#$%^&*()-_+=;:'\",<.>/?")]
        public void CanParseChildResources(string parentName, string name)
        {
            var resourceId = $"/subscriptions/0c2f6471-1bf0-4dda-aec3-cb9272f09575/resourceGroups/myRg/providers/Microsoft.Network/virtualNetworks/{Uri.EscapeDataString(parentName)}/subnets/{Uri.EscapeDataString(name)}";
            ResourceIdentifier subject = resourceId;
            Assert.AreEqual(subject.ToString(), resourceId);
            Assert.AreEqual(subject.Subscription, "0c2f6471-1bf0-4dda-aec3-cb9272f09575");
            Assert.AreEqual(Uri.UnescapeDataString(subject.ResourceGroup), "myRg");
            Assert.AreEqual(subject.Type.Namespace, "Microsoft.Network");
            Assert.AreEqual(subject.Type.Parent.Type, "virtualNetworks");
            Assert.AreEqual(subject.Type.Type, "virtualNetworks/subnets");
            Assert.AreEqual(Uri.UnescapeDataString(subject.Name), name);

            // check parent type parsing
            var parentResource = $"/subscriptions/0c2f6471-1bf0-4dda-aec3-cb9272f09575/resourceGroups/myRg/providers/Microsoft.Network/virtualNetworks/{Uri.EscapeDataString(parentName)}";
            Assert.AreEqual(subject.Parent, parentResource);
            Assert.AreEqual(subject.Parent.ToString(), parentResource);
            Assert.AreEqual(subject.Parent.Subscription, "0c2f6471-1bf0-4dda-aec3-cb9272f09575");
            Assert.AreEqual(Uri.UnescapeDataString(subject.Parent.ResourceGroup), "myRg");
            Assert.AreEqual(subject.Parent.Type.Namespace, "Microsoft.Network");
            Assert.AreEqual(subject.Parent.Type.Type, "virtualNetworks");
            Assert.AreEqual(Uri.UnescapeDataString(subject.Parent.Name), parentName);
        }

        [TestCase("UnformattedString", Description ="Too Few Elements")]
        [TestCase("/subs/sub1/rgs/rg1/", Description =  "No known parts")]
        [TestCase("/subscriptions/sub1/resourceGroups", Description = "Too few parts")]
        public void ThrowsOnInvalidUri(string resourceId)
        {
            Assert.Throws<ArgumentOutOfRangeException>(new TestDelegate(() => ConvertToResourceId(resourceId)));
        }

        public ResourceIdentifier ConvertToResourceId(string resourceId)
        {
            ResourceIdentifier subject = resourceId;
            return subject;
        }
    }
}
