<Query Kind="Program" />

void Main()
{
	   int?[,] arr =
            {
                {1,2,null,4,5,6 },
                {7,8,9,1,null,3, }
            };
	//ArrNullCounter(arr);
	
	int? a = 22;
	//Ternary operator
	 int? b=a==23?23:0;
            Console.WriteLine(b);
			
			//null coalecing operator
			  Console.Write(a ?? null);
}
//Null condional operator
 static void ArrNullCounter(int?[,] Twodarr)
        {
           int a=0;
            for (int i = 0; i < Twodarr.GetLength(0); i++)
            {
                for (int j = 0; j < Twodarr.GetLength(1); j++)
                {
                    Console.WriteLine(Twodarr?[i, j]);
                   
                }
            }
        }
// Define other methods and classes here
