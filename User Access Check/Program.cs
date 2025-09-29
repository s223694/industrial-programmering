// See https://aka.ms/new-console-template for more information
using System;

using System;
using System.ComponentModel.Design;
using System.Data.SqlTypes;

using System;
    


    class Program
    {
        static void Main()
        {
            

            // definerer variablen som skal holde brugerens ID:
            uint userId;
            // Programmet bliver ved med at spørge om ID, indtil brugeren skriver et gyldigt heltal eller 'exit'.
            while (true)
            {
                // Spørger om ID
                Console.Write("Welcome, what is your ID? ");
                // Læser input.
                var idInput = Console.ReadLine();
                ExitIfRequested(idInput);
                // Hvis inputtet kan konvereteres til unit, bliver det genmt i varialen userid, hvis ikke kan de forsøge igen. 
                if (uint.TryParse(idInput, out userId))
                    break;

                Console.WriteLine("Please enter a valid integer ID, or type 'exit'.");
            }
            // En admin har altid et userId > 65536
            bool userIsAdmin = userId > 65536;

            // Laver username variablen i en string, og lader også den køre i loop. 
            string username;
            while (true)
            {
                // afhængigt af deres ID, får de én af disse 2 beskeder. 
                Console.WriteLine(userIsAdmin
                    ? "Welcome Admin, what is your username?"
                    : "Welcome regular user, what is your username?");

                // Læser input. 
                username = Console.ReadLine() ?? string.Empty;
                ExitIfRequested(username);
                // reglen for brugernavn. 
                if (username.Length >= 3)
                    break;

                Console.WriteLine("Username has to be at least three characters, please try again or type 'exit'.");
            }

            // variabler for password. 
            // Hvis userIsAdmin = True, er required length 20, hvis False, er det 16. 
            int requiredLen = userIsAdmin ? 20 : 16;
            string password;
            while (true)
            {
                Console.Write("What is your password? ");
                password = Console.ReadLine() ?? string.Empty;
                ExitIfRequested(password);
                // hvis password indeholder én af de tre symboler, bliver den sat til true
                bool symbolOk = password.Contains('$') || password.Contains('|') || password.Contains('@');
                // hvis længden er det samme eller over requiredLen, så bliver den sat til true.
                bool lengthOk = password.Length >= requiredLen;
                // Hvis begge er true, så breaker vi. 
                if (symbolOk && lengthOk)
                    break;
                // hvis kun én eller ingen er true, siger vi dette: (Vi siger også deres specifikke længde afhængigt af om de er admin eller ej. 
                Console.WriteLine("Wrong, please try again, or type 'exit'.");
                Console.WriteLine($"Password must be at least {requiredLen} characters and contain at least one of $, |, @.");
            }

     
            Console.WriteLine("Welcome");
            // (Optionally print the flags)
            // Console.WriteLine($"userIsAdmin={userIsAdmin}, usernameValid={usernameValid}, passwordValid={passwordValid}");
        }

        // Her er min hjælpefunktion, som holder øje med om de skriver exit på noget tidspunkt. 
        // Kan ses ved "ExitIfRequested". Alle input bliver sendt hertil, og hvis det er = "exit", stopper vi. 
        static void ExitIfRequested(string? input)
        {
            if (string.Equals(input, "exit", StringComparison.OrdinalIgnoreCase))
            {
                Environment.Exit(0);
            }
        }
    }
