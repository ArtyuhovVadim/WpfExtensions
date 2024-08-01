namespace WpfExtensions.Mvvm.Messaging;

public abstract class BaseSubscription : ISubscription
{
    public Guid Id { get; } = Guid.NewGuid();

    public abstract bool IsAlive { get; }

    public abstract bool TryInvoke(object message);

    public abstract void Dispose();
}

public abstract class BaseSubscription<TMessage> : BaseSubscription
    where TMessage : class
{
    private readonly IMessageBus _bus;

    protected BaseSubscription(IMessageBus bus) => _bus = bus;

    public override bool TryInvoke(object message)
    {
        if (message is not TMessage messageT)
            throw new InvalidCastException($"Message must be {typeof(TMessage)} type.");

        return TryInvoke(messageT);
    }

    public abstract bool TryInvoke(TMessage message);

    public override void Dispose() => _bus.Unregister<TMessage>(this);
}