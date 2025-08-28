using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Linq;

public static class ArabicShaperWords
{

    public static Stack<string> reversLetters = new Stack<string>();


    public static string ShapeWord(string word, out Stack<string> revLetters)
    {
        
        StringBuilder shaped = new StringBuilder();
        reversLetters = new Stack<string>();
        
        // important to check every letter in the word
        char[] letters = word.ToCharArray();
        
        // Here to get and store a letter form even if a single letter or ligature form
        List<char> glyphData = CharacterStatus.ComputeStatuses(letters);               // check every status of the letter and reshape it 
        
        for (int i = 0; i < glyphData.Count; i++)
        {
            shaped.Append(glyphData[i].ToString());
            reversLetters.Push(glyphData[i].ToString());
        }

        revLetters = reversLetters;
        return shaped.ToString();
    }

    public static string ToUnicodeString(this string str)
    {
        if (string.IsNullOrEmpty(str))        
            return string.Empty;
        
        StringBuilder sb = new StringBuilder();
        foreach (char c in str)
        {
            sb.AppendFormat("\\u{0:X4}", (int)c);
        }
        return sb.ToString();
    }

    public static string UnicodeOf(string str) => string.IsNullOrEmpty(str) ? string.Empty : str.ToUnicodeString();
}