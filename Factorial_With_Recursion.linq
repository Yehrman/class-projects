<Query Kind="Program" />

void Main()
{
	Console.Write(factorial(6));
}
 int factorial(int num)
{
if(num<=1)
{
return 1;
}
else
{
int sum=num;
 sum*=factorial(num-1);
return sum;
}
}
// Define other methods and classes here
