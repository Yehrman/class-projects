<Query Kind="Program" />

void Main()
{
	string a="hello";
	string b="good";
	int c=3;
	int d=4;
	for(int i=1;i<=100;i++)
	{
	if(i%c==0)
	{
	Console.WriteLine(a);
	}
else	if(i%d==0)
	{
	Console.WriteLine(b);
	}
	else if(i%c==0&&i%d==0)
	{
	Console.WriteLine(a+b);
	}
	else
	{
	Console.WriteLine(i);
	}
	}
}

// Define other methods and classes here
