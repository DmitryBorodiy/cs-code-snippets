using System.Windows.Input;

namespace MvvmSample.WPF.Model
{
    public class RelayCommand : ICommand
    {
        private Action commandTask;
        public event EventHandler? CanExecuteChanged;

        public RelayCommand(Action action) 
        {
            commandTask = action;
        }

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            commandTask();
        }
    }
}
