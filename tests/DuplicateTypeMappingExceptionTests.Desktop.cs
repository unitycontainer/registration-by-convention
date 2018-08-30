using System;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security;
using System.Security.Permissions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.RegistrationByConvention.Exceptions;

namespace Microsoft.Practices.Unity.Tests
{
    [TestClass]
    public class DuplicateTypeMappingExceptionTests
    {
        [TestMethod]
        public void CanSerializeAndDeserialize()
        {
            var ex = new DuplicateTypeMappingException("SampleName", typeof(string), typeof(int), typeof(object));

            var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, ex);

            ms.Seek(0, SeekOrigin.Begin);
            var newEx = (DuplicateTypeMappingException) formatter.Deserialize(ms);
            ms.Dispose();

            Assert.AreEqual(newEx.MappedFromType, ex.MappedFromType);
            Assert.AreEqual(newEx.CurrentMappedToType, ex.CurrentMappedToType);
            Assert.AreEqual(newEx.Name, ex.Name);
            Assert.AreEqual(newEx.NewMappedToType, ex.NewMappedToType);
        }

        [TestMethod]
        public void TestLimitedPermissionSet() 
        {
            Type type = typeof(DuplicateTypeMappingException);

            var platform = Assembly.GetExecutingAssembly();
            var name = platform.FullName + ": Sandbox " + Guid.NewGuid();
            var setup = new AppDomainSetup { ApplicationBase = Path.GetDirectoryName(platform.Location) };
            PermissionSet permissionSet = new PermissionSet(PermissionState.None);
            permissionSet.AddPermission(new SecurityPermission(SecurityPermissionFlag.Execution));
            permissionSet.AddPermission(new FileIOPermission(FileIOPermissionAccess.Read, Path.Combine(platform.Location, "Unity.RegistrationByConvention.dll")));
            var sandbox = AppDomain.CreateDomain(name, null, setup, permissionSet);

            var value = (DuplicateTypeMappingException)sandbox.CreateInstanceAndUnwrap(
                type.Assembly.FullName,
                type.FullName, 
                false,
                BindingFlags.Default,
                null,
                new object[]{"SampleName", typeof(string), typeof(int), typeof(object)},
                null,
                new object[0]
            );

            var ms = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(ms, value);

            ms.Seek(0, SeekOrigin.Begin);
            var newEx = (DuplicateTypeMappingException)formatter.Deserialize(ms);
            ms.Dispose();

            Assert.AreEqual(newEx.MappedFromType, value.MappedFromType);
            Assert.AreEqual(newEx.CurrentMappedToType, value.CurrentMappedToType);
            Assert.AreEqual(newEx.Name, value.Name);
            Assert.AreEqual(newEx.NewMappedToType, value.NewMappedToType);
        }
    }
}