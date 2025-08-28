using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// هذا الملف يُعرّف فئة `UniCodeTable`، المصممة لتحميل وتمثيل بيانات الحروف العربية من ملف.
// توفر الفئة طريقة منظمة لإدارة خصائص الحروف مثل سلوك الاتصال وأشكال العرض المختلفة (منفرد،أولي، وسط، نهائي).
// الغرض الرئيسي هو تحليل ملف بيانات الحروف (مثل ملف CSV) وإنشاء مجموعة من الكائنات،
// والتي يمكن استخدامها بعد ذلك لبناء جداول المحارف الرئيسية في التطبيق.
// المحارف هنا أعني بها ال Glyphs
namespace UniMain.Library
{
    internal class UniCodeTable
    {
        public string BaseUnicode { get; set; } // base unicode format is u+XXXX
        public string Code { get; set; } // code format is 0-XXXX
        public string? GlyphName { get; set; } // get Glyph name like Alef with Hamza above
        public string Position { get; set; } // position of the letter in the word (e.g., initial, medial, final, isolated)
        public char? JoinType { get; set; } // [d]ual, [r]ight, [l]eft, [u]n-shaped
        public string? IsolatedGlyphCode { get; set; } // Unicode for isolated form
        public string? InitialGlyphCode { get; set; } // Unicode for initial form
        public string? MedialGlyphCode { get; set; } // Unicode for medial form
        public string? FinalGlyphCode { get; set; } // Unicode for final form
        public int? CodePoint { get; set; } // Code point value for the character converted from BaseUnicode to Char code


        public UniCodeTable(string baseUnicode, string code, string position, char? joinType, string? isolatedGlyphCode, string? initialGlyphCode, string? medialGlyphCode, string? finalGlyphCode, int? codePoint, string? gName)
        {
            BaseUnicode = baseUnicode;
            Code = code;
            Position = position;
            JoinType = joinType;
            IsolatedGlyphCode = isolatedGlyphCode;
            InitialGlyphCode = initialGlyphCode;
            MedialGlyphCode = medialGlyphCode;
            FinalGlyphCode = finalGlyphCode;
            CodePoint = codePoint;
            GlyphName = gName;
        }

        public static List<UniCodeTable> FillTableFromFile(string filePath)
        {
            List<UniCodeTable> unicodeTables = new List<UniCodeTable>();
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"Error: File not found at {filePath}");
                return unicodeTables;
            }

            foreach (var line in File.ReadLines(filePath).Skip(1)) // Skip header line
            {
                var parts = line.Split(',');
                if (parts.Length < 14) continue; // Ensure enough parts exist

                string baseUnicode = parts[1].Trim();
                string code = parts[2].Trim();
                string gName = parts[4].Trim();
                string position = parts[5].Trim();

                string joinTypeStr = parts[6].Trim();
                char? joinType = string.IsNullOrEmpty(joinTypeStr) ? null : joinTypeStr[0];

                string isolatedGlyphCode = parts[7].Trim();
                string initialGlyphCode = parts[8].Trim();
                string medialGlyphCode = parts[9].Trim();
                string finalGlyphCode = parts[10].Trim();

                int? codePoint = int.TryParse(parts[13].Trim(), NumberStyles.HexNumber, CultureInfo.InvariantCulture, out int cp) ? cp : null;

                unicodeTables.Add(new UniCodeTable(baseUnicode, code, position, joinType, isolatedGlyphCode, initialGlyphCode, medialGlyphCode, finalGlyphCode, codePoint, gName));
            }
            return unicodeTables;
        }

        public override string ToString()
        {
            // Helper to format the hex strings into C# char literals.
            static string ToCharLiteral(string? hex)
            {
                if (string.IsNullOrWhiteSpace(hex) || hex.Equals("n", StringComparison.OrdinalIgnoreCase))
                {
                    return "'n'"; // Use 'n' for "Not Applicable", matching the existing ArabicShaper table.
                }
                // Ensure the hex string is clean before formatting.
                string cleanHex = hex.Replace("U+", "", StringComparison.OrdinalIgnoreCase).Trim();
                return $"'\\u{cleanHex}'";
            }

            string baseChar = ToCharLiteral(this.Code);
            string isolatedChar = ToCharLiteral(this.IsolatedGlyphCode);
            string finalChar = ToCharLiteral(this.FinalGlyphCode);
            string initialChar = ToCharLiteral(this.InitialGlyphCode);
            string medialChar = ToCharLiteral(this.MedialGlyphCode);
            string glyphName = this.GlyphName?.Replace("\"", "\\\"") ?? "not supported"; // Escape quotes in name

            return $"{{{baseChar}, new ArabicLetter {{ Base={baseChar}, JoiningType = \"{this.JoinType}\", Isolated = {isolatedChar}, Final = {finalChar}, Initial = {initialChar}, Medial = {medialChar}, GlyphName = \"{glyphName}\" }}}},";
        }
    }
}
