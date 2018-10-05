namespace Commons.Repository
{
    public class EntityBase<TId> : IEntity<TId>
    {
        public virtual TId Id { get; set; }

        public virtual bool Equals(IEntity<TId> other)
            => Id.Equals(other.Id);

        public override bool Equals(object obj)
            => obj is EntityBase<TId> entity
            && Equals(entity);

        public override int GetHashCode()
            => Id.GetHashCode();
    }

    public class EntityBase : EntityBase<long> { }
}
