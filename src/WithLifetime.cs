using System;
using Unity.Lifetime;

namespace Unity.RegistrationByConvention
{
    /// <summary>
    /// Provides helper methods to specify the lifetime for a type with registration by convention.
    /// </summary>
    public static partial class WithLifetime
    {
        /// <summary>
        /// Returns a <see langword="null"/> <see cref="ITypeLifetimeManager"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A lifetime manager</returns>
        public static ITypeLifetimeManager None(Type type)
        {
            return null;
        }

        /// <summary>
        /// Returns a <see cref="ContainerControlledITypeLifetimeManager"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A container controlled lifetime manager.</returns>
        public static ContainerControlledLifetimeManager ContainerControlled(Type type)
        {
            return new ContainerControlledLifetimeManager();
        }

        /// <summary>
        /// Returns a <see cref="ExternallyControlledLifetimeManager"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>An externally controlled lifetime manager.</returns>
        public static ExternallyControlledLifetimeManager ExternallyControlled(Type type)
        {
            return new ExternallyControlledLifetimeManager();
        }

        /// <summary>
        /// Returns a <see cref="HierarchicalLifetimeManager"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A hierarchical lifetime manager.</returns>
        public static HierarchicalLifetimeManager Hierarchical(Type type)
        {
            return new HierarchicalLifetimeManager();
        }

        /// <summary>
        /// Returns a <see cref="PerResolveLifetimeManager"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A per resolve lifetime manager.</returns>
        public static PerResolveLifetimeManager PerResolve(Type type)
        {
            return new PerResolveLifetimeManager();
        }

        /// <summary>
        /// Returns a <see cref="TransientLifetimeManager"/>.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <returns>A transient lifetime manager.</returns>
        public static TransientLifetimeManager Transient(Type type)
        {
            return new TransientLifetimeManager();
        }

        /// <summary>
        /// Returns a <see cref="ITypeLifetimeManager" />.
        /// </summary>
        /// <typeparam name="T">The custom <see cref="ITypeLifetimeManager"/> type.</typeparam>
        /// <param name="type">The type.</param>
        /// <returns>
        /// A lifetime manager.
        /// </returns>
        public static ITypeLifetimeManager Custom<T>(Type type)
            where T : ITypeLifetimeManager, new()
        {
            return new T();
        }
    }
}
