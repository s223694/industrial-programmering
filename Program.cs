// See https://aka.ms/new-console-template for more information




using System.Globalization;


namespace SparePartsInventoryAssistant
{
    internal static class Program
    {
        // Hvad har vi, i vores inventar: 
        private static readonly HashSet<string> Components = new(StringComparer.OrdinalIgnoreCase)
        {
            "hydraulic pump",
            "PLC module",
            "servo motor"
        };

        // Ekstra Spørgsmål, og tilhørende svar. 
        private static readonly (string pattern, string answer)[] QuestionAnswers =
        {
            (
                pattern: "do you actually have any parts",
                answer: "Yes, We have these parts: hydraulic pump, PLC module, servo motor."
            ),
            (
                pattern: "do you have anything in stock at all",
                answer: "Yes, We have these parts:  hydraulic pump, PLC module, servo motor."
            )
        };
        // start prompt
        private const string Welcome = "Welcome, Which parts are you looking for?";
        // Hvis de skriver noget vi har:
        private const string FoundTemplate = "Yes, We have this in stock: {0}. Bye.";
        // Hvis vi ikke forstår deres besked
        private const string NotFound = "We dont have what you are looking for, Can i help with anything else? (Write 'exit' to end conversation)";
        private const string Prompt = "> ";

        private static void Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("— SparePartsInventoryAssistant —");
            Console.WriteLine("Write the name of a component (fx 'hydraulic pump'), ask a question or write 'exit' to end conversation.\n");
            // når koden kører: 
            while (true)
            {
                Console.WriteLine(Welcome);
                Console.Write(Prompt);
                // vi læser hvad de skriver: 
                var raw = Console.ReadLine();

                if (raw is null) continue;

                var input = Normalize(raw);
                if (input == "exit" || input == "slut")
                {
                    Console.WriteLine("Bye.");
                    return;
                }

                // 1) If (input) = components
                if (MatchesComponent(input, out var matchedComponent))
                {
                    Console.WriteLine(string.Format(FoundTemplate, matchedComponent));
                    return; // flow ends at "Slut"
                }

                // 2) Else if (input) = questions
                if (MatchesQuestion(input, out var answer))
                {
                    Console.WriteLine(answer);
                    // Efter svar: Slut
                    return;
                }

                // 3) Fallback -> Hvis vi ikke ved hvad de spørger om, har vi et fallback svar. 
                Console.WriteLine(NotFound);
                Console.WriteLine(); 
            }
        }

        // Hjælpefunktioner til at normalisere tekst og finde matches.        
        private static string Normalize(string s)
        {
            var trimmed = (s ?? string.Empty).Trim();
            var parts = trimmed.Split(new[] { ' ', '\t', '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(' ', parts).ToLowerInvariant();
        }

        private static bool MatchesComponent(string normalizedInput, out string matchedComponent)
        {
            // Hvis deres input indeholder en af vores komponenter, kan vi også retunerer
            foreach (var comp in Components)
            {
                var c = Normalize(comp);
                if (normalizedInput.Equals(c, StringComparison.OrdinalIgnoreCase) ||
                    normalizedInput.Contains(c, StringComparison.OrdinalIgnoreCase))
                {
                    matchedComponent = comp; //returnerer det komponent vi har fundet. 
                    return true;
                }
            }

            matchedComponent = string.Empty;
            return false;
        }
        // Sammenligner brugerens input med kendte spørgsmål (normaliseret, så store/små bogstaver ikke betyder noget)  
        private static bool MatchesQuestion(string normalizedInput, out string answer)
        {
            foreach (var (pattern, text) in QuestionAnswers)
            {
                var p = Normalize(pattern);
                if (normalizedInput.Equals(p, StringComparison.OrdinalIgnoreCase) ||
                    normalizedInput.Contains(p, StringComparison.OrdinalIgnoreCase))
                {
                    answer = text;
                    return true;
                }
            }

            // Ekstra logik: hvis input ligner et spørgsmål (fx "do you have ...?" eller ender med '?'), giver vi et generelt svar
            // så har vi fallback svar, hvor vi skriver hvad vi tilbyder. 
            // Igen hvis input indeholder en af komponenterne, kan vi give dem et svar, eller giver vi dem et fallback svar."
            if (normalizedInput.Contains("do you have") || normalizedInput.Contains("have any") || normalizedInput.EndsWith("?"))
            {
                // Her ser vi om vi kan finde en komponent. 
                if (MatchesComponent(normalizedInput, out var comp))
                {
                    answer = string.Format(FoundTemplate, comp);
                    return true;
                }

                // Her falder vi tilbage på svar. 
                var list = string.Join(", ", Components);
                answer = $"I am not sure what you are asking for, but this is what we offer: {list}.";
                return true;
            }

            answer = string.Empty;
            return false;
        }
    }

   
}
