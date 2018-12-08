﻿using System;
using System.Linq;

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
                
                var parser = new Parser(line);
                var syntaxTree = parser.Parse();
                var color = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkGray;
                PrintTree(syntaxTree.Root);
                Console.ForegroundColor = color;

                if(parser.Diagnostics.Any())
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    foreach(var error in parser.Diagnostics)
                        Console.WriteLine(error);
                        
                    Console.ForegroundColor = color;
                }                          
            }
        }

        static void PrintTree(SyntaxNode node, string indent = "", bool isLast = true)
        {
            var marker = isLast ? "└──" : "├──";
            Console.Write(indent);
            Console.Write(marker);
            Console.Write(node.Kind);
            if(node is SyntaxToken t && t.Value != null)
            {
                Console.Write(" ");
                Console.Write(t.Value);
            }
            Console.WriteLine();
            indent += isLast ? "    " : "│   ";
            var lastChild = node.GetChildren().LastOrDefault();            
            foreach (var child in node.GetChildren())
                PrintTree(child, indent, child == lastChild);            
        }
    }
}
