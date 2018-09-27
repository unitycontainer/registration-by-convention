

using System;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace Unity.RegistrationByConvention.Exceptions
{
    /// <summary>
    /// The exception that is thrown when registering multiple types would result in an type mapping being overwritten.
    /// </summary>
    [Serializable]
    public class DuplicateTypeMappingException : Exception
    {
        [NonSerialized] private DuplicateTypeMappingExceptionState state = new DuplicateTypeMappingExceptionState();

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
            state.DupplicatTypeMapping_Name = name;
            state.DuplicateTypeMapping_MappedFromType = (mappedFromType ?? throw new ArgumentNullException(nameof(mappedFromType))).AssemblyQualifiedName;
            state.DuplicateTypeMapping_CurrentMappedToType = (currentMappedToType ?? throw new ArgumentNullException(nameof(currentMappedToType))).AssemblyQualifiedName;
            state.DuplicateTypeMapping_NewMappedToTypee = (newMappedToType ?? throw new ArgumentNullException(nameof(newMappedToType))).AssemblyQualifiedName;

            HandleSerialization();
        }

        protected DuplicateTypeMappingException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
            state.DupplicatTypeMapping_Name = info.GetString("DuplicateTypeMapping_Name");
            state.DuplicateTypeMapping_MappedFromType = info.GetString("DuplicateTypeMapping_MappedFromType");
            state.DuplicateTypeMapping_CurrentMappedToType = info.GetString("DuplicateTypeMapping_CurrentMappedToType");
            state.DuplicateTypeMapping_NewMappedToTypee = info.GetString("DuplicateTypeMapping_NewMappedToTypee");

            HandleSerialization();
        }

        /// <summary>
        /// Gets the name for the mapping.
        /// </summary>
        public string Name => state.DupplicatTypeMapping_Name;

        /// <summary>
        /// Gets the source type for the mapping.
        /// </summary>
        public string MappedFromType => state.DuplicateTypeMapping_MappedFromType;

        /// <summary>
        /// Gets the type currently mapped.
        /// </summary>
        public string CurrentMappedToType => state.DuplicateTypeMapping_CurrentMappedToType;

        /// <summary>
        /// Gets the new type to map.
        /// </summary>
        public string NewMappedToType => state.DuplicateTypeMapping_NewMappedToTypee;


        private static string CreateMessage(string name, Type mappedFromType, Type currentMappedToType, Type newMappedToType)
        {
            return string.Format(CultureInfo.CurrentCulture, Constants.DuplicateTypeMappingException, name, mappedFromType, currentMappedToType, newMappedToType);
        }

        public void HandleSerialization()
        {
#if !NETCOREAPP
            SerializeObjectState += (sender, args) => args.AddSerializedState(state);
#endif
        }

        [Serializable]
        private struct DuplicateTypeMappingExceptionState : ISafeSerializationData
        {
            public string DupplicatTypeMapping_Name { get; set; }

            public string DuplicateTypeMapping_MappedFromType { get; set; }

            public string DuplicateTypeMapping_CurrentMappedToType { get; set; }

            public string DuplicateTypeMapping_NewMappedToTypee { get; set; }

            public void CompleteDeserialization(object deserialized)
            {
                var ex = deserialized as DuplicateTypeMappingException;
                if (ex == null)
                    return;

                ex.HandleSerialization();
                ex.state = this;
            }
        }
    }
}
