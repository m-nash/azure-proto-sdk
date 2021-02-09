// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using Azure.Identity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Azure.ResourceManager.Core.Tests
{
    public class TaggableResourceTests
    {
        const string Id = "/subscriptions/0accec26-d6de-4757-8e74-d080f38eaaab/resourceGroups/Aqua/providers/Microsoft.Storage/storageAccounts/aquadiag508";
        static readonly IDictionary<string, string> UpdateTags = new Dictionary<string, string> { { "UpdateKey1", "UpdateValue1" }, { "UpdateKey2", "UpdateValue2" } };
        
        [Test]
        public async Task TestSetTagsActivatorAsync()
        {
            var rgOp = new ResourceGroupOperations(
                            new SubscriptionOperations(
                                new AzureResourceManagerClientOptions(),
                                "0accec26-d6de-4757-8e74-d080f38eaaab",
                                new DefaultAzureCredential(), //should make a fake credential creation
                                new Uri("https://management.azure.com")),
                            "Aqua");
            TaggableResource taggableResource = new TaggableResource(rgOp, Id);
            await taggableResource.StartAddTag("key1", "value1").WaitForCompletionAsync();
            await taggableResource.StartAddTag("key2", "value2").WaitForCompletionAsync();
            var result = taggableResource.SetTags(UpdateTags);
            Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        }

        //[Test]
        //public async void TestSetTagsAsyncActivator()
        //{
        //    AzureResourceManagerClientOptions options = new AzureResourceManagerClientOptions();
        //    TaggableResource taggableResource = new TaggableResource(options, Id);
        //    taggableResource.StartAddTag("key1", "value1");
        //    taggableResource.StartAddTag("key2", "value2");
        //    var result = await taggableResource.SetTagsAsync(UpdateTags);
        //    Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        //}

        //[Test]
        //public void TestStartSetTagsActivator()
        //{
        //    AzureResourceManagerClientOptions options = new AzureResourceManagerClientOptions();
        //    TaggableResource taggableResource = new TaggableResource(options, Id);
        //    taggableResource.StartAddTag("key1", "value1");
        //    taggableResource.StartAddTag("key2", "value2");
        //    var result = taggableResource.StartSetTags(UpdateTags).WaitForCompletionAsync().Result;
        //    Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        //}

        //[Test]
        //public async void TestStartSetTagsAsyncActivator()
        //{
        //    AzureResourceManagerClientOptions options = new AzureResourceManagerClientOptions();
        //    TaggableResource taggableResource = new TaggableResource(options, Id);
        //    taggableResource.StartAddTag("key1", "value1");
        //    taggableResource.StartAddTag("key2", "value2");
        //    var result = await taggableResource.StartSetTagsAsync(UpdateTags);
        //    Assert.AreEqual(result.Value.Data.Tags, UpdateTags);
        //}

        //[TestCaseSource(nameof(TagSource))]
        //public void TestRemoveTagActivator(bool expect, string key, IDictionary<string, string> tags)
        //{
        //    AzureResourceManagerClientOptions options = new AzureResourceManagerClientOptions();
        //    TaggableResource taggableResource = new TaggableResource(options, Id);
        //    taggableResource.StartAddTag("key1", "value1");
        //    taggableResource.StartAddTag("key2", "value2");
        //    var result = taggableResource.RemoveTag(key);
        //    Assert.AreEqual(expect, result.Value.Data.Tags.Equals(tags));
        //}

        //[TestCaseSource(nameof(TagSource))]
        //public async void TestRemoveTagAsyncActivator(bool expect, string key, IDictionary<string, string> tags)
        //{
        //    AzureResourceManagerClientOptions options = new AzureResourceManagerClientOptions();
        //    TaggableResource taggableResource = new TaggableResource(options, Id);
        //    taggableResource.StartAddTag("key1", "value1");
        //    taggableResource.StartAddTag("key2", "value2");
        //    var result = await taggableResource.RemoveTagAsync(key);
        //    Assert.AreEqual(expect, result.Value.Data.Tags.Equals(tags));
        //}

        //[TestCaseSource(nameof(TagSource))]
        //public void TestStartRemoveTagActivator(bool expect, string key, IDictionary<string, string> tags)
        //{
        //    AzureResourceManagerClientOptions options = new AzureResourceManagerClientOptions();
        //    TaggableResource taggableResource = new TaggableResource(options, Id);
        //    taggableResource.StartAddTag("key1", "value1");
        //    taggableResource.StartAddTag("key2", "value2");
        //    var result = taggableResource.StartRemoveTag(key).WaitForCompletionAsync().Result;
        //    Assert.AreEqual(expect, result.Value.Data.Tags.Equals(tags));
        //}

        //[TestCaseSource(nameof(TagSource))]
        //public async void TestStartRemoveTagAsyncActivator(bool expect, string key, IDictionary<string, string> tags)
        //{
        //    AzureResourceManagerClientOptions options = new AzureResourceManagerClientOptions();
        //    TaggableResource taggableResource = new TaggableResource(options, Id);
        //    taggableResource.StartAddTag("key1", "value1");
        //    taggableResource.StartAddTag("key2", "value2");
        //    var result = await taggableResource.StartRemoveTagAsync(key);
        //    Assert.AreEqual(expect, result.Value.Data.Tags.Equals(tags));
        //}

        static IEnumerable<object[]> TagSource()
        {
            IDictionary<string, string> UpdateKey1 = new Dictionary<string, string> { { "UpdateKey1", "UpdateValue1" } };
            IDictionary<string, string> UpdateKey2 = new Dictionary<string, string> { { "UpdateKey2", "UpdateValue2" } };

            return new[] { new object[] { true, "UpdateKey1", UpdateKey2 },
                new object[] { true, "UpdateKey2", UpdateKey1 },
                new object[] { true, "NullKey", UpdateTags },
                new object[] { true, null, UpdateTags }
            };
        }
    }
}
