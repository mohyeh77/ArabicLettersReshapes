
public static class CharacterStatus
{
    public enum Form
    {
        Isolated,
        Initial,
        Medial,
        Final
    }

    private static readonly char[] JoiningTypes;

    static CharacterStatus()
    {
        // Initialize lookup table for U+0621 to U+064A with 'D' as default
        JoiningTypes = new char['\u064A' - '\u0621' + 1];
        Array.Fill(JoiningTypes, 'D');

        // Set Non-joining 'U'
        JoiningTypes['\u0621' - '\u0621'] = 'U'; // ء Hamza

        // Set Right-joining 'R'
        JoiningTypes['\u0622' - '\u0621'] = 'R'; // آ Alef with Madda
        JoiningTypes['\u0623' - '\u0621'] = 'R'; // أ Alef with Hamza above
        JoiningTypes['\u0624' - '\u0621'] = 'R'; // ؤ Waw with Hamza above
        JoiningTypes['\u0625' - '\u0621'] = 'R'; // إ Alef with Hamza below
        JoiningTypes['\u0627' - '\u0621'] = 'R'; // ا Alef
        JoiningTypes['\u0629' - '\u0621'] = 'R'; // ة Teh Marbuta
        JoiningTypes['\u062F' - '\u0621'] = 'R'; // د Dal
        JoiningTypes['\u0630' - '\u0621'] = 'R'; // ذ Thal
        JoiningTypes['\u0631' - '\u0621'] = 'R'; // ر Reh
        JoiningTypes['\u0632' - '\u0621'] = 'R'; // ز Zain
        JoiningTypes['\u0648' - '\u0621'] = 'R'; // و Waw
        JoiningTypes['\u0649' - '\u0621'] = 'R'; // ى Alef Maksura
    }

    private static char GetJoiningType(char c)
    {
    // what if the (C) is space \u0020 or ' ' or 32
    // what we have do here
        if (c < '\u0621' || c > '\u064A')
        {
            return 'U';  // Non-joining for non-Arabic characters
        }
        return JoiningTypes[c - '\u0621'];
    }

    /// <summary>
    /// Determines if a character can join with the character that precedes it in the string.
    /// For Arabic, this means the character can be in a Final or Medial form.
    /// </summary>
    private static bool CanJoinWithPrevious(char c)
    {
        char joiningType = GetJoiningType(c);
        return joiningType == 'D' || joiningType == 'R'; // Dual-joining and Right-joining characters can join with a previous character.
    }

    /// <summary>
    /// Determines if a character can join with the character that follows it in the string.
    /// For Arabic, this means the character can be in an Initial or Medial form.
    /// </summary>
    private static bool CanJoinWithNext(char c)
    {
        // Only Dual-joining characters can join with a following character.
        return GetJoiningType(c) == 'D';
    }


    public static List<char> ComputeStatuses(char[]? arr)
    {
        // from here we get the status of each character and it's glyph form
        //List<char> LetterInfo = new List<char>(); //  letter info is a list of char
        var LetterInfo = new List<char>(); 

        if (arr!=null)
        {
            LetterInfo.Capacity = arr.Length; 
        }
        
        char LetterData =' ';
        
        for (int i = 0; i < arr?.Length; i++)
        {
            char? prev = i > 0 ? arr[i - 1] : (char?)' ';
            char curr = arr[i];
            char? nextChar = i < arr.Length - 1 ? arr[i + 1] : (char?)' ';
            // for later use .... 
            char? afterNextChar = i < arr.Length - 2 ? arr[i + 2] : (char?)' ';

            // A character connects to the one before it if the previous character can join forward
            // AND the current character can join backward.
            #region TheBooleanFamily
            bool connectedToPrev = prev.HasValue && CanJoinWithNext(prev.Value) && CanJoinWithPrevious(curr);
            bool connectedToNext = nextChar.HasValue && CanJoinWithNext(curr) && CanJoinWithPrevious(nextChar.Value);
            bool connectedToAfterNext = afterNextChar.HasValue && CanJoinWithNext(nextChar.Value) && CanJoinWithPrevious(afterNextChar.Value); 
            #endregion

            string currLetterStatus = GetConnectionStatus(connectedToPrev, connectedToNext);

            var PrevValue = prev.HasValue ? prev : ' ';
            var NextValue = nextChar.HasValue ? nextChar : ' ';

            if (ArabicLigatureProcessor.IsLigatureCombination(curr, NextValue.Value))
            {
                char LigatureForm = ArabicLigatureProcessor.GetLigatureForm(currLetterStatus, PrevValue.Value, curr, NextValue.Value);

                LetterInfo?.Add(LigatureForm);
                // I should a space ' ' after forming the ligature
                // but if I do that I lose the medial form for another ligature like مجم
                i++;
                // LetterInfo?.Add(' '); // Add a space after the ligature, not sure if this is needed
                continue;
            }
            else if (curr == ' ' || curr == '\n' || curr == '\r')
            {
                  LetterInfo?.Add(curr); // Add space or newline as is
            }

                #region test_good
                // I think here is a good place to use ArabicLigatureProcessor
                // if found we have to scape next char 
                // here I have prev and curr and next char & I can also get status of all of them 
                // so I can create a Method that will take these 3 chars and status of them 
                // for safe code only I think of this

                //var PrevValue = prev.HasValue ? prev : '\0';
                //var NextValue = nextChar.HasValue ? nextChar : '\0';

                //char LigatureForm = ArabicLigatureProcessor.GetLigatureForm(currLetterStatus, PrevValue.Value, curr, NextValue.Value); 
                #endregion

            // carry the correct char and correct glyph form
            // its return ArabicLetterTable.ArabicLetter
            LetterData = ArabicLetterTable.GetGlyphForm(curr, currLetterStatus);

            LetterInfo?.Add(LetterData);
        }

        return LetterInfo;
    }

    internal static string GetConnectionStatus(bool connectedToPrev, bool connectedToNext)
    {
        return (connectedToPrev, connectedToNext) switch
        {
            (false, false) => "isolated",
            (true, true) => "medial",
            (false, true) => "initial",
            (true, false) => "final"
        };
    }
}
