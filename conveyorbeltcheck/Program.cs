// See https://aka.ms/new-console-template for more information


class ConveyorBeltCapacityCheck
{
    // herinde sker koden
    static void Main(string[] args)
    {
        // Spørger bruger hvor mange motorer? (gemmer i variabel x)
        Console.Write("How many motors ? ");
        int x = int.Parse(Console.ReadLine());

        // spørger bruger hvor mange kg? (gemmer i variable y) 
        Console.Write("How many kg ? ");
        float y = float.Parse(Console.ReadLine());

        // Hvis kg = 0 fejl 
        if (y == 0)
        {
            Console.WriteLine("Error: weight cannot be zero.");
            return;
        }

        // laver ratio til en variabel
        float ratio = (float)y/x;

        // opreation der vurderer om vi kan eller ej.
        if (ratio <= 5.6f)
        {
            Console.WriteLine("Yes, can carry.");
        }
        else
        {
            Console.WriteLine("No, can't carry.");
        }
    } 
}     