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
            var plan = new Azure.ResourceManager.Resources.Models.Plan();
            plan.Name = "name";
            plan.Product = "product";
            plan.Publisher = "publisher";
            plan.PromotionCode = "promo";
            plan.Version = "version";
            string kind = "UserAssigned";
            string managedBy = "test";
            var asArmOp = (ArmResource)TestListActivator(testDic, sku, plan, kind, managedBy, loc);
            
            Assert.IsNotNull(asArmOp.Data.Sku);
            Assert.AreEqual(sku.Capacity, asArmOp.Data.Sku.Capacity);
            Assert.AreEqual(sku.Family, asArmOp.Data.Sku.Family);
            Assert.AreEqual(sku.Name, asArmOp.Data.Sku.Name);
            Assert.AreEqual(sku.Size, asArmOp.Data.Sku.Size);
            Assert.AreEqual(sku.Tier, asArmOp.Data.Sku.Tier);

            Assert.IsNotNull(asArmOp.Data.Plan);
            Assert.AreEqual(plan.Name, asArmOp.Data.Plan.Name);
            Assert.AreEqual(plan.Product, asArmOp.Data.Plan.Product);
            Assert.AreEqual(plan.PromotionCode, asArmOp.Data.Plan.PromotionCode);
            Assert.AreEqual(plan.Publisher, asArmOp.Data.Plan.Publisher);
            Assert.AreEqual(plan.Version, asArmOp.Data.Plan.Version);

            Assert.IsTrue(loc == asArmOp.DefaultLocation);
            Assert.AreEqual(kind, asArmOp.Data.Kind);
            Assert.AreEqual(managedBy, asArmOp.Data.ManagedBy);
        }

        private static object TestListActivator(Dictionary<string, string> tags = null,
            Azure.ResourceManager.Resources.Models.Sku sku = default,
            Azure.ResourceManager.Resources.Models.Plan plan = default,
            string kind = default, 
            string managedBy = default,
            string location = "East US")
        {
            var testMethod = typeof(ResourceListOperations).GetMethod("CreateResourceConverter", BindingFlags.Static | BindingFlags.NonPublic);
            var options = new AzureResourceManagerClientOptions();
            var function = (Func<GenericResourceExpanded, ArmResource>)testMethod.Invoke(null, new object[] { options });
            var resource = new GenericResourceExpanded();
            resource.Location = location;
            resource.Tags = tags ?? new Dictionary<string, string>();
            resource.Sku = sku;
            resource.Plan = plan;
            resource.Kind = kind;
            resource.ManagedBy = managedBy;
            return function.DynamicInvoke(new object[] { resource });
        }
    }
}
