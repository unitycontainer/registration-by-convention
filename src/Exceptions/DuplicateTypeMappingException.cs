// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Globalization;

namespace Unity.RegistrationByConvention.Exceptions
{
    /// <summary>
    /// The exception that is thrown when registering multiple types would result in an type mapping being overwritten.
    /// </summary>
    public partial class DuplicateTypeMappingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateTypeMappingException"/> class.
        /// </summary>
        /// <param name="name">The name for the mapping.</param>
        /// <param name="mappedFromType">The source type for the mapping.</param>
        /// <param name="currentMappedToType">The type currently mapped.</param>
        /// <param name="newMappedToType">The new type to map.</param>
        public DuplicateTypeMappingException(string name, Type mappedFromType, Type currentMappedToType, Type newMappedToType)
            : base(CreateMessage(name, mappedFromType, currentMappedToType, newMappedToType))
        {
            Name = name;
            MappedFromType = (mappedFromType ?? throw new ArgumentNullException(nameof(mappedFromType))).AssemblyQualifiedName;
            CurrentMappedToType = (currentMappedToType ?? throw new ArgumentNullException(nameof(currentMappedToType))).AssemblyQualifiedName;
            NewMappedToType = (newMappedToType ?? throw new ArgumentNullException(nameof(newMappedToType))).AssemblyQualifiedName;

            RegisterSerializationHandler();
        }

        private static string CreateMessage(string name, Type mappedFromType, Type currentMappedToType, Type newMappedToType)
        {
            return string.Format(CultureInfo.CurrentCulture, Constants.DuplicateTypeMappingException, name, mappedFromType, currentMappedToType, newMappedToType);
        }

        partial void RegisterSerializationHandler();

        /// <summary>
        /// Gets the name for the mapping.
        /// </summary>
        public string Name { get; private set; }

        /// <summary>
        /// Gets the source type for the mapping.
        /// </summary>
        public string MappedFromType { get; private set; }

        /// <summary>
        /// Gets the type currently mapped.
        /// </summary>
        public string CurrentMappedToType { get; private set; }

        /// <summary>
        /// Gets the new type to map.
        /// </summary>
        public string NewMappedToType { get; private set; }
    }
}
