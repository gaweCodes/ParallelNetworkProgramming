using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pitfall4 
{
    public class RaceSample
    {
        private static readonly SemaphoreSlim SemaphoreSlim = new SemaphoreSlim(1, 1);
        public async Task CustomerBehaviorAsync(BankAccount account)
        {
            await SemaphoreSlim.WaitAsync();
            try
            {
                await Task.Run(() => { Console.WriteLine("Start"); });
                for (var i = 0; i < 1000000; i++)
                {
                    account.Deposit(100);
                    account.Withdraw(100);
                }
            }
            finally
            {
                SemaphoreSlim.Release();
            }
        }
        public async Task TestRunAsync()
        {
            var account = new BankAccount();
            var customer1 = CustomerBehaviorAsync(account);
            var customer2 = CustomerBehaviorAsync(account);
            await customer1;
            await customer2;
            if (account.Balance != 0) throw new Exception($"Race condition occurred: Balance is {account.Balance}");
            Console.ReadLine();
        }
    }
    public class BankAccount 
    {
        public int Balance { get; set; }
        public void Deposit(int amount) => Balance += amount;
        public bool Withdraw(int amount)
        {
            if (amount > Balance) return false;
            Balance -= amount;
            return true;
        }
    }
}
