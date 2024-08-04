namespace WpfExtensions.Mvvm.Messaging;

public class WeakRecipientSubscription<TMessage> : BaseSubscription<TMessage>
    where TMessage : class
{
    private readonly WeakReference<IRecipient<TMessage>> _recipientRef;

    public WeakRecipientSubscription(IMessageBus bus, IRecipient<TMessage> recipient) : base(bus) => _recipientRef = new WeakReference<IRecipient<TMessage>>(recipient);

    public override bool IsAlive => _recipientRef.TryGetTarget(out _);

    public override bool TryInvoke(TMessage message)
    {
        if (!_recipientRef.TryGetTarget(out var recipient))
            return false;

        recipient.Receive(message);

        return true;
    }
}