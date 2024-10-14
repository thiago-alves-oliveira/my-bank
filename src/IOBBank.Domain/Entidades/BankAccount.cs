using IOBBank.Domain.Entidades.Base;

namespace IOBBank.Domain.Entidades
{
    public class BankAccount : Entidade
    {
        private BankAccount()
        {
            
        }

        public BankAccount(string name, int account, int branch, double balance)
        {
            Account = account;
            Branch = branch;
            Balance = balance;
            Name = name;
        }

        public string Name { get; set; }
        public int Account { get; set; }
        public int Branch { get; set; }
        public double Balance { get; set; }
    }
}
