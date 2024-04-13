using System.Windows.Input;

namespace MVVM6.Commands
{
    public class RelayCommand : ICommand
    {
        //fields
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        //constructor
        public RelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        //event
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove {  CommandManager.RequerySuggested -= value;}
        }

        //member methods
        public bool CanExecute(object? parameter)
        {
            return _canExecute==null?true: _canExecute(parameter);
        }

        public void Execute(object? parameter)
        {
            _execute?.Invoke(parameter);
        }
    }
}
