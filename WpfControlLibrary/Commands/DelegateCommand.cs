using System;

namespace UIWPF.Commands
{
	public sealed class DelegateCommand : Command
	{
		private static readonly Func<bool> defaultCanExecuteMethod = () => true;

		private readonly Func<bool> canExecuteMethod;
		private readonly Action executeMethod;

		public DelegateCommand(Action executeMethod, Func<bool> canExecuteMethod)
		{
			this.canExecuteMethod = canExecuteMethod;
			this.executeMethod = executeMethod;
		}

		protected override bool CanExecute()
		{
			return canExecuteMethod();
		}

		protected override void Execute()
		{
			executeMethod();
		}
	}
}
