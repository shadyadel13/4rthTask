using System;
using System.Collections.Generic;
using System.Linq;

namespace _4rthTask
{
    public class Account
    {
        public string Name { get; set; }
        public double Balance { get; set; }

        public Account(string name = "Unnamed Account", double balance = 0.0)
        {
            this.Name = name;
            this.Balance = balance;
        }

        public virtual bool Deposit(double amount)
        {
            if (amount > 0)
            {
                Balance += amount;
                return true;
            }
            return false;
        }

        public virtual bool Withdraw(double amount)
        {
            if (Balance - amount >= 0)
            {
                Balance -= amount;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{Name}: Balance = {Balance:C}";
        }

        public static double operator +(Account a1, Account a2)
        {
            return a1.Balance + a2.Balance;
        }
    }

    public class SavingsAccount : Account
    {
        public double InterestRate { get; set; }

        public SavingsAccount(string name = "Unnamed Savings Account", double balance = 0.0, double interestRate = 2.0)
            : base(name, balance)
        {
            this.InterestRate = interestRate;
        }

        public override bool Deposit(double amount)
        {
            if (base.Deposit(amount))
            {
                Balance += (amount * InterestRate / 100);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{base.ToString()} (Savings - Interest Rate: {InterestRate}%)";
        }
    }

    public class CheckingAccount : Account
    {
        private const double WithdrawalFee = 1.50;

        public CheckingAccount(string name = "Unnamed Checking Account", double balance = 0.0)
            : base(name, balance) { }

        public override bool Withdraw(double amount)
        {
            return base.Withdraw(amount + WithdrawalFee);
        }

        public override string ToString()
        {
            return $"{base.ToString()} (Checking - Withdrawal Fee: {WithdrawalFee:C})";
        }
    }

    public class TrustAccount : SavingsAccount
    {
        private int WithdrawalsThisYear = 0;
        private const int MaxWithdrawals = 3;
        private const double BonusThreshold = 5000.0;
        private const double BonusAmount = 50.0;
        private const double MaxWithdrawalPercentage = 0.2;

        public TrustAccount(string name = "Unnamed Trust Account", double balance = 0.0, double interestRate = 2.0)
            : base(name, balance, interestRate) { }

        public override bool Deposit(double amount)
        {
            if (amount >= BonusThreshold)
                Balance += BonusAmount;

            return base.Deposit(amount);
        }

        public override bool Withdraw(double amount)
        {
            if (WithdrawalsThisYear >= MaxWithdrawals || amount > (Balance * MaxWithdrawalPercentage))
                return false;

            if (base.Withdraw(amount))
            {
                WithdrawalsThisYear++;
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{base.ToString()} (Trust - Withdrawals Left: {MaxWithdrawals - WithdrawalsThisYear})";
        }
    }

    public static class AccountUtil
    {
        public static void Display(List<Account> accounts)
        {
            Console.WriteLine("\n=== Accounts ==========================================");
            foreach (var acc in accounts)
                Console.WriteLine(acc);
        }

        public static void Deposit(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Depositing to Accounts =================================");
            foreach (var acc in accounts)
            {
                if (acc.Deposit(amount))
                    Console.WriteLine($"Deposited {amount:C} to {acc}");
                else
                    Console.WriteLine($"Failed Deposit of {amount:C} to {acc}");
            }
        }

        public static void Withdraw(List<Account> accounts, double amount)
        {
            Console.WriteLine("\n=== Withdrawing from Accounts ==============================");
            foreach (var acc in accounts)
            {
                if (acc.Withdraw(amount))
                    Console.WriteLine($"Withdrew {amount:C} from {acc}");
                else
                    Console.WriteLine($"Failed Withdrawal of {amount:C} from {acc}");
            }
        }
    }

