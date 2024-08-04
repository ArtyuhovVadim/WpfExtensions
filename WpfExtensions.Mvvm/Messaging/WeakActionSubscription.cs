using System.Runtime.CompilerServices;

namespace WpfExtensions.Mvvm.Messaging;

public class WeakActionSubscription<TRecipient, TMessage> : BaseSubscription<TMessage>
    where TRecipient : class
    where TMessage : class
{
    private readonly ConditionalWeakTable<TRecipient, Action<TRecipient, TMessage>> _cwt = [];

    public WeakActionSubscription(IMessageBus bus, TRecipient recipient, Action<TRecipient, TMessage> action) : base(bus) => _cwt.Add(recipient, action);

    public override bool IsAlive => _cwt.Any();

    public override bool TryInvoke(TMessage message)
    {
        if (!IsAlive)
            return false;

        var (recipient, action) = _cwt.First();
        action(recipient, message);

        return true;
    }
}