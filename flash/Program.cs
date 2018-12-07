using System;

namespace Flash
{
    class Program
    {
        static void Main(string[] args)
        {
            while(true)
            {
                Console.Write("> ");
                var line = Console.ReadLine();
                if(string.IsNullOrWhiteSpace(line))
                    return;
                
                foreach(var token in new Lexer(line).GetTokens())
                {
                    Console.Write($"{token.Kind} '{token.Text}'");
                    if(token.Value != null)
                        Console.Write($" {token.Value}");
                    
                    Console.WriteLine();
                }
            }
        }
    }
}
