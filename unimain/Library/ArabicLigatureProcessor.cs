using System;
using System.Collections.Generic;
using System.Text;

public static class ArabicLigatureProcessor
{
    /// <summary>
    /// Gets the appropriate ligature form based on context
    /// </summary>
    private static char GetFormForContext(LigatureTable ligature, string context)
    {
        // Return the appropriate form based on context, 
        // if context is not recognized, return the base form
        return context.ToLower() switch
        {
            "isolated" => ligature.Isolated,
            "initial" => ligature.Initial,
            "medial" => ligature.Medial,
            "final" => ligature.Final,
            _ => ligature.Base
        };
    }


    /// <summary>
    /// Generates a combination key from two Unicode characters
    /// </summary>
    private static string GetCombinationKey(char char1, char char2)
    {
        return ((int)char1).ToString() + ((int)char2).ToString();
    }

    
    /// <summary>
    /// Gets all available ligature combinations
    /// </summary>
    public static Dictionary<string, LigatureTable> GetAvailableLigatures()
    {
        return LigatureForm.LigForm ?? new Dictionary<string, LigatureTable>();
    }

    /// <summary>
    /// Checks if a character combination forms a ligature
    /// </summary>
    public static bool IsLigatureCombination(char char1, char char2)
    {
        string key = GetCombinationKey(char1, char2);
        return LigatureForm.LigForm.ContainsKey(key);
    }

    /// <summary>
    /// Gets ligature information for a specific character combination
    /// </summary>
    public static LigatureTable GetLigatureInfo(char char1, char char2)
    {
        string key = GetCombinationKey(char1, char2);
        return LigatureForm.LigForm.TryGetValue(key, out var ligature) ? ligature : null;
    }

    internal static char GetLigatureForm(string currLetterStatus, params char[] prevCurrNex)
    {
        // can I get the length of prevCurrNex?
        byte length = (byte)prevCurrNex.Length;
        if (length < 1 || length > 3)
        {
            throw new ArgumentException("prevCurrNex must contain 1 to 3 characters.");            
        }
        
        // prevCurrNex[0] is the previous character
        // prevCurrNex[1] is the current character
        // prevCurrNex[2] is the next character
        
        string key = GetCombinationKey(prevCurrNex[1], prevCurrNex[2]);
        if (LigatureForm.LigForm.TryGetValue(key, out LigatureTable? ligature))
        {
            return GetFormForContext(ligature, currLetterStatus);
        }
           
        //throw new NotImplementedException();
        // for now I will return the '\0'
        return ' ';
    }
}
