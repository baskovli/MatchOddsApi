namespace MatchOdds.Domain.Common;

public interface IBaseEntity<T>
{
    T ID { get; set; }
}

public interface IDeleteEntity
{
    bool IsDeleted { get; set; }
}

public interface IDeleteEntity<TKey> : IDeleteEntity, IBaseEntity<TKey>
{
}

public interface IAuditEntity
{
    DateTime CreatedDate { get; set; }
    //string CreatedBy { get; set; }
    DateTime? UpdatedDate { get; set; }
    //string UpdatedBy { get; set; }
}

public interface IAuditEntity<TKey> : IAuditEntity, IDeleteEntity<TKey>
{
}

