using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BalancedOpenAndClose
{
    class Program
    {
        static bool Balanced(string brackets)
        {
          //Program to figure out if every opening bracket has a closing bracket
            SortedList<char, char> keyValues = new SortedList<char, char>()
            {
                {'[',']' },
                {'{','}' },
                {'(',')' },
                {'<','>' }
            };
            
            Stack <char>stack= new Stack<char>();   
            foreach (var item in brackets)
            {
                if(keyValues.Keys.Contains(item))
                {
                    stack.Push(item);
                }
                else if(keyValues.Values.Contains(item))
                {
                 //   var value = brackets[i];
                    var key = keyValues.FirstOrDefault(x => x.Value == item).Key;
                    if(stack.Contains(key))
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }

            }
            return stack.Count() == 0 ? true : false;
        }
        static void Main(string[] args)
        {
            Console.WriteLine(Balanced("{}{]{}()><><"));
            Console.ReadKey();
        }
    }
}
