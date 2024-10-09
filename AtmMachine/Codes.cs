using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AtmMachine
{
    public static class Codes
    {
        //structure creation
        public struct Account
        {
            public string AccNumber;
            public string ClientName;
            public string AccountPin;
            public double AccBalance;
        }
        public static Account[] tabAccounts = new Account[200];//array creation
        //global variable creation
        static int NbAccount;
        static bool found = false;
        static string searchAcc;


        public static void Start()
        {
            int choice = 0;
            ReadFileToArray();
            Console.WriteLine("\t\tBANQUE ROYALE\n\t\t-------------\n\tGuichet Automatique Bancaire\n\t----------------------------");
            Console.WriteLine("\t");
            FindingAndVerifyingAccount();
            Console.WriteLine("\t");
            if (found == true)
            {
                Console.WriteLine("Choisir votre Transaction");
                Console.WriteLine("\t1 - Pour Deposer");
                Console.WriteLine("\t2 - Pour Retirer");
                Console.WriteLine("\t3 - pour Consulter");
                do
                {
                    Console.Write("Entrez votre choix(1-3):");
                    choice = Convert.ToInt32(Console.ReadLine());
                }
                while (choice < 1 || choice > 3);

                switch (choice)
                {
                    case 1:
                        Deposit();
                        TransStatus();
                        Consultation();
                        Pause();
                        break;
                    case 2:
                        Withdrawal();
                        TransStatus();
                        Consultation();
                        Pause();
                        break;
                    case 3:
                        Consultation();
                        Pause();
                        break;
                }
                writeArrayTofile();

            }
            else
            {
                Pause();
            }
        }

        public static void writeArrayTofile()//writing array to file
        {
            StreamWriter myfile = new StreamWriter("Accounts.txt");
            for (int i = 0; i < NbAccount; i++)
            {
                myfile.WriteLine(tabAccounts[i].AccNumber);
                myfile.WriteLine(tabAccounts[i].ClientName);
                myfile.WriteLine(tabAccounts[i].AccountPin);
                if (i != NbAccount - 1)
                {
                    myfile.WriteLine(tabAccounts[i].AccBalance);
                }
                else
                {
                    myfile.Write(tabAccounts[i].AccBalance);
                }
            }
            myfile.Close();
        }

        public static void Withdrawal()//choice 2 function
        {
            for (int i = 0; i < NbAccount; i++)
            {
                if (tabAccounts[i].AccNumber.ToUpper() == searchAcc.ToUpper().Trim())
                {
                    double amount;
                    Console.Write("Entrez le montant à retirer:");
                    amount = Convert.ToDouble(Console.ReadLine());
                    while (amount < 20 || amount > 500 || amount % 20 != 0 || amount >= tabAccounts[i].AccBalance)
                    {
                        Console.Write("Montant invalide ! Réessayez:");
                        amount = Convert.ToDouble(Console.ReadLine());
                    }
                    tabAccounts[i].AccBalance -= amount;
                }
            }

        }

        private static void Deposit()//choice 1 function 
        {
            for (int i = 0; i < NbAccount; i++)
            {
                if (tabAccounts[i].AccNumber.ToUpper() == searchAcc.ToUpper().Trim())
                {


                    double Amount;
                    Console.WriteLine("\t");
                    Console.Write("Entrez le montant à déposer:");
                    Amount = Convert.ToDouble(Console.ReadLine());
                    while (Amount > 20000 || Amount < 2)
                    {
                        Console.Write("Montant invalide ! Réessayez:");
                        Amount = Convert.ToDouble(Console.ReadLine());
                    }
                    tabAccounts[i].AccBalance += Amount;
                }
            }
        }

        public static void TransStatus()//message function
        {
            Console.WriteLine("\t");
            Console.WriteLine("--- la transaction a reussi ---");
            Console.WriteLine("\t");
        }

        public static void Pause()//function to return
        {
            Console.WriteLine("\t");
            Console.WriteLine("Merci d'avoir utilises n os services\n Appuyez sur une touche pour continuer...");
            string temp = Console.ReadLine();
        }

        public static void Consultation()//choice 3 function
        {
            for (int i = 0; i < NbAccount; i++)
            {
                if (tabAccounts[i].AccNumber.ToUpper() == searchAcc.ToUpper().Trim())
                {
                    Console.WriteLine("Les infos du compte");
                    Console.WriteLine("\tNumero: " + tabAccounts[i].AccNumber);
                    Console.WriteLine("\tClient: " + tabAccounts[i].ClientName);
                    Console.WriteLine("\tNip: " + tabAccounts[i].AccountPin);
                    Console.WriteLine("\tSolde: " + tabAccounts[i].AccBalance);
                }
            }
        }

        public static void FindingAndVerifyingAccount()//function to find account by account number and then verifying the pin
        {

            Console.Write("Entrez votre numero de compte:");
            searchAcc = Console.ReadLine();
            for (int i = 0; i < NbAccount; i++)
            {
                if (tabAccounts[i].AccNumber.ToUpper() == searchAcc.ToUpper().Trim())
                {
                    Console.WriteLine("\tBienvenue "+tabAccounts[i].ClientName);
                    string pin;
                    do
                    {
                        Console.Write("Entrez votre nip:");
                        pin = Console.ReadLine();
                    }
                    while (tabAccounts[i].AccountPin.ToUpper() != pin.ToUpper().Trim());
                    found = true;
                }
            }
                if (found == false)
                {
                    Console.WriteLine("Compte non trouvé!");
                }
            
        }

        public static void ReadFileToArray()//reading file to array
        {
            
            int i = 0;

            StreamReader Myfile = new StreamReader("Accounts.txt");
            while (Myfile.EndOfStream == false)
            {
                tabAccounts[i].AccNumber = Myfile.ReadLine();
                tabAccounts[i].ClientName = Myfile.ReadLine();
                tabAccounts[i].AccountPin = Myfile.ReadLine();
                tabAccounts[i].AccBalance = Convert.ToDouble(Myfile.ReadLine());
                i++;
            }
            Myfile.Close();
            NbAccount = i;
        }
    }
}

