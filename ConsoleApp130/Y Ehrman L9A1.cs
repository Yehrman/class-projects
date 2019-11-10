using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace L9A1 //Y Ehrman
{
    class Gradebook
    /* Design a Student Gradebook class.,Features:,Add students,Enter new grades,Get the current average 
     of any one student,Get the overall average of the whole class*/

    {

        private string[] _studentName = {} ;
        
        public void AddStudent(string StudentName)
        {
            string[] a = new string[_studentName.Length+1];
            for (int i = 0; i <_studentName.Length; i++)
            {
                a[i] = _studentName[i];
            }
            a[_studentName.Length] = StudentName;
            _studentName = a;
        }
        public string[] GetName()
        {
            return _studentName;
        }
        private int[] _Test1 = { };
        private int[] _Test2 ={ };
        private int[] _Test3 = { };
       
        public void EnterGradeTest1(int Grade)
        {
            int[] a = new int[_Test1.Length+1];
            for (int i = 0; i < _Test1.Length; i++)
            {
                a[i] = _Test1[i];
            }
            a[_Test1.Length] = Grade;
            _Test1 = a;
        }
        public void EnterGradeTest2(int Grade)
        {
            int[] a = new int[_Test2.Length + 1];
            for (int i = 0; i < _Test2.Length; i++)
            {
                a[i] = _Test2[i];
            }
            a[_Test2.Length] = Grade;
            _Test2 = a;
        }
        public void EnterGradeTest3(int Grade)
        {
            int[] a = new int[_Test3.Length + 1];
            for (int i = 0; i < _Test3.Length; i++)
            {
                a[i] = _Test3[i];
            }
            a[_Test3.Length] = Grade;
            _Test3 = a;
        }
         private int GetStudentByIndex(string name)
         {
             for (int i = 0; i < _studentName.Length; i++)
             {
                 if ( _studentName[i]==name)
                         {
                     return i;
                 }
             }
             return 0;
         }
         public int GetStudentAverage(string name)
         {
             int n = GetStudentByIndex(name);
             int a= (_Test1[n] + _Test2[n] + _Test3[n])/3;
             return a;
         }
        public int Classavg()
        {
            int sum = 0;
            for (int i = 0; i < _Test1.Length && i < _Test2.Length && i < _Test3.Length; i++)
            {
                sum += _Test1[i] + _Test2[i] + _Test3[i];
            }
            return sum /(_Test1.Length+_Test2.Length+_Test3.Length); 
        }
        
        /* This is a constructor. 
        public Gradebook()
            {
            for (int i=0;i<_studentName.Length; i++)
            {
                _studentName[i] = "Bob";
            }
          
            for (int i = 0; i < Stud1tests.Length && i< Stud3Tests.Length && i < Stud2Tests.Length;i++)
            {
                _Stud1Tests[i] = 100;
                Stud2Tests[i] = 100;
                Stud3Tests[i] = 100;
            }*/



        static void Main(string[] args)
        {
            Gradebook a = new Gradebook();
            a.AddStudent("Bob");
            a.AddStudent("Tim");
            a.AddStudent("Jose");
            a.AddStudent("Boris");
            a.AddStudent("Abdulla");
            a.EnterGradeTest1(78);
            a.EnterGradeTest1(74);
            a.EnterGradeTest1(95);
            a.EnterGradeTest2(87);
            a.EnterGradeTest2(69);
            a.EnterGradeTest2(80);
            a.EnterGradeTest3(87);
            a.EnterGradeTest3(98);
            a.EnterGradeTest3(100);
           a.EnterGradeTest1(89);
            a.EnterGradeTest1(60);
            a.EnterGradeTest2(90);
            a.EnterGradeTest2(48);
            a.EnterGradeTest3(79);
            a.EnterGradeTest3(50);
            string[] d = a.GetName();
            foreach (var item in d)
            {
                if(item=="Boris"||item=="Tim"||item=="Abdulla")
                {
                    Console.WriteLine(item); ;
                }
            }
            Console.WriteLine("The average of Bob is "+a.GetStudentAverage("Bob"));
            Console.WriteLine("The average of Tim is " + a.GetStudentAverage("Tim"));
            Console.WriteLine("The average of Jose is " + a.GetStudentAverage("Jose"));
            Console.WriteLine("The average of Boris is " + a.GetStudentAverage("Boris"));
            Console.WriteLine("The average of Abdulla is " + a.GetStudentAverage("Abdulla"));
            int c = a.Classavg();
            Console.WriteLine("The average of the entire class is " + c);
            Console.ReadKey();
        }
    }
} 
