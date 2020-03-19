using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SortedListExersize
{
    //L15A1
    class Program
    {
        //Loop through the array and tally up how many of each word/number there are.  
        //Use a sortedList to keep track of how many of each word there are.

        static void Main(string[] args)
        {

            int[] arr = {1,3,2,2,4,5,6,7,6,7,8,4,2,9 };
            SortedList sortedList = new SortedList();
            
            for (int i = 0; i < arr.Length; i++)
            { 
                if(sortedList.Count==0)
                {
                    sortedList.Add(arr[i], 1);
                }

                else
                {
                    if (sortedList.Contains(arr[i]))
                    {
                        int value =(int) sortedList[arr[i]];
                        
                        sortedList.Remove(arr[i]);
                        sortedList.Add(arr[i], value+1);
                    }
                    else
                    {
                        sortedList.Add(arr[i], 1);
                    }
                }
            }
            Console.WriteLine("keys");
            foreach (var item in sortedList.Keys)
            {
                Console.Write(item+" ");
            }
            Console.WriteLine();
            Console.WriteLine("values");
            foreach (var item in sortedList.Values)
            {
                Console.Write(item+" ");
            }
         
            Console.ReadKey();
        }
    }
}
