<Query Kind="Program" />

void Main()
{
//L11A4
// Given a word ending with the suffix 'ing', return the word without the suffix.
string a="talking";
	 Console.Write(ingSubtracter(a));        
}
 static string ingSubtracter(string word)
        {
            string a = "ing";
			string x=string.Empty;
            for (int i = 0; i < word.Length; i++)
            {
                if(word.Substring(i)==a)
                {
                   x=  word.Remove(i);
                }
            }
            return x ;
        }
// Define other methods and classes here
