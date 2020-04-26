using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MethodToFindNullValInArray
{
    class Program
    {
        public static int ? FindNum(int[]arr,int  numb )
        {
            for (int i = 0; i < arr.Length; i++)
            {
                if (numb == arr[i])
                {
                    return i;
                }
            }
            return null;

        }
        static void Main(string[] args)
        {
            int[] arr1 = { 2, 3, 4, 6, 8, 77, 8 };
            int b = 6;
            int? res = FindNum(arr1, b);
            if(res.HasValue)
            {
                Console.WriteLine("The number {0} is in the array at index {1} ",b,res);
            }
            else
            {
                Console.WriteLine("The number {b} is not in the array ",b);
            }
            Console.ReadKey();
        }
    }
}
