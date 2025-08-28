/*
 * Regex regex = new Regex(@"\s+");
 * string[] words = regex.Split("Hello   World");
 */
using System;
using unimain.stuff;

public static class LigatureUsageExample
{
    public static void DemonstrateLigatureUsage()
    {
        Console.WriteLine("=== Arabic Ligature Processing Examples ===\n");
        Console.OutputEncoding = System.Text.Encoding.UTF8; // عرض الحروف العربية اليونيكود
        // Example 1: Basic ligature processing
        string arabicText = "\u200f"+"لا ملأ ملاز"; // Lam + Alef test
        Console.WriteLine($"New class Direction: {TextDirectionUni.ApplyDirection(arabicText)}");
        //string arabicText = "محمد بن يحيى"; // Lam + Alef
        Console.WriteLine($"Original: {arabicText.ToUnicodeString()}");        
        Stack<string> qString = new Stack<string>();
        string processed = ArabicShaperWords.ShapeWord(arabicText, out qString);
        Console.WriteLine($"Processed: {processed}");
        Console.WriteLine($"Processed: {processed.ToUnicodeString()}");
        Console.WriteLine();
    }
}
