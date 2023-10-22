/*using System;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello Develop03 World!");
    }
}
*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        var scripture = new Scripture("John 3:16", "For God so loved the world that he gave his one and only Son, that whoever believes in him shall not perish but have eternal life.");

        while (!scripture.AllWordsHidden)
        {
            Console.Clear();
            Console.WriteLine(scripture);
            Console.WriteLine("\nPress Enter to continue or type 'quit' to exit.");

            string input = Console.ReadLine();
            if (input.ToLower() == "quit")
                break;

            scripture.HideRandomWords();
        }
    }
}

class Scripture
{
    private List<Verse> verses;
    private bool allWordsHidden;

    public bool AllWordsHidden
    {
        get { return allWordsHidden; }
    }

    public Scripture(string reference, string text)
    {
        verses = new List<Verse>();
        string[] verseParts = reference.Split(' ');

        foreach (string part in verseParts)
        {
            Verse verse = new Verse(part);
            verses.Add(verse);
        }

        string[] words = text.Split(' ');
        verses[0].Words.AddRange(words.Select(word => new Word(word))); // Initialize with Word objects
        allWordsHidden = false;
    }

    public void HideRandomWords()
    {
        Random random = new Random();
        foreach (var verse in verses)
        {
            if (verse.Words.Count > 1)
            {
                int wordIndex = random.Next(verse.Words.Count);
                verse.Words[wordIndex].Hide(); // Hide the word
            }
            else if (!verse.Words[0].IsHidden)
            {
                verse.Words[0].Hide(); // Hide the word
            }
        }

        CheckAllWordsHidden();
    }

    private void CheckAllWordsHidden()
    {
        allWordsHidden = verses.All(verse => verse.Words.All(word => word.IsHidden));
    }

    public override string ToString()
    {
        string scriptureText = string.Join(" ", verses);
        return scriptureText;
    }
}

class Verse
{
    public List<Word> Words { get; private set; }

    public Verse(string verseReference)
    {
        Words = new List<Word>();
        Words.Add(new Word(verseReference));
    }

    public override string ToString()
    {
        return string.Join(" ", Words);
    }
}

class Word
{
    public string Text { get; private set; }
    public bool IsHidden { get; private set; }

    public Word(string text)
    {
        Text = text;
        IsHidden = false;
    }

    public void Hide()
    {
        IsHidden = true;
    }

    public override string ToString()
    {
        if (IsHidden)
            return "[Hidden]";
        return Text;
    }
}
