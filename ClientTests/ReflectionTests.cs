using azure_proto_core;
using NUnit.Framework;
using System.Reflection;
using azure_proto_compute;
using Azure.Identity;
using System;
using Azure.ResourceManager.Resources.Models;
using Azure.ResourceManager.Compute.Models;
namespace azure_proto_core_test
{
    public class ReflectionTest
    {
        [TestCase]
        public void TestVm()
        {
            var testMethod = typeof(azure_proto_core.ResourceListOperations).GetMethod("CreateResourceConvertor", BindingFlags.Static | BindingFlags.NonPublic);
            var asGeneric = testMethod.MakeGenericMethod(new System.Type[] {typeof(XVirtualMachine), typeof(PhVirtualMachine)});
            var context = new ArmClientContext(new Uri("https://management.azure.com"), new DefaultAzureCredential());
            var function =  (Func<GenericResourceExpanded, XVirtualMachine>) asGeneric.Invoke(null, new object[] {context, new ArmClientOptions()} );
            var resource = new GenericResourceExpanded();
            resource.Location = "East US";
            function.DynamicInvoke(new object [] {resource});
        }
    }
}
