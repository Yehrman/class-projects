using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using org.mariuszgromada.math.mxparser;

namespace CalculatorWithEnum
{
    class Program
    {
  
     
        static void Op (string a)
        {
            Expression expression = new Expression(a);
            mXparser.consolePrint(expression.getExpressionString()+"="+ expression.calculate());
        }
        static void Main(string[] args)
        {
       
            
            while(true)
            {
                Console.WriteLine("Please type in your math operator");
                string a = Console.ReadLine();

             
                Op(a);
                Console.WriteLine();
                Console.WriteLine("Press any key to continue");
            
                Console.ReadLine();
                Console.Clear();
          
            }
            
        }
    }
}
