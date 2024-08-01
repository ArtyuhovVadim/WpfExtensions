namespace WpfExtensions.Mvvm.Messaging;

public class StrongRecipientSubscription<TMessage> : BaseSubscription<TMessage>
    where TMessage : class
{
    private readonly IRecipient<TMessage> _recipient;

    public StrongRecipientSubscription(IMessageBus bus, IRecipient<TMessage> recipient) : base(bus) => _recipient = recipient;

    public override bool IsAlive => true;

    public override bool TryInvoke(TMessage message)
    {
        if (!IsAlive)
            return false;

        _recipient.Receive(message);

        return true;
    }
}