namespace WpfExtensions.Mvvm.Messaging;

public class StrongActionSubscription<TRecipient, TMessage> : BaseSubscription<TMessage>
    where TRecipient : class
    where TMessage : class
{
    private readonly TRecipient _recipient;
    private readonly Action<TRecipient, TMessage> _action;

    public StrongActionSubscription(IMessageBus bus, TRecipient recipient, Action<TRecipient, TMessage> action) : base(bus)
    {
        _recipient = recipient;
        _action = action;
    }

    public override bool IsAlive => true;

    public override bool TryInvoke(TMessage message)
    {
        if (!IsAlive)
            return false;

        _action(_recipient, message);

        return true;
    }
}