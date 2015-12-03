
namespace HomeRunner.Foundation.Cqrs
{
    public interface IWithIdentifier<out TIdentifier>
    {
        TIdentifier Id { get; }
    }
}
