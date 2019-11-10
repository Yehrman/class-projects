using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp130
{
    class Gradebook
    /* Design a Student Gradebook class.,Features:,Add students,Enter new grades,Get the current average 
     of any one student,Get the overall average of the whole class*/

    {

        public string[] _studentName = { "Zalman", "", "" };
       /* public string[] studentName
        {
          //  get;
         //   set;
        }*/
       public int[] Test1 = new int[3];
      public  int[] Test2 = new int[3];
       public int[] Test3 = new int[3];
       /*  public int[] test1;
        
             public int[] test2;
 public int[] test3;
           {
             //  get;
               set;
           }

           {
             //  get;
             //  set;
           }
           public int[] test3
           {
              get;
               set;

           }

           {
                   return _Test1;
               }
               set
               {
                   for (int i = 0; i < test1.Length; i++)
                   {
                       if (test1[i] < 101)
                       {

                       }
                       else
                       {
                           Console.WriteLine("Please enter a number between 0-100");
                       }
                   }
               }
           }
           public int[] test2
           {
               get
               {
                   return _Test2;
               }
               set
               {
                   for (int i = 0; i < test2.Length; i++)
                   {
                       if (test2[i] < 101)
                       {
                           Console.WriteLine(test2[i]);
                       }
                       else
                       {
                           Console.WriteLine("Please enter a number between 0-100");
                       }
                   }

               }
           }
           public int[] test3
           {
               get
               {
                   return _Test3;
               }
               set
               {
                  for (int i = 0; i < test3.Length; i++)
                   {
                       if (test3[i] < 101)
                       {
                         _test3[]=
                       }
                       else
                       {
                           Console.WriteLine("Please enter a number between 0-100");
                       }
                   }
               }
           }*/
        public int t1avg(int avg)
        {
            int sum = 0;
            for (int i = 0; i < Test1.Length; i++)
            {
                sum += Test1[i];
            }
            return sum / 3;
        }
        public int t2avg(int avg)
        {
            int sum = 0;
            for (int i = 0; i < Test2.Length; i++)
            {
                sum += Test2[i];
            }
            return sum / 3;
        }
        public int t3avg(int avg)
        {
            int sum = 0;
            for (int i = 0; i < Test3.Length; i++)
            {
                sum += Test3[i];
            }
            return sum / 3;
        }

        static void Main(string[] args)
        {


            Gradebook a = new Gradebook();

            
            a._studentName[1] = "Shimon";
            a._studentName[2] = "Boruch";
            a.Test1[0] = 83;
            a.Test1[1] = 89;
            a.Test1[2] = 90;
            a.Test2[0] = 92;
            a.Test2[1] = 82;
            a.Test2[2] = 76;
            a.Test3[0] = 89;
            a.Test3[1] = 68;
            a.Test3[2] = 79;
            int b= a.t1avg(3);
            int c = a.t2avg(3);
            int d = a.t3avg(3);
            Console.WriteLine("The average of test1 is "+b);
            Console.WriteLine("The average of test2 is " + c);
            Console.WriteLine("The average of test2 is " + d);


            Console.ReadKey();
        }
    }
} 
