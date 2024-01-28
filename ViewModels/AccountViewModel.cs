using Delegates_Events.Models;
using Delegates_Events.ViewModels.Base;
using Delegates_Events.ViewModels.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Documents;
using System.Windows.Input;

namespace Delegates_Events.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private readonly Client _selectedClient;
        private ObservableCollection<IAccount<object>> _accounts;
        private IAccount<object> _selectedAccount;
        private IAccount<object> _accountToTransfer;
        private IAccount<object> _accountFromTransfer;
        private decimal _amount;
        private decimal _amountToTransfer;
        public ObservableCollection<IAccount<object>> Accounts { get => _accounts; set => SetProperty(ref _accounts, value); }
        public IAccount<object> SelectedAccount { get => _selectedAccount; set => SetProperty(ref _selectedAccount, value); }
        public IAccount<object> AccountToTransfer { get => _accountToTransfer; set => SetProperty(ref _accountToTransfer, value); }
        public IAccount<object> AccountFromTransfer { get => _accountFromTransfer; set => SetProperty(ref _accountFromTransfer, value); }
        public decimal Amount { get => _amount; set => SetProperty(ref _amount, value); }
        public decimal AmountToTransfer { get => _amountToTransfer; set => SetProperty(ref _amountToTransfer, value); }
        public List<IAccount<object>> AccountsList { get; set; }

        public ICommand TopUpCommand { get; set; }
        public ICommand TransferCommand { get; set; }
        public AccountViewModel(Client selectedClient, List<IAccount<object>> accounts) 
        {
            _selectedClient = selectedClient;
            AccountsList = accounts;
            Accounts = new ObservableCollection<IAccount<object>>(_selectedClient.Accounts);
            TopUpCommand = new RelayCommand(ExecuteTopUp, CanExecuteTopUp);
            TransferCommand = new RelayCommand(ExecuteTransfer, CanExecuteTransfer);
        }

        private bool CanExecuteTransfer(object arg) => true;

        private void ExecuteTransfer(object obj)
        {
            AccountFromTransfer.Transfer(AccountToTransfer, AmountToTransfer);
        }

        private bool CanExecuteTopUp(object arg) => true;

        private void ExecuteTopUp(object obj) => _selectedAccount.TopUp(Amount);
    }
}
