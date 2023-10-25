using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

[Serializable]
class Goal
{
    public string Title { get; set; }
    public string Description { get; set; }
    public int Points { get; set; }
    public bool IsComplete { get; set; }

    public Goal(string title, string description, int points)
    {
        Title = title;
        Description = description;
        Points = points;
        IsComplete = false;
    }
}

class Program
{
    static List<Goal> goals = new List<Goal>();
    static int totalScore = 0;

    static void Main(string[] args)
    {
        bool running = true;
        while (running)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Create a New Goal");
            Console.WriteLine("2. List Goals");
            Console.WriteLine("3. Save Goals");
            Console.WriteLine("4. Load Goals");
            Console.WriteLine("5. Record Event");
            Console.WriteLine("6. Quit");

            Console.WriteLine("Select a choice from the menu:");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateNewGoal();
                    break;
                case 2:
                    ListGoals();
                    break;
                case 3:
                    SaveGoals();
                    break;
                case 4:
                    LoadGoals();
                    break;
                case 5:
                    RecordEvent();
                    break;
                case 6:
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void ListGoals()
    {
        Console.WriteLine("Your Goals:");
        for (int i = 0; i < goals.Count; i++)
        {
            Console.WriteLine($"{i + 1}. {goals[i].Title} - {goals[i].Description} ({goals[i].Points} points) - Status: {goals[i].IsComplete}");
        }
    }

    static void CreateNewGoal()
    {
        Console.WriteLine("The types of Goals are:");
        Console.WriteLine("1. Simple Goal");
        Console.WriteLine("2. Eternal Goal");
        Console.WriteLine("3. Checklist Goal");

        Console.WriteLine("Which type of goal would you like to create?");
        int type = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("What is the name of your goal?");
        string title = Console.ReadLine();

        Console.WriteLine("What is a short description of it?");
        string description = Console.ReadLine();

        Console.WriteLine("What is the amount of points associated with this goal?");
        int points = Convert.ToInt32(Console.ReadLine());

        Goal newGoal = new Goal(title, description, points);
        goals.Add(newGoal);

        Console.WriteLine($"New goal '{newGoal.Title}' created.");
    }

    static void RecordEvent()
    {
        ListGoals();
        Console.WriteLine("Enter the number of the goal you completed:");
        int goalNumber = Convert.ToInt32(Console.ReadLine()) - 1;

        if (goalNumber >= 0 && goalNumber < goals.Count)
        {
            Goal selectedGoal = goals[goalNumber];
            selectedGoal.IsComplete = true;
            totalScore += selectedGoal.Points;
            Console.WriteLine($"You gained {selectedGoal.Points} points for completing '{selectedGoal.Title}'!");
        }
        else
        {
            Console.WriteLine("Invalid goal number.");
        }
    }

    static void SaveGoals()
    {
        XmlSerializer serializer = new XmlSerializer(typeof(List<Goal>));
        using (FileStream stream = new FileStream("goals.xml", FileMode.Create))
        {
            serializer.Serialize(stream, goals);
        }
        Console.WriteLine("Goals saved to 'goals.xml'.");
    }

    static void LoadGoals()
    {
        if (File.Exists("goals.xml"))
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<Goal>));
            using (FileStream stream = new FileStream("goals.xml", FileMode.Open))
            {
                goals = (List<Goal>)serializer.Deserialize(stream);
            }
            Console.WriteLine("Goals loaded from 'goals.xml'.");
        }
        else
        {
            Console.WriteLine("No saved goals found.");
        }
    }
}
