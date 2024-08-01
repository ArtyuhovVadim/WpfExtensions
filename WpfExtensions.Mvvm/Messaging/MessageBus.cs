namespace WpfExtensions.Mvvm.Messaging;

public class MessageBus : IMessageBus
{
    private readonly Dictionary<Type, Dictionary<Guid, BaseSubscription>> _subscriptionsMap = [];
    private readonly object _lockObject = new();

    public ISubscription RegisterHandler<TRecipient, TMessage>(TRecipient recipient, Action<TRecipient, TMessage> handler, RefType refType = RefType.Weak)
        where TRecipient : class
        where TMessage : class
    {
        BaseSubscription subscription = refType switch
        {
            RefType.Weak => new WeakActionSubscription<TRecipient, TMessage>(this, recipient, handler),
            RefType.Strong => new StrongActionSubscription<TRecipient, TMessage>(this, recipient, handler),
            _ => throw new ArgumentOutOfRangeException(nameof(refType), refType, null)
        };

        return RegisterInternal<TMessage>(subscription);
    }

    public ISubscription RegisterHandler<TMessage>(IRecipient<TMessage> recipient, RefType refType = RefType.Weak)
        where TMessage : class
    {
        BaseSubscription subscription = refType switch
        {
            RefType.Weak => new WeakRecipientSubscription<TMessage>(this, recipient),
            RefType.Strong => new StrongRecipientSubscription<TMessage>(this, recipient),
            _ => throw new ArgumentOutOfRangeException(nameof(refType), refType, null)
        };

        return RegisterInternal<TMessage>(subscription);
    }

    public bool Unregister<T>(ISubscription subscription) where T : class
    {
        lock (_lockObject)
        {
            var messageType = typeof(T);

            if (_subscriptionsMap.TryGetValue(messageType, out var subscriptions))
            {
                var res = subscriptions.Remove(subscription.Id);

                if (subscriptions.Count == 0)
                {
                    _subscriptionsMap.Remove(messageType);
                }

                return res;
            }

            return false;
        }
    }

    public void Send<T>(T message) where T : class
    {
        lock (_lockObject)
        {
            var messageType = typeof(T);

            if (_subscriptionsMap.TryGetValue(messageType, out var subscriptions))
            {
                var aliveSubscriptions = subscriptions.Values.Where(x => x.IsAlive).ToList();

                if (aliveSubscriptions.Count == 0)
                {
                    _subscriptionsMap.Remove(messageType);
                    return;
                }

                foreach (var subscription in aliveSubscriptions)
                {
                    subscription.TryInvoke(message);
                }

                if (aliveSubscriptions.Count != subscriptions.Count)
                {
                    _subscriptionsMap[messageType] = aliveSubscriptions.ToDictionary(x => x.Id);
                }
            }
        }
    }

    private BaseSubscription RegisterInternal<T>(BaseSubscription subscription) where T : class
    {
        lock (_lockObject)
        {
            var messageType = typeof(T);

            if (_subscriptionsMap.TryGetValue(messageType, out var subscriptions))
            {
                subscriptions[subscription.Id] = subscription;
            }
            else
            {
                _subscriptionsMap[messageType] = new Dictionary<Guid, BaseSubscription>
                {
                    [subscription.Id] = subscription
                };
            }

            return subscription;
        }
    }
}