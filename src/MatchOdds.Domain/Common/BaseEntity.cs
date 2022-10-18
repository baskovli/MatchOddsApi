using System.ComponentModel.DataAnnotations;

namespace MatchOdds.Domain.Common
{
#pragma warning disable
    public abstract class BaseEntity<TKey> : AuditableEntity, IHasKey<TKey>
    {
        [Key]
        public TKey ID { get; set; }
    }
}
