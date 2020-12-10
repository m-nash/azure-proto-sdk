using azure_proto_core;
using NUnit.Framework;
using System.Reflection;
using azure_proto_compute;
using Azure.Identity;
using System;
using Azure.ResourceManager.Resources.Models;
using System.Collections.Generic;

namespace azure_proto_core_test
{
    public class ResourceListOperationsTest
    {
        [TestCase]
        public void TestArmResonseArmResource()
        {
             TestListActivator<ArmResourceOperations, ArmResource>();
        }
        private static void TestListActivator<T, U>()
        {
            var testMethod = typeof(azure_proto_core.ResourceListOperations).GetMethod("CreateResourceConvertor", BindingFlags.Static | BindingFlags.NonPublic);
            var asGeneric = testMethod.MakeGenericMethod(new System.Type[] {typeof(T), typeof(U)});
            var context = new ArmClientContext(new Uri("https://management.azure.com"), new DefaultAzureCredential());
            var function =  (Func<GenericResourceExpanded, T>) asGeneric.Invoke(null, new object[] {context, new ArmClientOptions()} );
            var resource = new GenericResourceExpanded();
            resource.Location = "East US";
            resource.Tags =  new Dictionary<string, string>{ {"tag1", "value1"} };
            function.DynamicInvoke(new object [] {resource});
        }
    }
}
