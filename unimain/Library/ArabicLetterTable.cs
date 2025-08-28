using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

public static class ArabicLetterTable
{
    // Enum representing the positions based on the patterns in the image.
    // Note: The labels in the image are used, but mapped to standard Arabic shaping terms.
    // [0,1,0] -> Isolated (no connections)
    // [0,1,1] -> FinalR (this seems to correspond to 'Initial' in standard terms, as it connects only to the right/next)
    // [1,1,1] -> Medial (connects to both left/previous and right/next)
    // [1,1,0] -> FinalL (this corresponds to 'Final' in standard terms, as it connects only to the left/previous)
    // We're using these names to match the image, but the logic treats FinalR as Initial and FinalL as Final.

    public enum Position
    {
        Isolated,
        Initial,
        Medial,
        Final,
        spaceOrNone
    }


    //glyph_unicode-base_unicode-code-name-position-join_type-range-isolated-initial-medial-final-connect_right-connect_left-codepoint

    public struct ArabicLetter
    {
        public char Base;
        public string JoiningType;
        public char Isolated;
        public char Final;
        public char Initial;
        public char Medial;        

        public string? GlyphName { get; internal set; }

        public override string ToString()
        {
            return $"{Base} ({GlyphName}) - {JoiningType}: I:{Isolated}, F:{Final}, In:{Initial}, M:{Medial}";
        }
    }

