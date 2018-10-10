namespace Commons.Repository
{
    /// <summary>
    /// Basic implementation of an <see cref="IEntity{TId}"/>.
    /// </summary>
    /// <typeparam name="TId">Type of its identifier property.</typeparam>
    public abstract class EntityBase<TId> : IEntity<TId>
    {
        /// <inheritdoc />
        public virtual TId Id { get; set; }

        /// <summary>
        /// Determines whether this instance is equivalent to the given instance.
        /// </summary>
        /// <param name="other">Instance to be tested for equality with this instance.</param>
        /// <returns><code>true</code> if both instances are considered equal (see remarks), false otherwise.</returns>
        /// <remarks>
        /// For this base implementation, two instances are equal if both <see cref="Id"/> properties
        /// are equal (by calling its <see cref="object.Equals(object)"/> method)
        /// and if both instances types have the same <see cref="System.Type.FullName"/> property value.
        /// This process will mark as distinct two instances with same <see cref="Id"/> but different classes.
        /// </remarks>
        public virtual bool Equals(IEntity<TId> other)
            => Id.Equals(other.Id)
            && GetType().FullName == other.GetType().FullName;

        /// <summary>
        /// Determines whether this instance is equivalent to the given instance.
        /// </summary>
        /// <param name="obj">Instance to be tested for equality with this instance.</param>
        /// <returns><code>true</code> if both instances are considered equal (see remarks), false otherwise.</returns>
        /// <remarks>
        /// This methods first verify <paramref name="obj"/> is of type <see cref="EntityBase{TId}"/>
        /// and then invokes <see cref="Equals(IEntity{TId})"/>.
        /// </remarks>
        public override bool Equals(object obj)
            => obj is IEntity<TId> entity
            && Equals(entity);

        /// <summary>
        /// Returns the hash code for this instance..
        /// </summary>
        /// <returns>The hash code of this instance.</returns>
        /// <remarks>
        /// The basic implementation invokes the method <see cref="object.GetHashCode"/>
        /// of <see cref="Id"/>.
        /// </remarks>
        public override int GetHashCode()
            => Id.GetHashCode();
    }

    /// <summary>
    /// Basic implementation of an <see cref="IEntity{TId}"/> of type <code>long</code>.
    /// </summary>
    public abstract class EntityBase : EntityBase<long>, IEntity { }
}
