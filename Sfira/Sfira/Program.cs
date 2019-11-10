using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sfira
{
    class Program
    {
        static void Main(string[] args)
        {
            int dayTracker = 0;
            int day = 1;
            int weekTracker = 0;
            int dayOfWeek = 1;
            while (day <= 49)
            {             
                if (day == dayTracker + 7)
                {
                    weekTracker++;
                    dayTracker += 7;
                    dayOfWeek = 0;
                }
                Console.WriteLine(day + ", Today is the " + day + " day of the omer that is " + weekTracker + " weeks and "+dayOfWeek+" days to the omer ");
                day++;
                dayOfWeek++;
            }    
            Console.ReadKey();
        }
    }
}
