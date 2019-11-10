using System;
using System.Collections.Generic;
//Generator to find multiples of 6

namespace generators
{
    class Program
    {
      static IEnumerable<int> prime(int limit)
        {
            for (int i = 2; i < limit; i++)
            {
                for (int j = 2; j < i; j++)
                {
                  if(  i % j != 0)
                    {
                        yield return i;
                    }    
                }
              
            }
        }
        static void Main(string[] args)
        {
            foreach (var item in prime(100))
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();
        }
    }
}
