<Query Kind="Program" />

void Main()
{
	int a=17;
	Console.WriteLine("Guess the number");
	int i=0;

	while(i<10)
	{

		string guess=Console.ReadLine();
		int s=Convert.ToInt32(guess);
		if(s>a)
		{
		i++;
		Console.WriteLine("Guess lower");
		}
		else if(s<a)
		{
		i++;
			Console.WriteLine("Guess higher");
			}
			else
			{
				Console.WriteLine("You got it.It took you "+i+" chances");
				break;
			
			}
}

}
// Define other methods and classes here