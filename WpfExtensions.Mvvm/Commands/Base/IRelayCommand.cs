using System.ComponentModel;
using System.Windows.Input;

namespace WpfExtensions.Mvvm.Commands.Base;

public interface IRelayCommand : ICommand, INotifyPropertyChanged
{
    bool CanExecute();

    void Execute();
}