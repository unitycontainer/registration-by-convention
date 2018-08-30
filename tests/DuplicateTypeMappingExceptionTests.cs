using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization.Json;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Unity.RegistrationByConvention.Exceptions;

namespace Microsoft.Practices.Unity.Tests
{
#if !NETCOREAPP
    [TestClass]
    public class DuplicateTypeMappingExceptionTests
    {
        [TestMethod]
        public void CanSerializeAndDeserialize()
        {
            var ex = new DuplicateTypeMappingException("SampleName", typeof(string), typeof(int), typeof(object));

            var fs = new FileStream("data.dat", FileMode.Create);
            var formatter = new BinaryFormatter();
            formatter.Serialize(fs, ex);
            fs.Close();
            
            fs = new FileStream("data.dat", FileMode.Open);
            var newEx = (DuplicateTypeMappingException) formatter.Deserialize(fs);

            Assert.AreEqual(newEx.MappedFromType, ex.MappedFromType);
            Assert.AreEqual(newEx.CurrentMappedToType, ex.CurrentMappedToType);
            Assert.AreEqual(newEx.Name, ex.Name);
            Assert.AreEqual(newEx.NewMappedToType, ex.NewMappedToType);
        }
    }
#endif
}