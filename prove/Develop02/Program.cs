using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Entry
{
    public string Prompt { get; set; }
    public string Response { get; set; }
    public DateTime Date { get; set; }
}

class Journal
{
    public List<Entry> entries = new List<Entry>();

    public void AddEntry(string prompt, string response)
    {
        Entry entry = new Entry
        {
            Prompt = prompt,
            Response = response,
            Date = DateTime.Now
        };
        entries.Add(entry);
    }

    public void DisplayEntries()
    {
        foreach (var entry in entries)
        {
            Console.WriteLine($"Date: {entry.Date}");
            Console.WriteLine($"Prompt: {entry.Prompt}");
            Console.WriteLine($"Response: {entry.Response}");
            Console.WriteLine();
        }
    }

    //Load
    public void LoadJournal(string filename)
    {
        using (StreamWriter writer = new StreamWriter(filename))
        {
            foreach (var entry in entries)
            {
                writer.WriteLine($"Date: {entry.Date}");
                writer.WriteLine($"Prompt: {entry.Prompt}");
                writer.WriteLine($"Response: {entry.Response}");
                writer.WriteLine();
            }
        }
    }
    //Save
    public void SaveJournal(string filename)
    {
        entries.Clear();

        if (File.Exists(filename))
        {
            using (StreamReader reader = new StreamReader(filename))
            {
                Entry entry = null;

                while (reader.Peek() >= 0)
                {
                    var line = reader.ReadLine();

                    if (line.StartsWith("Date: "))
                    {
                        entry = new Entry();
                        entry.Date = DateTime.Parse(line.Substring(6));
                    }
                    else if (line.StartsWith("Prompt: "))
                    {
                        entry.Prompt = line.Substring(8);
                    }
                    else if (line.StartsWith("Response: "))
                    {
                        entry.Response = line.Substring(10);
                        entries.Add(entry);
                    }
                }
            }
        }
        else
        {
            Console.WriteLine("File not found.");
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        Journal journal = new Journal();

        List<string> prompts = new List<string>
        {
            "Who was the most interesting person I interacted with today?",
            "What was the best part of my day?",
            "How did I see the hand of the Lord in my life today?",
            "What was the strongest emotion I felt today?",
            "If I had one thing I could do over today, what would it be?"
        };

        while (true)
        {
            Console.WriteLine("\nJournal Menu:");
            Console.WriteLine("1. Write");
            Console.WriteLine("2. Display");
            Console.WriteLine("3. Load");
            Console.WriteLine("4. Save");
            Console.WriteLine("5. Quit");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    string randomPrompt = prompts[new Random().Next(prompts.Count)];
                    Console.WriteLine($"Prompt: {randomPrompt}");
                    Console.Write("Response: ");
                    string response = Console.ReadLine();
                    journal.AddEntry(randomPrompt, response);
                    break;
                case "2":
                    journal.DisplayEntries();
                    break;
                case "3":
                    Console.Write("Enter a filename to load the journal: ");
                    string loadFilename = Console.ReadLine();
                    journal.LoadJournal(loadFilename);
                    break;
                case "4":
                    Console.Write("Enter a filename to save the journal: ");
                    string saveFilename = Console.ReadLine();
                    journal.SaveJournal(saveFilename);
                    break;
                case "5":
                    return;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }
}