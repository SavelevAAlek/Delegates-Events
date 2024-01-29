using Delegates_Events.Infrastructure;
using ViewModelLibrary;

namespace Delegates_Events.Models
{
    public delegate void AccountHandler(string message);

    public class Account<T> : ViewModelBase, IAccount<T>
    {
        private decimal _amount;

        public event AccountHandler Notify;
        public int Id { get; set; }
        /// <summary>
        /// Тип аккаунта
        /// </summary>
        public virtual string Name { get; set; }
        /// <summary>
        /// Сумма средств на балансе
        /// </summary>
        public decimal Amount { get => _amount; set => SetProperty(ref _amount, value); }
        /// <summary>
        /// Св-во для отображения получателя
        /// </summary>
        public string Recipient { get => $"Счет №{Id} ({Name})"; }

        public Account() { }
        public Account(int id, string name, decimal amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        /// <summary>
        /// Пополнение баланса
        /// </summary>
        /// <param name="amount"></param>
        public void TopUp(decimal amount)
        {
            Amount += amount;
            Notify?.Invoke($"Баланс пополнен на {amount} Р\n");
        }
        /// <summary>
        /// Перевод средств на выбранный счет
        /// </summary>
        /// <typeparam name="K"></typeparam>
        /// <param name="account"></param>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string Transfer<K>(IAccount<K> account, decimal amount)
        {

            try
            {
                if (amount > Amount) { throw new NotEnoughMoneyException("Недостаточно средств на счете"); }
                account.Amount += amount;
                Amount -= amount;
                Notify?.Invoke($"Выполнен перевод на сумму {amount} Р\n");
                return new string($"Выполнен перевод на сумму {amount} Р\n");
            }
            catch(NotEnoughMoneyException ex)
            {
                return ex.Message;
            }
        }
        /// <summary>
        /// Снятие средств со счета
        /// </summary>
        /// <param name="amount"></param>
        /// <returns></returns>
        public string Withdraw(decimal amount)
        {
            if (amount > Amount)
            {
                return new string("Недостаточно средств на балансе");
            }
            else
            {
                Amount -= amount;
                return new string("Операция выполнена успешно");
            }
        }
    }

    public class DepositAccount : Account<DepositAccount>
    {
        public override string Name { get => ToString(); }
        public override string ToString() => "Депозитный";
        public DepositAccount(int id, string name, decimal amount) : base(id, name, amount) { }
    }

    public class NonDepositAccount : Account<NonDepositAccount>
    {
        public override string Name { get => ToString(); }
        public override string ToString() => "Не депозитный";
        public NonDepositAccount(int id, string name, decimal amount) : base(id, name, amount) { }
    }
}
