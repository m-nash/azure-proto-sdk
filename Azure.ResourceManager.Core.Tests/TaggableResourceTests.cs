// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Identity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core.Tests
{
    [TestFixture]
    public class TaggableResourceTests
    {
        const string Id = "/subscriptions/0accec26-d6de-4757-8e74-d080f38eaaab/resourceGroups/Aqua/providers/Microsoft.Storage/storageAccounts/aquadiag508";
        static readonly IDictionary<string, string> UpdateTags = new Dictionary<string, string> { { "UpdateKey1", "UpdateValue1" }, { "UpdateKey2", "UpdateValue2" } };
        static readonly IDictionary<string, string> OriTags = new Dictionary<string, string> { { "key1", "value1" }, { "key2", "value2" } };
        static ResourceGroupOperations Operations = new ResourceGroupOperations(
                new SubscriptionOperations(
                    new AzureResourceManagerClientOptions(),
                    "0accec26-d6de-4757-8e74-d080f38eaaab",
                    new DefaultAzureCredential(), //should make a fake credential creation
                    new Uri("https://management.azure.com")),
                "Aqua");
        static TaggableResource TaggableResource = new TaggableResource(Operations, Id);

        [SetUp]
        public async Task GlobalSetUp()
        {
            await TaggableResource.StartAddTag("key1", "value1").WaitForCompletionAsync();
            await TaggableResource.StartAddTag("key2", "value2").WaitForCompletionAsync();
        }

        [TearDown]
        public void GlobalTearDown()
        {
            String[] keys = { "key1", "key2", "UpdateKey1", "UpdateKey2" };
            foreach (string key in keys)
            {
                TaggableResource.RemoveTag(key);
            }
        }

        [Test]
        public void TestSetTagsActivator()
        {
            var result = TaggableResource.SetTags(UpdateTags);
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [Test]
        public async Task TestSetTagsAsyncActivator()
        {
            var result = await TaggableResource.SetTagsAsync(UpdateTags);
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [Test]
        public void TestStartSetTagsActivator()
        {
            var result = TaggableResource.StartSetTags(UpdateTags).WaitForCompletionAsync().Result;
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [Test]
        public async Task TestStartSetTagsAsyncActivator()
        {
            var result = await TaggableResource.StartSetTagsAsync(UpdateTags);
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [TestCaseSource(nameof(TagSource))]
        public void TestRemoveTagActivator(string key, IDictionary<string, string> tags)
        {
            var result = TaggableResource.RemoveTag(key);
            Assert.AreEqual(result.Value.Data.Tags, tags);
        }

        [TestCaseSource(nameof(TagSource))]
        public async Task TestRemoveTagAsyncActivator(string key, IDictionary<string, string> tags)
        {
            var result = await TaggableResource.RemoveTagAsync(key);
            Assert.AreEqual(result.Value.Data.Tags, tags);
        }

        [TestCaseSource(nameof(TagSource))]
        public void TestStartRemoveTagActivator(string key, IDictionary<string, string> tags)
        {
            var result = TaggableResource.StartRemoveTag(key).WaitForCompletionAsync().Result;
            Assert.AreEqual(result.Value.Data.Tags, tags);
        }

        [TestCaseSource(nameof(TagSource))]
        public async Task TestStartRemoveTagAsyncActivator(string key, IDictionary<string, string> tags)
        {
            var result = await TaggableResource.StartRemoveTagAsync(key);
            Assert.AreEqual(result.Value.Data.Tags, tags);
        }

        static IEnumerable<object[]> TagSource()
        {
            IDictionary<string, string> OriKey1 = new Dictionary<string, string> { { "key1", "value1" } };
            IDictionary<string, string> OriKey2 = new Dictionary<string, string> { { "key2", "value2" } };

            return new[] { new object[] {"key1", OriKey2 },
                new object[] {"key2", OriKey1 },
                new object[] {"NullKey", OriTags }
            };
        }
    }
}
