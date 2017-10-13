// Copyright (c) Microsoft Corporation. All rights reserved. See License.txt in the project root for license information.

using System;
using System.Runtime.Serialization;

namespace Unity.RegistrationByConvention.Exceptions
{
    [Serializable]
    public partial class DuplicateTypeMappingException
    {
        #region Serialization Support

        partial void RegisterSerializationHandler()
        {
            SerializeObjectState += (s, e) =>
            {
                e.AddSerializedState(new DuplicateTypeMappingExceptionSerializationData(Name, MappedFromType, CurrentMappedToType, NewMappedToType));
            };
        }

        [Serializable]
        private struct DuplicateTypeMappingExceptionSerializationData : ISafeSerializationData
        {
            private readonly string _name;
            private readonly string _mappedFromType;
            private readonly string _currentMappedToType;
            private readonly string _newMappedToType;

            public DuplicateTypeMappingExceptionSerializationData(string name, string mappedFromType, string currentMappedToType, string newMappedToType)
            {
                _name = name;
                _mappedFromType = mappedFromType;
                _currentMappedToType = currentMappedToType;
                _newMappedToType = newMappedToType;
            }

            public void CompleteDeserialization(object deserialized)
            {
                var exception = (DuplicateTypeMappingException)deserialized;
                exception.Name = _name;
                exception.MappedFromType = _mappedFromType;
                exception.CurrentMappedToType = _currentMappedToType;
                exception.NewMappedToType = _newMappedToType;
            }
        }

        #endregion
    }
}