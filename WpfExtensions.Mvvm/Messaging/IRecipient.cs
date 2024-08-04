namespace WpfExtensions.Mvvm.Messaging;

public interface IRecipient<in T>
{
    void Receive(T message);
}