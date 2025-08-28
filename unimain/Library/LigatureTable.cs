using System.Reflection;
using System.Text;
public class LigatureTable
{

    public string CompinationCode { get; set; } // Combination code for the ligature
    public char Base { get; set; } // Unicode representation of the ligature
    public char JoiningType { get; set; } // Unicode for the glyph in the ligature   
    public char Isolated { get; set; } // Isolated form of the ligature
    public char Final { get; set; } // Final form of the ligature
    public char Initial { get; set; } // Initial form of the ligature
    public char Medial { get; set; } // Medial form of the ligature

}

public static class LigatureForm
{

    public static readonly Dictionary<string, LigatureTable>? LigForm = new Dictionary<string, LigatureTable>
    {   // key                                      ligature code    base letter code   link type        isolated+code        final form code   initial form code   medial form code
        {"16041570", new LigatureTable { CompinationCode = "\ufef5", Base = '\ufef5', JoiningType = 'd', Isolated = '\ufef5', Final = '\ufef6', Initial = '\ufef5', Medial = '\ufef6' }},        
        {"16041571", new LigatureTable { CompinationCode = "\ufef7", Base = '\ufef7', JoiningType = 'd', Isolated = '\ufef7', Final = '\ufef8', Initial = '\ufef7', Medial = '\ufef8' }},        
        {"16041573", new LigatureTable { CompinationCode = "\ufef9", Base = '\ufef9', JoiningType = 'd', Isolated = '\ufef9', Final = '\ufefa', Initial = '\ufef9', Medial = '\ufefa' }},        
        {"16041575", new LigatureTable { CompinationCode = "\ufefb", Base = '\ufefb', JoiningType = 'd', Isolated = '\ufefb', Final = '\ufefc', Initial = '\ufefb', Medial = '\ufefc' }}
    };

    public static char GetFormForContext(LigatureTable ligature, string context)
    {
        return context.ToLower() switch
        {
            "isolated" => ligature.Isolated,
            "final" => ligature.Final,
            "initial" => ligature.Initial,
            "medial" => ligature.Medial,
            _ => ligature.Base // Default to base if context is not recognized
        };
    }

    public static string ApplyLigatures(string text, string[] statuses)
    {
        var result = new StringBuilder();
        for (int i = 0; i < text.Length; i++)
        {
            char currentChar = text[i];
            string ligatureKey = currentChar.ToString() + (i < statuses.Length ? statuses[i] : "");

            if (LigForm.TryGetValue(ligatureKey, out LigatureTable? ligature))
            {
                result.Append(GetFormForContext(ligature, statuses[i]));
            }
            else
            {
                result.Append(currentChar); // Append the character as is if no ligature found
            }
        }
        // return the processed string with ligatures applied
        return result.ToString();
    }


    public static string GetCombinationKey(string txt, int sindex)
    {
        // this is for the ligature combination code, that create a code or (key) like "16041570" for the ligature
        if (sindex + 1 < txt.Length)
            return ((int)txt[sindex]).ToString() + ((int)txt[sindex + 1]).ToString();

        return NullabilityState.Nullable.ToString();
    }
}