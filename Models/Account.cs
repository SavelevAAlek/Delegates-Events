namespace Delegates_Events.Models
{
    public class Account<T> : IAccount<T>
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public decimal Amount { get; set; }

        public Account() { }
        public Account(int id, string name, decimal amount)
        {
            Id = id;
            Name = name;
            Amount = amount;
        }

        public void TopUp(decimal amount) => Amount += amount;

        public string Transfer<K>(IAccount<K> account, decimal amount)
        {
            if (amount > Amount)
            {
                return new string("Недостаточно средств на балансе");
            }
            else
            {
                account.Amount += amount;
                Amount -= amount;
                return new string("Операция выполнена успешно");
            }
        }

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

    public interface IAccount<out T>
    {
        int Id { get; set; }
        string Name { get; set; }
        decimal Amount { get; set; }
        void TopUp(decimal amoont);
        string Transfer<K>(IAccount<K> account, decimal amount);
        string Withdraw(decimal amount);
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
