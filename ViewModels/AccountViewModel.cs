using Delegates_Events.Models;
using Delegates_Events.ViewModels.Base;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace Delegates_Events.ViewModels
{
    public class AccountViewModel : ViewModelBase
    {
        private string _notification;
        private readonly Client _selectedClient;
        private ObservableCollection<IAccount<object>> _accounts;
        private ObservableCollection<IAccount<object>> _accountsList;
        private IAccount<object> _selectedAccount;
        private IAccount<object> _accountToTransfer;
        private IAccount<object> _accountFromTransfer;
        private decimal _amount;
        private decimal _amountToTransfer;
        private string _newAccountType;

        /// <summary>
        /// Тип создаваемого счета
        /// </summary>
        public string NewAccountType { get => _newAccountType; set => SetProperty(ref _newAccountType, value); } 
        /// <summary>
        /// Уведомление для журнала событий
        /// </summary>
        public string Notification { get => _notification; set => SetProperty(ref _notification, value); }
        /// <summary>
        /// Список аккаунтов выбранного клиента
        /// </summary>
        public ObservableCollection<IAccount<object>> Accounts { get => _accounts; set => SetProperty(ref _accounts, value); }
        public IAccount<object> SelectedAccount
        {
            get => _selectedAccount;
            set
            {
                SetProperty(ref _selectedAccount, value);
                _selectedAccount.Notify += DisplayMessage;

            }
        }
        /// <summary>
        /// Счет, на который осуществляется перевод
        /// </summary>
        public IAccount<object> AccountToTransfer { get => _accountToTransfer; set => SetProperty(ref _accountToTransfer, value); }
        /// <summary>
        /// Счет с которого осуществляется перевод
        /// </summary>
        public IAccount<object> AccountFromTransfer { get => _accountFromTransfer; set => SetProperty(ref _accountFromTransfer, value); }
        public decimal Amount { get => _amount; set => SetProperty(ref _amount, value); }
        /// <summary>
        /// Сумма переводимых средств
        /// </summary>
        public decimal AmountToTransfer { get => _amountToTransfer; set => SetProperty(ref _amountToTransfer, value); }
        /// <summary>
        /// Список счетов для перевода
        /// </summary>
        public ObservableCollection<IAccount<object>> AccountsList { get => _accountsList; set => SetProperty(ref _accountsList, value); }

        #region Команда пополнения
        public ICommand TopUpCommand { get; set; }
        private bool CanExecuteTopUp(object arg) => _selectedAccount != null ? true : false;
        private void ExecuteTopUp(object obj) => _selectedAccount.TopUp(Amount);
        #endregion

        #region Команда перевода
        public ICommand TransferCommand { get; set; }
        private bool CanExecuteTransfer(object arg) => _selectedAccount != null ? true : false;
        private void ExecuteTransfer(object obj)
        {
            var msg = AccountFromTransfer.Transfer(AccountToTransfer, AmountToTransfer);
            MessageBox.Show(msg);
        }
        #endregion

        #region Команда открытия счета
        public ICommand OpenCommand { get; set; }
        private bool CanExecuteOpen(object arg) => true;
        private void ExecuteOpen(object obj)
        {
            Random r = new Random();
            _selectedClient.CreateNewAccount(r.Next(1, 1000), NewAccountType.Remove(0, 38), 0);
            OnPropertyChanged(nameof(Accounts));
        }
        #endregion

        #region Команда закрытия счета
        public ICommand CloseCommand { get; set; }
        private bool CanExecuteClose(object arg) => _selectedAccount != null ? true : false;
        private void ExecuteClose(object obj) => _selectedClient.DeleteAccount(Accounts.First(acc => acc == SelectedAccount));
        #endregion

        public AccountViewModel(Client selectedClient, ObservableCollection<IAccount<object>> accounts) 
        {
            _selectedClient = selectedClient;
            _selectedClient.Notify += DisplayMessage;
            AccountsList = accounts;
            Accounts = new ObservableCollection<IAccount<object>>(_selectedClient.Accounts);
            TopUpCommand = new RelayCommand(ExecuteTopUp, CanExecuteTopUp);
            TransferCommand = new RelayCommand(ExecuteTransfer, CanExecuteTransfer);
            OpenCommand = new RelayCommand(ExecuteOpen, CanExecuteOpen);
            CloseCommand = new RelayCommand(ExecuteClose, CanExecuteClose);
        }

        private void DisplayMessage(string message) => Notification += message;
    }
}
