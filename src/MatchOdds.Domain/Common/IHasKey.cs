namespace MatchOdds.Domain.Common;

public interface IHasKey<T>
{
    T ID { get; set; }
}
