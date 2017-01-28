using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFirstATM
{

    public class Methods
    {
        public static string Read(string input)
        {
            Console.WriteLine(input);
            return Console.ReadLine();
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
                        Login();
                    }

                }
                else //Login
                {
                    Login();
                }
            }
        }

        public static void Login()
        {
            Console.Clear();
            Console.WriteLine("FIRST BANK OF DAD");
            Console.WriteLine("Please login...");
            var tmpUser = Read("Enter Username");
            var tmpPass = Read("Enter Password");
            //check if password matches
            //if so, goto Transaction screen
            //TransactionScreen();
        }
        /*
       public void TransactionScreen()
       {
           //Show balance, deposit, withdraw, exit
       }
       public double Deposit()
       {
           //add money to account
       }
       public double Withdraw()
       {
           return amount;
       }
       */
    }
}

