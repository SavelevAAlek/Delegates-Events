namespace Delegates_Events.Models
{

    public interface IAccount<out T>
    {
        public event AccountHandler Notify;
        int Id { get; set; }
        string Name { get; set; }
        decimal Amount { get; set; }
        void TopUp(decimal amount);
        string Transfer<K>(IAccount<K> account, decimal amount);
        string Withdraw(decimal amount);
    }
}
