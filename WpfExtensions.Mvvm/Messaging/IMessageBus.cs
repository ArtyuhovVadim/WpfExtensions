namespace WpfExtensions.Mvvm.Messaging;

public interface IMessageBus
{
    ISubscription RegisterHandler<TRecipient, TMessage>(TRecipient recipient, Action<TRecipient, TMessage> handler, RefType refType = RefType.Weak) 
        where TMessage : class 
        where TRecipient : class;

    ISubscription RegisterHandler<TMessage>(IRecipient<TMessage> recipient, RefType refType = RefType.Weak)
        where TMessage : class;

    bool Unregister<TMessage>(ISubscription subscription) where TMessage : class;

    void Send<TMessage>(TMessage message) where TMessage : class;
}