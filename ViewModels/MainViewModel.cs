﻿using Delegates_Events.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ViewModelLibrary;

namespace Delegates_Events.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Client _selectedClient;
        private ObservableCollection<Client> _clientsList;
        private ViewModelBase _accountViewModel;
        private ObservableCollection<IAccount<object>> _accountsList;

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                SetProperty(ref _selectedClient, value);

                foreach (var account in _selectedClient.Accounts) _accountsList.Remove(account);
                _accountViewModel = new AccountViewModel(SelectedClient, _accountsList);
                OnPropertyChanged(nameof(AccountViewModel));
            }
        }
        public ObservableCollection<Client> ClientsList { get => _clientsList; set => SetProperty(ref _clientsList, value); }
        public ViewModelBase AccountViewModel {  get => _accountViewModel; set => SetProperty(ref _accountViewModel, value);}

        public MainViewModel()
        {
            ClientsList = new ObservableCollection<Client>
            {
                new Client("Alex", new List<IAccount<object>>
                {
                    new DepositAccount(1, "Deposit", 100),
                    new NonDepositAccount(2, "NonDeposit", 100)
                }),
                new Client("Dmitry", new List<IAccount<object>>
                {
                    new DepositAccount(3, "Deposit", 100),
                    new NonDepositAccount(4, "NonDeposit", 100)
                }),
            };

            _accountsList = new ObservableCollection<IAccount<object>>();

            foreach (var client in ClientsList)
            {
                foreach (var acc in client.Accounts) _accountsList.Add(acc);
            }
        }
    }
}
