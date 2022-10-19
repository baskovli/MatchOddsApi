using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MatchOdds.Domain.Common;

#pragma warning disable

public abstract class BaseEntity<TKey> : IBaseEntity<TKey>
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public virtual TKey ID { get; set; }
}

public abstract class DeleteEntity<TKey> : BaseEntity<TKey>, IDeleteEntity<TKey>
{
    public bool IsDeleted { get; set; }
}

public abstract class AuditEntity<TKey> : DeleteEntity<TKey>, IAuditEntity<TKey>
{
    public DateTime CreatedDate { get; set; }
    public string CreatedBy { get; set; }
    public DateTime? UpdatedDate { get; set; }
    public string UpdatedBy { get; set; }
}

