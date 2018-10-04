using System;

namespace Commons.Repository
{
    public interface IEntity<TId> : IEquatable<IEntity<TId>> where TId : struct
    {
        TId Id { get; set; }
    }

    public interface IEntity : IEntity<long> { }
}
