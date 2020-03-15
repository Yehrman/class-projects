<Query Kind="Program" />

void Main()
{
//L11A3
// Given a long phrase, convert it to it's acronym.
	string phrase="Given a long phrase, convert it to it's acronym.";
	Acroynym(phrase);
}
    static void Acroynym(string phrase)
        {
            
            char[] characters = phrase.ToCharArray();
			Console.Write(characters[0]);
            for (int i = 0; i < characters.Length; i++)
            {
               bool check= char.IsWhiteSpace(characters[i]);
                if(check==true)
                {
                    Console.Write(characters[i + 1]);
                }
            }
        }
// Define other methods and classes here
