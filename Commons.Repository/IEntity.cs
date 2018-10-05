using System;

namespace Commons.Repository
{
    public interface IEntity<TId> : IEquatable<IEntity<TId>>
    {
        TId Id { get; set; }
    }

    public interface IEntity : IEntity<long> { }
}
