
namespace HomeRunner.Foundation.Cqrs
{
    public interface IWithCorrelationIdentifier<TIdentifier>
    {
        TIdentifier CorrelationId { get; set; }
    }
}
