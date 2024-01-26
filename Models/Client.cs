using System.Collections.Generic;

namespace Delegates_Events.Models
{
    public class Client
    {
        public string Name { get; set; }
        public List<IAccount<object>> Accounts { get; set; }

        public void CreateNewAccount(int id, string name, decimal amount)
        {
            switch (name)
            {
                case "Депозитный": Accounts.Add(new DepositAccount(id, name, amount)); break;
                case "Не депозитный": Accounts.Add(new NonDepositAccount(id, name, amount)); break;
                default: return;
            }
        }

        public void DeleteAccount(IAccount<object> selectedAccount) => Accounts.Remove(selectedAccount);


    }
}
