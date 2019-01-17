using System;
using System.Collections.Generic;
using Unity.Injection;
using Unity.Lifetime;

namespace Unity.RegistrationByConvention
{
    /// <summary>
    /// Represents a set of types to register and their registration settings.
    /// </summary>
    public abstract class RegistrationConvention
    {
        /// <summary>
        /// Gets types to register.
        /// </summary>
        public abstract IEnumerable<Type> GetTypes();

        /// <summary>
        /// Gets a function to get the types that will be requested for each type to configure.
        /// </summary>
        public abstract Func<Type, IEnumerable<Type>> GetFromTypes();

        /// <summary>
        /// Gets a function to get the name to use for the registration of each type.
        /// </summary>
        public abstract Func<Type, string> GetName();

        /// <summary>
        /// Gets a function to get the <see cref="LifetimeManager"/> for the registration of each type. Defaults to no lifetime management.
        /// </summary>
        /// <returns></returns>
        public abstract Func<Type, LifetimeManager> GetLifetimeManager();

        /// <summary>
        /// Gets a function to get the additional <see cref="InjectionMember"/> objects for the registration of each type. Defaults to no injection members.
        /// </summary>
        /// <returns></returns>
        public abstract Func<Type, IEnumerable<InjectionMember>> GetInjectionMembers();
    }
}
