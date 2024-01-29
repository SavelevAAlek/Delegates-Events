using System.Collections.Generic;

namespace Delegates_Events.Models
{
    public class Client
    {
        public event AccountHandler Notify;
        public string ClientName { get; set; }
        public List<IAccount<object>> Accounts { get; set; }

        public Client(string name, List<IAccount<object>> accounts) { ClientName = name; Accounts = accounts; }
        /// <summary>
        /// Создание нового счета
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name"></param>
        /// <param name="amount"></param>
        public void CreateNewAccount(int id, string name, decimal amount)
        {
            switch (name)
            {
                case "Депозитный": 
                    {
                        Accounts.Add(new DepositAccount(id, name, amount));
                        Notify?.Invoke("Создан депозитный счет");
                        break;
                    }  
                case "Не депозитный":
                    {
                        Accounts.Add(new NonDepositAccount(id, name, amount));
                        Notify?.Invoke("Создан не депозитный счет");
                        break;
                    }
                default: return;
            }
        }
        /// <summary>
        /// Закрытие счета
        /// </summary>
        /// <param name="selectedAccount"></param>
        public void DeleteAccount(IAccount<object> selectedAccount)
        {
            var acc = selectedAccount;
            Accounts.Remove(selectedAccount);
            Notify?.Invoke($"Счет №{acc.Id} закрыт");
        }

    }
}
