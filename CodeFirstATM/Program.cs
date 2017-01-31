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
        public static string Read(string input)
        {
            Console.WriteLine(input);
            return Console.ReadLine();
        }

        static void Main(string[] args)
        {
            Welcome();

        }

        public static void Welcome()
        {
            using (var db = new CodeFirstATMContext())
            {
                Console.WriteLine("FIRST BANK OF DAD");
                Console.WriteLine("1) New User");
                Console.WriteLine("2) Login");
                int choice = int.Parse(Read(">  "));

                if (choice == 1) //New user
                {
                    var username = Read("Enter a UserName...");
                    if (db.Users.Any(u => u.Username == username))
                    {
                        Console.WriteLine("This username is already taken. Press any key to try again");
                        Console.ReadKey();
                        Console.Clear();
                        Welcome();
                    }
                    else
                    {
                        var password = Read("Enter a Password...");
                        User newUser = new User
                        {
                            Username = username,
                            Password = password,
                        };                       
                        db.Users.Add(newUser);
                        db.SaveChanges();
                        Console.WriteLine("Congratulations, your account is now active.");
                        Console.WriteLine("Press any key to login");
                        Console.ReadKey();
                        Console.Clear();
                        Login(db);
                    }

                }
                else //Login
                {
                    Login(db);
                }
            }
        }

        public static void Login(CodeFirstATMContext db)
        {
            Console.Clear();
            Console.WriteLine("FIRST BANK OF DAD");
            Console.WriteLine("Please login...");
            var tmpUser = Read("Enter Username");
            var tmpPass = Read("Enter Password");
            
            if (db.Users.Any(u => u.Username == tmpUser && u.Password == tmpPass))
            {
                var tmpUserId = db.Users.Where(u => u.Username == tmpUser).First();
                int UserId = tmpUserId.UserId; 
                Console.Clear();
                TransactionScreen(db, UserId);
            }
            Console.WriteLine("Sorry your username or password are incorrect");
            Console.WriteLine("Press 1 to try again or 2 to exit");
            int choice = int.Parse(Read(">.."));
            if (choice == 1)
            {
                Console.Clear();
                Login(db);
            }
            Console.Clear();
            Welcome();
        }

        public static void TransactionScreen(CodeFirstATMContext db, int _userId)
        {
            int userId = _userId;
            double balance = db.Transactions.Where(t => t.UserId == userId).Sum(t => t.Amount); 
            Console.Clear();
            Console.WriteLine($"Hello, your balance is {balance}");
            Console.WriteLine("Press 1 to make a Deposit");
            Console.WriteLine("Press 2 to make a Withdrawal");
            Console.WriteLine("Press 3 to Exit");
            int choice = int.Parse(Read(">.."));
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Deposit(db, userId);
                    break;
                case 2:
                    Console.Clear();
                    Withdraw(db, userId);
                    break;
                default:
                    Console.Clear();
                    Welcome();
                    break;
            }
        }

        public static void Deposit(CodeFirstATMContext db, int _userId)
        {
            double amount = double.Parse(Read("Enter the amount that you would like to deposit"));

            Transaction newTransaction = new Transaction
            {
                UserId = _userId,
                Amount = amount,
            };
            db.Transactions.Add(newTransaction);
            db.SaveChanges();
            Console.Clear();
            TransactionScreen(db, 1);
        }

        public static void Withdraw(CodeFirstATMContext db, int _userId)
        {

            double balance = db.Transactions.Where(t => t.UserId == _userId).Sum(t => t.Amount);
            double amount = double.Parse(Read("Enter the amount that you would like to withdraw"));
            if ((balance-amount) >= 0)
            {

                Transaction newTransaction = new Transaction
                {
                    UserId = _userId,
                    Amount = -(amount),
                };
                db.Transactions.Add(newTransaction);
                db.SaveChanges();
                Console.Clear();
                TransactionScreen(db, _userId);
            }
            Console.Clear();
            Console.WriteLine("You have insufficient funds for this transaction");
            Console.WriteLine("Press any key to return to the menu");
            Console.ReadLine();
            TransactionScreen(db, _userId);

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
