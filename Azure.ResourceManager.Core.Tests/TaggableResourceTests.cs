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

        [SetUp]
        public void GlobalSetUp()
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            String[] keys = { "key1", "key2", "UpdateKey1", "UpdateKey2" };
            foreach (string key in keys)
            {
                taggableResource.RemoveTag(key);
            }
        }

        [Test]
        public async Task TestSetTagsActivator()
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            await taggableResource.StartAddTag("key1", "value1").WaitForCompletionAsync();
            await taggableResource.StartAddTag("key2", "value2").WaitForCompletionAsync();
            var result = taggableResource.SetTags(UpdateTags);
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [Test]
        public async Task TestSetTagsAsyncActivator()
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            taggableResource.StartAddTag("key1", "value1");
            taggableResource.StartAddTag("key2", "value2");
            var result = await taggableResource.SetTagsAsync(UpdateTags);
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [Test]
        public void TestStartSetTagsActivator()
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            taggableResource.StartAddTag("key1", "value1");
            taggableResource.StartAddTag("key2", "value2");
            var result = taggableResource.StartSetTags(UpdateTags).WaitForCompletionAsync().Result;
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [Test]
        public async Task TestStartSetTagsAsyncActivator()
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            taggableResource.StartAddTag("key1", "value1");
            taggableResource.StartAddTag("key2", "value2");
            var result = await taggableResource.StartSetTagsAsync(UpdateTags);
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        [TestCaseSource(nameof(TagSource))]
        public void TestRemoveTagActivator(string key, IDictionary<string, string> tags)
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            taggableResource.StartAddTag("key1", "value1");
            taggableResource.StartAddTag("key2", "value2");
            var result = taggableResource.RemoveTag(key);
            Assert.AreEqual(result.Value.Data.Tags, tags);
        }

        [TestCaseSource(nameof(TagSource))]
        public async Task TestRemoveTagAsyncActivator(string key, IDictionary<string, string> tags)
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            taggableResource.StartAddTag("key1", "value1");
            taggableResource.StartAddTag("key2", "value2");
            var result = await taggableResource.RemoveTagAsync(key);
            Assert.AreEqual(result.Value.Data.Tags, tags);
        }

        [TestCaseSource(nameof(TagSource))]
        public void TestStartRemoveTagActivator(string key, IDictionary<string, string> tags)
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            taggableResource.StartAddTag("key1", "value1");
            taggableResource.StartAddTag("key2", "value2");
            var result = taggableResource.StartRemoveTag(key).WaitForCompletionAsync().Result;
            Assert.AreEqual(result.Value.Data.Tags, tags);
        }

        [TestCaseSource(nameof(TagSource))]
        public async Task TestStartRemoveTagAsyncActivator(string key, IDictionary<string, string> tags)
        {
            TaggableResource taggableResource = new TaggableResource(Operations, Id);
            taggableResource.StartAddTag("key1", "value1");
            taggableResource.StartAddTag("key2", "value2");
            var result = await taggableResource.StartRemoveTagAsync(key);
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
