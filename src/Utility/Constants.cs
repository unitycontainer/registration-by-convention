using System;
using System.Collections.Generic;
using System.Text;

namespace Unity.RegistrationByConvention
{
    public static class Constants
    {
        public const string DuplicateTypeMappingException = "An attempt to override an existing mapping was detected for type {1} with name \"{0}\", currently mapped to type {2}, to type {3}.";
        public const string ExceptionNullAssembly = "The set of assemblies contains a null element.";
    }
}
