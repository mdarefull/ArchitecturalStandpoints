using System;

namespace Commons.Repository
{
    /// <summary>
    /// Represents a business object (Entity) which is unequivocally identified by and Id.
    /// </summary>
    /// <typeparam name="TId">Type of its identifier property.</typeparam>
    /// <remarks>
    /// All entities implemented using this interface should be able to be uniquely identified by the <see cref="IEntity{TId}.Id"/>
    /// property and compared to other <see cref="IEntity{TId}"/>
    /// It is recommended to also override the <see cref="object.Equals(object)"/> and <see cref="object.GetHashCode"/> methods.
    /// </remarks>
    public interface IEntity<TId> : IEquatable<IEntity<TId>>
    {
        /// <summary>
        /// Gets or sets the entity's id.
        /// </summary>
        TId Id { get; set; }
    }

    /// <summary>
    /// Represents a business object (Entity) which is unequivocally identified by and Id of type <code>long</code>.
    /// </summary>
    /// <remarks>
    /// It is a typed extension of <see cref="IEntity{TId}"/> with <code>long</code> as its <see cref="IEntity{TId}.Id"/> type.
    /// </remarks>
    public interface IEntity : IEntity<long> { }
}