    // The "table data" - a dictionary mapping base characters to their letter info.
    // This acts as the database/table with rows for each letter pattern/form.
    // For demonstration, hardcoded with a few common letters: Alef (R), Ba (D), Ra (R), Ta (D).
    // In a real app, this could be loaded from a file or database.
    // Unicode values are from standard Arabic presentation forms (\uFE8x range).
    public static readonly Dictionary<char, ArabicLetter> LetterTable = new Dictionary<char, ArabicLetter>
    {

        {'\u0621', new ArabicLetter { Base='\u0621', JoiningType = "u", Isolated = '\ufe80', Final = '\ufe80', Initial = '\ufe80', Medial = '\ufe80', GlyphName = "arabic letter hamza"}},
        {'\u0622', new ArabicLetter { Base='\u0622', JoiningType = "r", Isolated = '\ufe81', Final = '\ufe82', Initial = '\ufe81', Medial = '\ufe81', GlyphName = "arabic letter alef with madda above"}},
        {'\u0623', new ArabicLetter { Base='\u0623', JoiningType = "r", Isolated = '\ufe83', Final = '\ufe84', Initial = '\ufe83', Medial = '\ufe83', GlyphName = "arabic letter alef with hamza above"}},
        {'\u0624', new ArabicLetter { Base='\u0624', JoiningType = "r", Isolated = '\ufe85', Final = '\ufe86', Initial = '\ufe85', Medial = '\ufe85', GlyphName = "arabic letter waw with hamza above"}},
        {'\u0625', new ArabicLetter { Base='\u0625', JoiningType = "r", Isolated = '\ufe87', Final = '\ufe88', Initial = '\ufe87', Medial = '\ufe87', GlyphName = "arabic letter alef with hamza below"}},
        {'\u0626', new ArabicLetter { Base='\u0626', JoiningType = "d", Isolated = '\ufe89', Final = '\ufe8a', Initial = '\ufe8b', Medial = '\ufe8c', GlyphName = "arabic letter yeh with hamza above"}},
        {'\u0627', new ArabicLetter { Base='\u0627', JoiningType = "r", Isolated = '\ufe8d', Final = '\ufe8e', Initial = '\ufe8d', Medial = '\ufe8d', GlyphName = "arabic letter alef"}},
        {'\u0628', new ArabicLetter { Base='\u0628', JoiningType = "d", Isolated = '\ufe8f', Final = '\ufe90', Initial = '\ufe91', Medial = '\ufe92', GlyphName = "arabic letter beh"}},
        {'\u0629', new ArabicLetter { Base='\u0629', JoiningType = "r", Isolated = '\ufe93', Final = '\ufe94', Initial = '\ufe93', Medial = '\ufe93', GlyphName = "arabic letter teh marbuta"}},
        {'\u062a', new ArabicLetter { Base='\u062a', JoiningType = "d", Isolated = '\ufe95', Final = '\ufe96', Initial = '\ufe97', Medial = '\ufe98', GlyphName = "arabic letter teh"}},
        {'\u062b', new ArabicLetter { Base='\u062b', JoiningType = "d", Isolated = '\ufe99', Final = '\ufe9a', Initial = '\ufe9b', Medial = '\ufe9c', GlyphName = "arabic letter theh"}},
        {'\u062c', new ArabicLetter { Base='\u062c', JoiningType = "d", Isolated = '\ufe9d', Final = '\ufe9e', Initial = '\ufe9f', Medial = '\ufea0', GlyphName = "arabic letter jeem"}},
        {'\u062d', new ArabicLetter { Base='\u062d', JoiningType = "d", Isolated = '\ufea1', Final = '\ufea2', Initial = '\ufea3', Medial = '\ufea4', GlyphName = "arabic letter hah"}},
        {'\u062e', new ArabicLetter { Base='\u062e', JoiningType = "d", Isolated = '\ufea5', Final = '\ufea6', Initial = '\ufea7', Medial = '\ufea8', GlyphName = "arabic letter khah"}},
        {'\u062f', new ArabicLetter { Base='\u062f', JoiningType = "r", Isolated = '\ufea9', Final = '\ufeaa', Initial = '\ufea9', Medial = '\ufea9', GlyphName = "arabic letter dal"}},
        {'\u0630', new ArabicLetter { Base='\u0630', JoiningType = "r", Isolated = '\ufeab', Final = '\ufeac', Initial = '\ufeab', Medial = '\ufeab', GlyphName = "arabic letter thal"}},
        {'\u0631', new ArabicLetter { Base='\u0631', JoiningType = "r", Isolated = '\ufead', Final = '\ufeae', Initial = '\ufead', Medial = '\ufead', GlyphName = "arabic letter reh"}},
        {'\u0632', new ArabicLetter { Base='\u0632', JoiningType = "r", Isolated = '\ufeaf', Final = '\ufeb0', Initial = '\ufeaf', Medial = '\ufeb0', GlyphName = "arabic letter zain"}},
        {'\u0633', new ArabicLetter { Base='\u0633', JoiningType = "d", Isolated = '\ufeb1', Final = '\uFEB2', Initial = '\uFEB3', Medial = '\uFEB4', GlyphName = "arabic letter seen"}},
        {'\u0634', new ArabicLetter { Base='\u0634', JoiningType = "d", Isolated = '\uFEB5', Final = '\uFEB6', Initial = '\uFEB7', Medial = '\uFEB8', GlyphName = "arabic letter sheen"}},
        {'\u0635', new ArabicLetter { Base='\u0635', JoiningType = "d", Isolated = '\uFEB9', Final = '\ufeba', Initial = '\ufebb', Medial = '\ufebc', GlyphName = "arabic letter sad"}},
        {'\u0636', new ArabicLetter { Base='\u0636', JoiningType = "d", Isolated = '\ufebd', Final = '\ufebe', Initial = '\ufebf', Medial = '\ufec0', GlyphName = "arabic letter dad"}},
        {'\u0637', new ArabicLetter { Base='\u0637', JoiningType = "d", Isolated = '\ufec1', Final = '\ufec2', Initial = '\ufec3', Medial = '\ufec4', GlyphName = "arabic letter tah"}},
        {'\u0638', new ArabicLetter { Base='\u0638', JoiningType = "d", Isolated = '\ufec5', Final = '\ufec6', Initial = '\ufec7', Medial = '\ufec8', GlyphName = "arabic letter zah"}},
        {'\u0639', new ArabicLetter { Base='\u0639', JoiningType = "d", Isolated = '\ufec9', Final = '\ufeca', Initial = '\ufecb', Medial = '\ufecc', GlyphName = "arabic letter ain"}},
        {'\u063a', new ArabicLetter { Base='\u063a', JoiningType = "d", Isolated = '\ufecd', Final = '\ufece', Initial = '\ufecf', Medial = '\ufed0', GlyphName = "arabic letter ghain"}},
        {'\u063b', new ArabicLetter { Base='\u063b', JoiningType = "r", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "arabic letter keheh with two dots above"}},
        {'\u063c', new ArabicLetter { Base='\u063c', JoiningType = "r", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "arabic letter keheh with three dots below"}},
        {'\u063d', new ArabicLetter { Base='\u063d', JoiningType = "u", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "arabic letter farsi yeh with inverted v"}},
        {'\u063e', new ArabicLetter { Base='\u063e', JoiningType = "u", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "arabic letter farsi yeh with two dots above"}},
        {'\u063f', new ArabicLetter { Base='\u063f', JoiningType = "u", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "arabic letter farsi yeh with three dots above"}},
        {'\u0640', new ArabicLetter { Base='\u0640', JoiningType = "d", Isolated = '\u0640', Final = '\u0640', Initial = '\u0640', Medial = '\u0640', GlyphName = "arabic tatweel"}},
        {'\u0641', new ArabicLetter { Base='\u0641', JoiningType = "d", Isolated = '\ufed1', Final = '\ufed2', Initial = '\ufed3', Medial = '\ufed4', GlyphName = "arabic letter feh"}},
        {'\u0642', new ArabicLetter { Base='\u0642', JoiningType = "d", Isolated = '\ufed5', Final = '\ufed6', Initial = '\ufed7', Medial = '\ufed8', GlyphName = "arabic letter qaf"}},
        {'\u0643', new ArabicLetter { Base='\u0643', JoiningType = "d", Isolated = '\ufed9', Final = '\ufeda', Initial = '\ufedb', Medial = '\ufedc', GlyphName = "arabic letter kaf"}},
        {'\u0644', new ArabicLetter { Base='\u0644', JoiningType = "d", Isolated = '\ufedd', Final = '\ufede', Initial = '\ufedf', Medial = '\ufee0', GlyphName = "arabic letter lam"}},
        {'\u0645', new ArabicLetter { Base='\u0645', JoiningType = "d", Isolated = '\ufee1', Final = '\ufee2', Initial = '\ufee3', Medial = '\ufee4', GlyphName = "arabic letter meem"}},
        {'\u0646', new ArabicLetter { Base='\u0646', JoiningType = "d", Isolated = '\ufee5', Final = '\ufee6', Initial = '\ufee7', Medial = '\ufee8', GlyphName = "arabic letter noon"}},
        {'\u0647', new ArabicLetter { Base='\u0647', JoiningType = "d", Isolated = '\ufee9', Final = '\ufeea', Initial = '\ufeeb', Medial = '\ufeec', GlyphName = "arabic letter heh"}},
        {'\u0648', new ArabicLetter { Base='\u0648', JoiningType = "r", Isolated = '\ufeed', Final = '\ufeee', Initial = '\ufeed', Medial = '\ufeed', GlyphName = "arabic letter waw"}},
        {'\u0649', new ArabicLetter { Base='\u0649', JoiningType = "r", Isolated = '\ufeef', Final = '\ufef0', Initial = '\ufeef', Medial = '\ufeef', GlyphName = "arabic letter alef maksura"}},
        {'\u064a', new ArabicLetter { Base='\u064a', JoiningType = "d", Isolated = '\ufef1', Final = '\ufef2', Initial = '\ufef3', Medial = '\ufef4', GlyphName = "arabic letter yeh"}},
        {'\ufe80', new ArabicLetter { Base='\ufe80', JoiningType = "u", Isolated = '\ufe80', Final = '\ufe80', Initial = '\ufe80', Medial = '\ufe80', GlyphName = "arabic letter hamza isolated form"}},
        {'\ufe81', new ArabicLetter { Base='\ufe81', JoiningType = "r", Isolated = '\ufe81', Final = '\ufe82', Initial = '\ufe81', Medial = '\ufe81', GlyphName = "arabic letter alef with madda above isolated form"}},
        {'\ufe82', new ArabicLetter { Base='\ufe82', JoiningType = "r", Isolated = 'n', Final = '\ufe82', Initial = 'n', Medial = 'n', GlyphName = "arabic letter alef with madda above final form"}},
        {'\ufe83', new ArabicLetter { Base='\ufe83', JoiningType = "r", Isolated = '\ufe83', Final = '\ufe84', Initial = '\ufe83', Medial = '\ufe83', GlyphName = "arabic letter alef with hamza above isolated form"}},
        {'\ufe84', new ArabicLetter { Base='\ufe84', JoiningType = "r", Isolated = 'n', Final = '\ufe84', Initial = 'n', Medial = 'n', GlyphName = "arabic letter alef with hamza above final form"}},
        {'\ufe85', new ArabicLetter { Base='\ufe85', JoiningType = "r", Isolated = '\ufe85', Final = '\ufe86', Initial = '\ufe85', Medial = 'n', GlyphName = "arabic letter waw with hamza above isolated form"}},
        {'\ufe86', new ArabicLetter { Base='\ufe86', JoiningType = "r", Isolated = 'n', Final = '\ufe86', Initial = 'n', Medial = 'n', GlyphName = "arabic letter waw with hamza above final form"}},
        {'\ufe87', new ArabicLetter { Base='\ufe87', JoiningType = "r", Isolated = '\ufe87', Final = '\ufe88', Initial = '\ufe87', Medial = 'n', GlyphName = "arabic letter alef with hamza below isolated form"}},
        {'\ufe88', new ArabicLetter { Base='\ufe88', JoiningType = "r", Isolated = 'n', Final = '\ufe88', Initial = 'n', Medial = 'n', GlyphName = "arabic letter alef with hamza below final form"}},
        {'\ufe89', new ArabicLetter { Base='\ufe89', JoiningType = "r", Isolated = '\ufe89', Final = 'n', Initial = '\ufe89', Medial = 'n', GlyphName = "arabic letter yeh with hamza above isolated form"}},
        {'\ufe8a', new ArabicLetter { Base='\ufe8a', JoiningType = "r", Isolated = 'n', Final = '\ufe8a', Initial = 'n', Medial = 'n', GlyphName = "arabic letter yeh with hamza above final form"}},
        {'\ufe8b', new ArabicLetter { Base='\ufe8b', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufe8b', Medial = 'n', GlyphName = "arabic letter yeh with hamza above initial form"}},
        {'\ufe8c', new ArabicLetter { Base='\ufe8c', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufe8c', GlyphName = "arabic letter yeh with hamza above medial form"}},
        {'\ufe8d', new ArabicLetter { Base='\ufe8d', JoiningType = "r", Isolated = '\ufe8d', Final = '\ufe8e', Initial = '\ufe8d', Medial = 'n', GlyphName = "arabic letter alef isolated form"}},
        {'\ufe8e', new ArabicLetter { Base='\ufe8e', JoiningType = "r", Isolated = 'n', Final = '\ufe8e', Initial = 'n', Medial = 'n', GlyphName = "arabic letter alef final form"}},
        {'\ufe8f', new ArabicLetter { Base='\ufe8f', JoiningType = "d", Isolated = '\ufe8f', Final = '\ufe90', Initial = '\ufe8f', Medial = '\ufe92', GlyphName = "arabic letter beh isolated form"}},
        {'\ufe90', new ArabicLetter { Base='\ufe90', JoiningType = "r", Isolated = 'n', Final = '\ufe90', Initial = 'n', Medial = 'n', GlyphName = "arabic letter beh final form"}},
        {'\ufe91', new ArabicLetter { Base='\ufe91', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufe91', Medial = 'n', GlyphName = "arabic letter beh initial form"}},
        {'\ufe92', new ArabicLetter { Base='\ufe92', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufe92', GlyphName = "arabic letter beh medial form"}},
        {'\ufe93', new ArabicLetter { Base='\ufe93', JoiningType = "r", Isolated = '\ufe93', Final = '\ufe94', Initial = '\ufe93', Medial = 'n', GlyphName = "arabic letter teh marbuta isolated form"}},
        {'\ufe94', new ArabicLetter { Base='\ufe94', JoiningType = "r", Isolated = 'n', Final = '\ufe94', Initial = 'n', Medial = 'n', GlyphName = "arabic letter teh marbuta final form"}},
        {'\ufe95', new ArabicLetter { Base='\ufe95', JoiningType = "d", Isolated = '\ufe95', Final = '\ufe96', Initial = '\ufe97', Medial = '\ufe98', GlyphName = "arabic letter teh isolated form"}},
        {'\ufe96', new ArabicLetter { Base='\ufe96', JoiningType = "r", Isolated = 'n', Final = '\ufe96', Initial = 'n', Medial = 'n', GlyphName = "arabic letter teh final form"}},
        {'\ufe97', new ArabicLetter { Base='\ufe97', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufe97', Medial = 'n', GlyphName = "arabic letter teh initial form"}},
        {'\ufe98', new ArabicLetter { Base='\ufe98', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufe98', GlyphName = "arabic letter teh medial form"}},
        {'\ufe99', new ArabicLetter { Base='\ufe99', JoiningType = "d", Isolated = '\ufe99', Final = '\ufe9a', Initial = '\ufe9b', Medial = '\ufe9c', GlyphName = "arabic letter theh isolated form"}},
        {'\ufe9a', new ArabicLetter { Base='\ufe9a', JoiningType = "r", Isolated = 'n', Final = '\ufe9a', Initial = 'n', Medial = 'n', GlyphName = "arabic letter theh final form"}},
        {'\ufe9b', new ArabicLetter { Base='\ufe9b', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufe9b', Medial = 'n', GlyphName = "arabic letter theh initial form"}},
        {'\ufe9c', new ArabicLetter { Base='\ufe9c', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufe9c', GlyphName = "arabic letter theh medial form"}},
        {'\ufe9d', new ArabicLetter { Base='\ufe9d', JoiningType = "d", Isolated = '\ufe9d', Final = '\ufe9e', Initial = '\ufe9f', Medial = '\ufea0', GlyphName = "arabic letter jeem isolated form"}},
        {'\ufe9e', new ArabicLetter { Base='\ufe9e', JoiningType = "r", Isolated = 'n', Final = '\ufe9e', Initial = 'n', Medial = 'n', GlyphName = "arabic letter jeem final form"}},
        {'\ufe9f', new ArabicLetter { Base='\ufe9f', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufe9f', Medial = 'n', GlyphName = "arabic letter jeem initial form"}},
        {'\ufea0', new ArabicLetter { Base='\ufea0', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufea0', GlyphName = "arabic letter jeem medial form"}},
        {'\ufea1', new ArabicLetter { Base='\ufea1', JoiningType = "d", Isolated = '\ufea1', Final = 'n', Initial = '\ufea1', Medial = 'n', GlyphName = "arabic letter hah isolated form"}},
        {'\ufea2', new ArabicLetter { Base='\ufea2', JoiningType = "r", Isolated = 'n', Final = '\ufea2', Initial = 'n', Medial = 'n', GlyphName = "arabic letter hah final form"}},
        {'\ufea3', new ArabicLetter { Base='\ufea3', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufea3', Medial = 'n', GlyphName = "arabic letter hah initial form"}},
        {'\ufea4', new ArabicLetter { Base='\ufea4', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufea4', GlyphName = "arabic letter hah medial form"}},
        {'\ufea5', new ArabicLetter { Base='\ufea5', JoiningType = "d", Isolated = '\ufea5', Final = 'n', Initial = '\ufea5', Medial = 'n', GlyphName = "arabic letter khah isolated form"}},
        {'\ufea6', new ArabicLetter { Base='\ufea6', JoiningType = "r", Isolated = 'n', Final = '\ufea6', Initial = 'n', Medial = 'n', GlyphName = "arabic letter khah final form"}},
        {'\ufea7', new ArabicLetter { Base='\ufea7', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufea7', Medial = 'n', GlyphName = "arabic letter khah initial form"}},
        {'\ufea8', new ArabicLetter { Base='\ufea8', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufea8', GlyphName = "arabic letter khah medial form"}},
        {'\ufea9', new ArabicLetter { Base='\ufea9', JoiningType = "r", Isolated = '\ufea9', Final = 'n', Initial = '\ufea9', Medial = 'n', GlyphName = "arabic letter dal isolated form"}},
        {'\ufeaa', new ArabicLetter { Base='\ufeaa', JoiningType = "r", Isolated = 'n', Final = '\ufeaa', Initial = 'n', Medial = 'n', GlyphName = "arabic letter dal final form"}},
        {'\ufeab', new ArabicLetter { Base='\ufeab', JoiningType = "r", Isolated = '\ufeab', Final = 'n', Initial = '\ufeab', Medial = 'n', GlyphName = "arabic letter thal isolated form"}},
        {'\ufeac', new ArabicLetter { Base='\ufeac', JoiningType = "r", Isolated = 'n', Final = '\ufeac', Initial = 'n', Medial = 'n', GlyphName = "arabic letter thal final form"}},
        {'\ufead', new ArabicLetter { Base='\ufead', JoiningType = "r", Isolated = '\ufead', Final = 'n', Initial = '\ufead', Medial = 'n', GlyphName = "arabic letter reh isolated form"}},
        {'\ufeae', new ArabicLetter { Base='\ufeae', JoiningType = "r", Isolated = 'n', Final = '\ufeae', Initial = 'n', Medial = 'n', GlyphName = "arabic letter reh final form"}},
        {'\ufeaf', new ArabicLetter { Base='\ufeaf', JoiningType = "r", Isolated = '\ufeaf', Final = '\ufeb0', Initial = '\ufeaf', Medial = '\ufeb0', GlyphName = "arabic letter zain isolated form"}},
        {'\ufeb0', new ArabicLetter { Base='\ufeb0', JoiningType = "r", Isolated = 'n', Final = '\ufeb0', Initial = 'n', Medial = 'n', GlyphName = "arabic letter zain final form"}},
        {'\ufeb1', new ArabicLetter { Base='\ufeb1', JoiningType = "d", Isolated = '\ufeb1', Final = 'n', Initial = '\ufeb1', Medial = 'n', GlyphName = "arabic letter seen isolated form"}},
        {'\ufeb2', new ArabicLetter { Base='\ufeb2', JoiningType = "r", Isolated = 'n', Final = '\uFEB2', Initial = 'n', Medial = 'n', GlyphName = "arabic letter seen final form"}},
        {'\ufeb3', new ArabicLetter { Base='\ufeb3', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\uFEB3', Medial = 'n', GlyphName = "arabic letter seen initial form"}},
        {'\ufeb4', new ArabicLetter { Base='\ufeb4', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\uFEB4', GlyphName = "arabic letter seen medial form"}},
        {'\ufeb5', new ArabicLetter { Base='\ufeb5', JoiningType = "d", Isolated = '\uFEB5', Final = 'n', Initial = '\uFEB5', Medial = 'n', GlyphName = "arabic letter sheen isolated form"}},
        {'\ufeb6', new ArabicLetter { Base='\ufeb6', JoiningType = "r", Isolated = 'n', Final = '\uFEB6', Initial = 'n', Medial = 'n', GlyphName = "arabic letter sheen final form"}},
        {'\ufeb7', new ArabicLetter { Base='\ufeb7', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\uFEB7', Medial = 'n', GlyphName = "arabic letter sheen initial form"}},
        {'\ufeb8', new ArabicLetter { Base='\ufeb8', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\uFEB8', GlyphName = "arabic letter sheen medial form"}},
        {'\ufeb9', new ArabicLetter { Base='\ufeb9', JoiningType = "d", Isolated = '\uFEB9', Final = 'n', Initial = '\uFEB9', Medial = 'n', GlyphName = "arabic letter sad isolated form"}},
        {'\ufeba', new ArabicLetter { Base='\ufeba', JoiningType = "r", Isolated = 'n', Final = '\ufeba', Initial = 'n', Medial = 'n', GlyphName = "arabic letter sad final form"}},
        {'\ufebb', new ArabicLetter { Base='\ufebb', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufebb', Medial = 'n', GlyphName = "arabic letter sad initial form"}},
        {'\ufebc', new ArabicLetter { Base='\ufebc', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufebc', GlyphName = "arabic letter sad medial form"}},
        {'\ufebd', new ArabicLetter { Base='\ufebd', JoiningType = "d", Isolated = '\ufebd', Final = 'n', Initial = '\ufebd', Medial = 'n', GlyphName = "arabic letter dad isolated form"}},
        {'\ufebe', new ArabicLetter { Base='\ufebe', JoiningType = "r", Isolated = 'n', Final = '\ufebe', Initial = 'n', Medial = 'n', GlyphName = "arabic letter dad final form"}},
        {'\ufebf', new ArabicLetter { Base='\ufebf', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufebf', Medial = 'n', GlyphName = "arabic letter dad initial form"}},
        {'\ufec0', new ArabicLetter { Base='\ufec0', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufec0', GlyphName = "arabic letter dad medial form"}},
        {'\ufec1', new ArabicLetter { Base='\ufec1', JoiningType = "d", Isolated = '\ufec1', Final = 'n', Initial = '\ufec1', Medial = 'n', GlyphName = "arabic letter tah isolated form"}},
        {'\ufec2', new ArabicLetter { Base='\ufec2', JoiningType = "r", Isolated = 'n', Final = '\ufec2', Initial = 'n', Medial = 'n', GlyphName = "arabic letter tah final form"}},
        {'\ufec3', new ArabicLetter { Base='\ufec3', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufec3', Medial = 'n', GlyphName = "arabic letter tah initial form"}},
        {'\ufec4', new ArabicLetter { Base='\ufec4', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufec4', GlyphName = "arabic letter tah medial form"}},
        {'\ufec5', new ArabicLetter { Base='\ufec5', JoiningType = "d", Isolated = '\ufec5', Final = 'n', Initial = '\ufec5', Medial = 'n', GlyphName = "arabic letter zah isolated form"}},
        {'\ufec6', new ArabicLetter { Base='\ufec6', JoiningType = "r", Isolated = 'n', Final = '\ufec6', Initial = 'n', Medial = 'n', GlyphName = "arabic letter zah final form"}},
        {'\ufec7', new ArabicLetter { Base='\ufec7', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufec7', Medial = 'n', GlyphName = "arabic letter zah initial form"}},
        {'\ufec8', new ArabicLetter { Base='\ufec8', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufec8', GlyphName = "arabic letter zah medial form"}},
        {'\ufec9', new ArabicLetter { Base='\ufec9', JoiningType = "d", Isolated = '\ufec9', Final = 'n', Initial = '\ufec9', Medial = 'n', GlyphName = "arabic letter ain isolated form"}},
        {'\ufeca', new ArabicLetter { Base='\ufeca', JoiningType = "r", Isolated = 'n', Final = '\ufeca', Initial = 'n', Medial = 'n', GlyphName = "arabic letter ain final form"}},
        {'\ufecb', new ArabicLetter { Base='\ufecb', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufecb', Medial = 'n', GlyphName = "arabic letter ain initial form"}},
        {'\ufecc', new ArabicLetter { Base='\ufecc', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufecc', GlyphName = "arabic letter ain medial form"}},
        {'\ufecd', new ArabicLetter { Base='\ufecd', JoiningType = "d", Isolated = '\ufecd', Final = 'n', Initial = '\ufecd', Medial = 'n', GlyphName = "arabic letter ghain isolated form"}},
        {'\ufece', new ArabicLetter { Base='\ufece', JoiningType = "r", Isolated = 'n', Final = '\ufece', Initial = 'n', Medial = 'n', GlyphName = "arabic letter ghain final form"}},
        {'\ufecf', new ArabicLetter { Base='\ufecf', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufecf', Medial = 'n', GlyphName = "arabic letter ghain initial form"}},
        {'\ufed0', new ArabicLetter { Base='\ufed0', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufed0', GlyphName = "arabic letter ghain medial form"}},
        {'\ufed1', new ArabicLetter { Base='\ufed1', JoiningType = "d", Isolated = '\ufed1', Final = 'n', Initial = '\ufed1', Medial = 'n', GlyphName = "arabic letter feh isolated form"}},
        {'\ufed2', new ArabicLetter { Base='\ufed2', JoiningType = "r", Isolated = 'n', Final = '\ufed2', Initial = 'n', Medial = 'n', GlyphName = "arabic letter feh final form"}},
        {'\ufed3', new ArabicLetter { Base='\ufed3', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufed3', Medial = 'n', GlyphName = "arabic letter feh initial form"}},
        {'\ufed4', new ArabicLetter { Base='\ufed4', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufed4', GlyphName = "arabic letter feh medial form"}},
        {'\ufed5', new ArabicLetter { Base='\ufed5', JoiningType = "d", Isolated = '\ufed5', Final = 'n', Initial = '\ufed5', Medial = 'n', GlyphName = "arabic letter qaf isolated form"}},
        {'\ufed6', new ArabicLetter { Base='\ufed6', JoiningType = "r", Isolated = 'n', Final = '\ufed6', Initial = 'n', Medial = 'n', GlyphName = "arabic letter qaf final form"}},
        {'\ufed7', new ArabicLetter { Base='\ufed7', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufed7', Medial = 'n', GlyphName = "arabic letter qaf initial form"}},
        {'\ufed8', new ArabicLetter { Base='\ufed8', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufed8', GlyphName = "arabic letter qaf medial form"}},
        {'\ufed9', new ArabicLetter { Base='\ufed9', JoiningType = "d", Isolated = '\ufed9', Final = 'n', Initial = '\ufed9', Medial = 'n', GlyphName = "arabic letter kaf isolated form"}},
        {'\ufeda', new ArabicLetter { Base='\ufeda', JoiningType = "r", Isolated = 'n', Final = '\ufeda', Initial = 'n', Medial = 'n', GlyphName = "arabic letter kaf final form"}},
        {'\ufedb', new ArabicLetter { Base='\ufedb', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufedb', Medial = 'n', GlyphName = "arabic letter kaf initial form"}},
        {'\ufedc', new ArabicLetter { Base='\ufedc', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufedc', GlyphName = "arabic letter kaf medial form"}},
        {'\ufedd', new ArabicLetter { Base='\ufedd', JoiningType = "d", Isolated = '\ufedd', Final = 'n', Initial = '\ufedd', Medial = 'n', GlyphName = "arabic letter lam isolated form"}},
        {'\ufede', new ArabicLetter { Base='\ufede', JoiningType = "r", Isolated = 'n', Final = '\ufede', Initial = 'n', Medial = 'n', GlyphName = "arabic letter lam final form"}},
        {'\ufedf', new ArabicLetter { Base='\ufedf', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufedf', Medial = 'n', GlyphName = "arabic letter lam initial form"}},
        {'\ufee0', new ArabicLetter { Base='\ufee0', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufee0', GlyphName = "arabic letter lam medial form"}},
        {'\ufee1', new ArabicLetter { Base='\ufee1', JoiningType = "d", Isolated = '\ufee1', Final = 'n', Initial = '\ufee1', Medial = 'n', GlyphName = "arabic letter meem isolated form"}},
        {'\ufee2', new ArabicLetter { Base='\ufee2', JoiningType = "r", Isolated = 'n', Final = '\ufee2', Initial = 'n', Medial = 'n', GlyphName = "arabic letter meem final form"}},
        {'\ufee3', new ArabicLetter { Base='\ufee3', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufee3', Medial = 'n', GlyphName = "arabic letter meem initial form"}},
        {'\ufee4', new ArabicLetter { Base='\ufee4', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufee4', GlyphName = "arabic letter meem medial form"}},
        {'\ufee5', new ArabicLetter { Base='\ufee5', JoiningType = "d", Isolated = '\ufee5', Final = 'n', Initial = '\ufee5', Medial = 'n', GlyphName = "arabic letter noon isolated form"}},
        {'\ufee6', new ArabicLetter { Base='\ufee6', JoiningType = "r", Isolated = 'n', Final = '\ufee6', Initial = 'n', Medial = 'n', GlyphName = "arabic letter noon final form"}},
        {'\ufee7', new ArabicLetter { Base='\ufee7', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufee7', Medial = 'n', GlyphName = "arabic letter noon initial form"}},
        {'\ufee8', new ArabicLetter { Base='\ufee8', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufee8', GlyphName = "arabic letter noon medial form"}},
        {'\ufee9', new ArabicLetter { Base='\ufee9', JoiningType = "d", Isolated = '\ufee9', Final = 'n', Initial = '\ufee9', Medial = 'n', GlyphName = "arabic letter heh isolated form"}},
        {'\ufeea', new ArabicLetter { Base='\ufeea', JoiningType = "r", Isolated = 'n', Final = '\ufeea', Initial = 'n', Medial = 'n', GlyphName = "arabic letter heh final form"}},
        {'\ufeeb', new ArabicLetter { Base='\ufeeb', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufeeb', Medial = 'n', GlyphName = "arabic letter heh initial form"}},
        {'\ufeec', new ArabicLetter { Base='\ufeec', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufeec', GlyphName = "arabic letter heh medial form"}},
        {'\ufeed', new ArabicLetter { Base='\ufeed', JoiningType = "r", Isolated = '\ufeed', Final = '\ufeee', Initial = '\ufeed', Medial = 'n', GlyphName = "arabic letter waw isolated form"}},
        {'\ufeee', new ArabicLetter { Base='\ufeee', JoiningType = "r", Isolated = 'n', Final = '\ufeee', Initial = 'n', Medial = 'n', GlyphName = "arabic letter waw final form"}},
        {'\ufeef', new ArabicLetter { Base='\ufeef', JoiningType = "r", Isolated = '\ufeef', Final = 'n', Initial = '\ufeef', Medial = 'n', GlyphName = "arabic letter alef maksura isolated form"}},
        {'\ufef0', new ArabicLetter { Base='\ufef0', JoiningType = "r", Isolated = 'n', Final = '\ufef0', Initial = 'n', Medial = 'n', GlyphName = "arabic letter alef maksura final form"}},
        {'\ufef1', new ArabicLetter { Base='\ufef1', JoiningType = "r", Isolated = '\ufef1', Final = 'n', Initial = '\ufef1', Medial = 'n', GlyphName = "arabic letter yeh isolated form"}},
        {'\ufef2', new ArabicLetter { Base='\ufef2', JoiningType = "r", Isolated = 'n', Final = '\ufef2', Initial = 'n', Medial = 'n', GlyphName = "arabic letter yeh final form"}},
        {'\ufef3', new ArabicLetter { Base='\ufef3', JoiningType = "l", Isolated = 'n', Final = 'n', Initial = '\ufef3', Medial = 'n', GlyphName = "arabic letter yeh initial form"}},
        {'\ufef4', new ArabicLetter { Base='\ufef4', JoiningType = "d", Isolated = 'n', Final = 'n', Initial = 'n', Medial = '\ufef4', GlyphName = "arabic letter yeh medial form"}},
        {'\ufef5', new ArabicLetter { Base='\ufef5', JoiningType = "u", Isolated = '\ufef5', Final = 'n', Initial = '\ufef5', Medial = '\ufef5', GlyphName = "arabic ligature lam with alef with madda above isolated form"}},
        {'\ufef6', new ArabicLetter { Base='\ufef6', JoiningType = "r", Isolated = 'n', Final = '\ufef6', Initial = 'n', Medial = '\ufef6', GlyphName = "arabic ligature lam with alef with madda above final form"}},
        {'\ufef7', new ArabicLetter { Base='\ufef7', JoiningType = "u", Isolated = '\ufef7', Final = '\ufef7', Initial = '\ufef7', Medial = '\ufef7', GlyphName = "arabic ligature lam with alef with hamza above isolated form"}},
        {'\ufef8', new ArabicLetter { Base='\ufef8', JoiningType = "r", Isolated = 'n', Final = '\ufef8', Initial = 'n', Medial = '\ufef8', GlyphName = "arabic ligature lam with alef with hamza above final form"}},
        {'\ufef9', new ArabicLetter { Base='\ufef9', JoiningType = "u", Isolated = '\ufef9', Final = 'n', Initial = '\ufef9', Medial = 'n', GlyphName = "arabic ligature lam with alef with hamza below isolated form"}},
        {'\ufefa', new ArabicLetter { Base='\ufefa', JoiningType = "r", Isolated = 'n', Final = '\ufefa', Initial = 'n', Medial = '\ufefa', GlyphName = "arabic ligature lam with alef with hamza below final form"}},
        {'\ufefb', new ArabicLetter { Base='\ufefb', JoiningType = "u", Isolated = '\ufefb', Final = 'n', Initial = '\ufefb', Medial = '\ufefb', GlyphName = "arabic ligature lam with alef isolated form"}},
        {'\ufefc', new ArabicLetter { Base='\ufefc', JoiningType = "r", Isolated = 'n', Final = '\ufefc', Initial = 'n', Medial = '\ufefc', GlyphName = "arabic ligature lam with alef final form"}},
        {'\ufefd', new ArabicLetter { Base='\ufefd', JoiningType = "u", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "not supported"}},
        {'\ufefe', new ArabicLetter { Base='\ufefe', JoiningType = "u", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "not supported"}},
        {'\ufeff', new ArabicLetter { Base='\ufeff', JoiningType = "u", Isolated = 'n', Final = 'n', Initial = 'n', Medial = 'n', GlyphName = "not supported"}}

    };

    public static char GetGlyphForm (char c, string form)
    {
        if (LetterTable.TryGetValue(c, out ArabicLetter letter))
        {
            switch (form.ToLower())
            {
                case ("isolated"):
                    return letter.Isolated;                
                case ("initial"):
                    return letter.Initial;                    
                case ("medial"):
                    return letter.Medial;                    
                case ("final"):
                    return letter.Final;                    
                default:
                    return ' ';                    
            }
        }
        return letter.Base;
        
    }
    public static ArabicLetter GetArabicLetter(char c)
    {
        // Check if the character is in the Arabic letter table or not
        
        if (LetterTable.TryGetValue(c, out ArabicLetter letter))
        {
            return letter;
        }
        // If not found, return a default ArabicLetter with the character as base and all forms set to the character itself
        // something like this is useful for characters that are not Arabic letters
        return new ArabicLetter { Base = c, JoiningType = "u", Isolated = c, Final = c, Initial = c, Medial = c, GlyphName = "not supported" };
    }

    public static int GetArabicLetterUniCode(char c)
    {
        // Check if the character is in the Arabic letter table or not
        // and return its base Unicode value
        if (LetterTable.TryGetValue(c, out ArabicLetter letter))
        {
            return letter.Base;
        }
        return c;
    }

    public static string? GetGlyphName(char c)
    {
        if (LetterTable.TryGetValue(c, out ArabicLetter letter))
        {
            return letter.GlyphName;
        }
        return "not supported";
    }

    public static List<string> GetLetterForms(char c)
    {
        if (LetterTable.TryGetValue(c, out ArabicLetter letter))
        {
            return new List<string> { letter.Isolated.ToString(), letter.Final.ToString(), letter.Initial.ToString(), letter.Medial.ToString() };
        }
        return new List<string> { c.ToString(), c.ToString(), c.ToString(), c.ToString() };
    }
}