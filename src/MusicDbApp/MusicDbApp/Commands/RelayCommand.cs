using System;

namespace MusicDbApp.Commands
{
    public class RelayCommand : CommandBase
    {
        private Action<object> _execute;
        private Func<object, bool> _canExecute;

        public RelayCommand(Action<object> execute, Func<object, bool> canExecute = null)
        {
            _execute = execute;
            _canExecute = canExecute;
        }

        public override bool CanExecute(object? parameter)
        {
            return _canExecute == null ? base.CanExecute(parameter) : _canExecute(parameter);
        }

        public override void Execute(object? parameter)
        {
            _execute.Invoke(parameter);
        }
    }
}
