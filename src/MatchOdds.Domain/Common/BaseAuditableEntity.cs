namespace MatchOdds.Domain.Common;

public abstract class BaseAuditableEntity
{
    //public string CreatedBy { get; set; }
    public DateTime CreatedDate { get; set; }
    //public string LastModifiedBy { get; set; }
    public DateTime? LastModifiedDate { get; set; }
}

