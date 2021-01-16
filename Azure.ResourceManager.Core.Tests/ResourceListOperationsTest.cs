using Azure.Identity;
using Azure.ResourceManager.Resources.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Azure.ResourceManager.Core.Tests
{
    public class ResourceListOperationsTest
    {
        [TestCase]
        public void TestArmResponseArmResource()
        {
            var testDic = new Dictionary<string, string> { { "tag1", "value1" } };
            String loc = "Japan East";
            var sku = new Azure.ResourceManager.Resources.Models.Sku();
            sku.Capacity = 10;
            sku.Family = "family";
            sku.Name = "name";
            sku.Size = "size";
            sku.Tier = "tier";
            var asArmOp = (ArmResource)TestListActivator(testDic, sku, loc);
            Assert.IsTrue(loc == asArmOp.DefaultLocation);
            Assert.IsNotNull(asArmOp.Data.Sku);
            Assert.IsTrue(sku.Capacity == asArmOp.Data.Sku.Capacity);
            Assert.IsTrue(sku.Family == asArmOp.Data.Sku.Family);
            Assert.IsTrue(sku.Name == asArmOp.Data.Sku.Name);
            Assert.IsTrue(sku.Size == asArmOp.Data.Sku.Size);
            Assert.IsTrue(sku.Tier == asArmOp.Data.Sku.Tier);
        }

        private static object TestListActivator(Dictionary<string, string> tags = null, Azure.ResourceManager.Resources.Models.Sku sku = default, string location = "East US")
        {
            var testMethod = typeof(ResourceListOperations).GetMethod("CreateResourceConverter", BindingFlags.Static | BindingFlags.NonPublic);
            var options = new AzureResourceManagerClientOptions(new Uri("https://management.azure.com"), new DefaultAzureCredential());
            var function = (Func<GenericResourceExpanded, ArmResource>)testMethod.Invoke(null, new object[] { options });
            var resource = new GenericResourceExpanded();
            resource.Location = location;
            resource.Tags = tags ?? new Dictionary<string, string>();
            resource.Sku = sku;
            return function.DynamicInvoke(new object[] { resource });
        }
    }
}
