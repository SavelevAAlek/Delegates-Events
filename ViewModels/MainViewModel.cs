using Delegates_Events.Models;
using Delegates_Events.ViewModels.Base;
using System.Collections.ObjectModel;

namespace Delegates_Events.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private Client _selectedClient;
        private ObservableCollection<Client> _clientsList;
        private ViewModelBase _accountViewModel;

        public Client SelectedClient
        {
            get => _selectedClient;
            set
            {
                SetProperty(ref _selectedClient, value);
                _accountViewModel = new AccountViewModel(SelectedClient);
            }
        }
        public ObservableCollection<Client> ClientsList { get => _clientsList; set => SetProperty(ref _clientsList, value); }
        public ViewModelBase AccountViewModel {  get => _accountViewModel; set => SetProperty(ref _accountViewModel, value);}

        public MainViewModel()
        {

        }
    }
}
