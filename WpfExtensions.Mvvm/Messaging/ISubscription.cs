namespace WpfExtensions.Mvvm.Messaging;

public interface ISubscription : IDisposable
{
    Guid Id { get; }
}