using Delegates_Events.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Delegates_Events.ViewModels.Commands
{
    public class TopUpCommand : ICommand
    {
        private IAccount<object> _account;
        private decimal _amount;

        public TopUpCommand(IAccount<object> account, decimal amount)
        {
            _account = account;
            _amount = amount;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter) => true;

        public void Execute(object? parameter) => _account.TopUp(_amount);
    }
}
