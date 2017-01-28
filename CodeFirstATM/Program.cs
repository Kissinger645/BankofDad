using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstATM
{
    class Program
    {
        
        static void Main(string[] args)
        {
            Methods.Welcome();
        }
    }

    public class User
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
    public class Account
    {
        public int AccountId { get; set; }
        public double Balance { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
    public class Transaction
    {
        public int TransactionID { get; set; }
        public double Amount { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
    public class CodeFirstATMContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
    }

}
