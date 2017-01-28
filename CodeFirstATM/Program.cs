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
            string tmpUser;
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
                    // check if user already exists
                    if (db.Users.Any(u => u.Username == username))
                    {
                        Console.WriteLine("This user already exists. Press any key to try again");
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
            //get User.Id from tmpUser
            if (db.Users.Any(u => u.Username == tmpUser))//need to implement password matches check
            {
                //store UserId in temp file
                Console.Clear();
                TransactionScreen(db);
            }
            Console.WriteLine("Sorry your username or password are incorrect");
            Console.WriteLine("Press 1 to try again or 2 to exit");
            int choice = int.Parse(Read(">.."));
            if (choice == 1)
            {
                Login(db);
            }
            Welcome();
        }

        public static void TransactionScreen(CodeFirstATMContext db)
        {

            //Show balance, deposit, withdraw, exit
            Console.WriteLine($"Hello, your balance is ");
            Console.WriteLine("Press 1 to make a Deposit");
            Console.WriteLine("Press 2 to make a Withdrawel");
            Console.WriteLine("Press 3 to Exit");
            int choice = int.Parse(Read(">.."));
            switch (choice)
            {
                case 1:
                    Console.Clear();
                    Deposit(db);
                    break;
                case 2:
                    Console.Clear();
                    Withdraw(db);
                    break;
                default:
                    Console.Clear();
                    Welcome();
                    break;
            }
        }

        public static void Deposit(CodeFirstATMContext db)
        {
            double amount = double.Parse(Read("Enter the amount that you would like to deposit"));

            Transaction newTransaction = new Transaction
            {
                UserId = 1,
                Amount = amount,
            };
            db.Transactions.Add(newTransaction);
            db.SaveChanges();
            Console.Clear();
            TransactionScreen(db);
        }

        public static void Withdraw(CodeFirstATMContext db)
        {

            double amount = double.Parse(Read("Enter the amount that you would like to withdraw"));

            Transaction newTransaction = new Transaction
            {
                UserId = 1,
                Amount = -(amount),
            };
            db.Transactions.Add(newTransaction);
            db.SaveChanges();
            Console.Clear();
            TransactionScreen(db);
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
