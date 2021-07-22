using System;
using System.Windows.Input;

public class RelayCommand<T> : ICommand
{
    private readonly bool _canexecute = false; // canExecute True False
    private readonly Predicate<T> _canExecute; // canExecute with Condition
    private readonly Action<T> _execute;       // execute with Paramater


    /// <summary>
    /// Creates a new command that can always execute.
    /// </summary>
    /// <param name="execute"></param>
    public RelayCommand(Action<T> execute)
    {
        _execute = execute;
    }

    /// <summary>
    /// Creates a new command that can always execute.
    /// </summary>
    /// <param name="execute"></param>
    public RelayCommand(Action<T> execute, bool canExecute = true)
    {
        _canexecute = canExecute;
        _execute = execute;
    }

    /// <summary>
    /// Creates a new command.
    /// </summary>
    /// <param name="canExecute"></param>
    /// <param name="execute"></param>
    public RelayCommand(Predicate<T> canExecute, Action<T> execute)
    {
        if (execute == null)
            throw new ArgumentNullException("execute");
        _canExecute = canExecute;
        _execute = execute;
    }

    /// <summary>
    /// CanExecute
    /// </summary>
    /// <param name="parameter"></param>
    /// <returns></returns>
    public bool CanExecute(object parameter)
    {
        return _canExecute == null ? true : _canExecute((T)parameter);
    }

    /// <summary>
    /// Execute
    /// </summary>
    /// <param name="parameter"></param>
    public void Execute(object parameter)
    {
        _execute((T)parameter);
    }

    public event EventHandler CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }
}