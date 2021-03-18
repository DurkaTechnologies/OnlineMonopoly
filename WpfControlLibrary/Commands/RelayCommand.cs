using System;
using System.Windows.Input;

namespace UIWPF.Commands
{
	public class RelayCommand : ICommand
	{
		private static readonly Func<object, bool> defaultCanExecuteMethod = (parameter) => true;

		private readonly Func<object, bool> canExecuteMethod;
		private readonly Action<object> executeMethod;

		public event EventHandler CanExecuteChanged;

		public RelayCommand(Action<object> executeMethod) :
			this(executeMethod, defaultCanExecuteMethod)
		{
		}

		public RelayCommand(Action<object> executeMethod, Func<object, bool> canExecuteMethod)
		{
			this.canExecuteMethod = canExecuteMethod;
			this.executeMethod = executeMethod;
		}

		public bool CanExecute(object parameter)
		{
			return canExecuteMethod(parameter);
		}

		public void Execute(object parameter)
		{
			executeMethod(parameter);
		}

		protected virtual void OnCanExecuteChanged(CanExecuteEventArgs e)
		{
			if (e != null)
				CanExecuteChanged?.Invoke(this, e);
		}

		public void RaiseCanExecuteChanged(CanExecuteEventArgs e)
		{
			OnCanExecuteChanged(e);
		}

		bool ICommand.CanExecute(object parameter)
		{
			return CanExecute(parameter);
		}

		void ICommand.Execute(object parameter)
		{
			Execute(parameter);
		}
	}

	public class CanExecuteEventArgs : EventArgs
	{
		public CanExecuteEventArgs(object info)
		{
			Info = info;
		}
		public object Info { get; set; }
	}
}