    public class Program
    {
        static void Main()
        {
            var accounts = new List<Account>
            {
                new Account(),
                new Account("Larry"),
                new Account("Moe", 2000),
                new Account("Curly", 5000)
            };

            AccountUtil.Display(accounts);
            AccountUtil.Deposit(accounts, 1000);
            AccountUtil.Withdraw(accounts, 2000);

            var savAccounts = new List<SavingsAccount>
           
            {
                new SavingsAccount(),
                new SavingsAccount("Superman"),
                new SavingsAccount("Batman", 2000),
                new SavingsAccount("Wonderwoman", 5000, 5.0)
            };
            AccountUtil.Display(savAccounts.Cast<Account>().ToList());
            AccountUtil.Deposit(savAccounts.Cast<Account>().ToList(), 1000);
            AccountUtil.Withdraw(savAccounts.Cast<Account>().ToList(), 2000);
            var checAccounts = new List<CheckingAccount>
            {
                new CheckingAccount(),
                new CheckingAccount("Larry2"),
                new CheckingAccount("Moe2", 2000),
                new CheckingAccount("Curly2", 5000)
            };

            AccountUtil.Display(checAccounts.Cast<Account>().ToList());
            AccountUtil.Deposit(checAccounts.Cast<Account>().ToList(), 1000);
            AccountUtil.Withdraw(checAccounts.Cast<Account>().ToList(), 2000);


            var trustAccounts = new List<TrustAccount>
            {
                new TrustAccount(),
                new TrustAccount("Superman2"),
                new TrustAccount("Batman2", 2000),
                new TrustAccount("Wonderwoman2", 5000, 5.0)
            };
            // هنا زودت السحب  علشان اتاكد من الفيلد هيجي لما يسحب اكتر من 3 مرات
            AccountUtil.Display(trustAccounts.Cast<Account>().ToList());
            AccountUtil.Deposit(trustAccounts.Cast<Account>().ToList(), 1000);
            AccountUtil.Deposit(trustAccounts.Cast<Account>().ToList(), 6000); 
            AccountUtil.Withdraw(trustAccounts.Cast<Account>().ToList(), 2000);
            AccountUtil.Withdraw(trustAccounts.Cast<Account>().ToList(), 3000);
            AccountUtil.Withdraw(trustAccounts.Cast<Account>().ToList(), 500);
            AccountUtil.Withdraw(trustAccounts.Cast<Account>().ToList(), 500);
            AccountUtil.Withdraw(trustAccounts.Cast<Account>().ToList(), 500);

            var acc1 = new Account("Alpha", 3000);
            var acc2 = new Account("Beta", 4500);
            Console.WriteLine($"\nSumming Balances: {acc1} + {acc2} = {(acc1 + acc2):C}");

            Console.WriteLine();
        }
    }
}
// first search

//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace DuplicateNumberChecker
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                // Read a list of integers from the user
//                Console.WriteLine("Enter a list of integers separated by spaces:");
//                string input = Console.ReadLine();

//                // Convert input to a list of integers
//                List<int> numbers = input.Split(' ')
//                                         .Select(int.Parse)
//                                         .ToList();

//                // Check for duplicates
//                CheckForDuplicates(numbers);

//                Console.WriteLine("No duplicates found. The list is valid.");
//            }
//            catch (DuplicateNumberException ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//            catch (FormatException)
//            {
//                Console.WriteLine("Invalid input. Please enter integers only.");
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred: {ex.Message}");
//            }
//        }

//        static void CheckForDuplicates(List<int> numbers)
//        {
//            // Use a HashSet to detect duplicates
//            HashSet<int> uniqueNumbers = new HashSet<int>();

//            foreach (int number in numbers)
//            {
//                if (!uniqueNumbers.Add(number)) 
//                {
//                    throw new DuplicateNumberException($"Duplicate number found: {number}");
//                }
//            }
//        }
//    }

//    
//    public class DuplicateNumberException : Exception
//    {
//        public DuplicateNumberException(string message) : base(message)
//        {
//        }
//    }
//}





// second search
//using System;

//namespace VowelChecker
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            try
//            {
//                Console.WriteLine("Enter a string:");
//                string input = Console.ReadLine(); 

//                CheckForVowels(input);
//                Console.WriteLine("The string contains vowels.");
//            }
//            catch (NoVowelsException ex)
//            {
//                Console.WriteLine(ex.Message);
//            }
//            catch (Exception ex)
//            {
//                Console.WriteLine($"An error occurred: {ex.Message}");
//            }
//        }

//        static void CheckForVowels(string input)
//        {
//           
//            string vowels = "aeiouAEIOU";

//          
//            bool containsVowel = false;
//            foreach (char c in input)
//            {
//                if (vowels.Contains(c))
//                {
//                    containsVowel = true;
//                    break;
//                }
//            }

//           
//            if (!containsVowel)
//            {
//                throw new NoVowelsException("The string does not contain any vowels.");
//            }
//        }
//    }

//   
//    public class NoVowelsException : Exception
//    {
//        public NoVowelsException(string message) : base(message)
//        {
//        }
//    }
//}