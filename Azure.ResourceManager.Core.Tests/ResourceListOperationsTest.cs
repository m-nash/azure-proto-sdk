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
        public void TestArmResonseArmResource()
        {
            var testDic = new Dictionary<string, string> { { "tag1", "value1" } };
            String loc = "Japan East";
            var asArmOp = (ArmResourceOperations)TestListActivater<ArmResourceOperations, ArmResource>(testDic, loc);
            Assert.IsTrue(loc == asArmOp.DefaultLocation);
        }

        private static object TestListActivater<TOperation, TResource>(Dictionary<string, string> tags = null, string location = "East US")
        {
            var testMethod = typeof(ResourceListOperations).GetMethod("CreateResourceConverter", BindingFlags.Static | BindingFlags.NonPublic);
            var asGeneric = testMethod.MakeGenericMethod(new Type[] { typeof(TOperation), typeof(TResource) });
            var context = new AzureResourceManagerClientOptions(new Uri("https://management.azure.com"), new DefaultAzureCredential());
            var function = (Func<GenericResourceExpanded, TOperation>)asGeneric.Invoke(null, new object[] { context });
            var resource = new GenericResourceExpanded();
            resource.Location = location;
            resource.Tags = tags ?? new Dictionary<string, string>();
            return function.DynamicInvoke(new object[] { resource });
        }
    }
}
