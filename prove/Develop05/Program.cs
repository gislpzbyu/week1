using System;
using System.Collections.Generic;
using System.IO;

// Base class for goals
class Goal
{
    public string Name { get; set; }
    public int Points { get; set; }
    public bool IsCompleted { get; set; }

    public virtual void CompleteGoal()
    {
        IsCompleted = true;
    }

    public override string ToString()
    {
        return $"Goal: {Name} - Points: {Points} - Completed: {(IsCompleted ? "Yes" : "No")}";
    }
}

// Derived class for simple goals
class SimpleGoal : Goal
{
    public SimpleGoal(string name, int points)
    {
        Name = name;
        Points = points;
    }
}

// Derived class for eternal goals
class EternalGoal : Goal
{
    public EternalGoal(string name, int points)
    {
        Name = name;
        Points = points;
    }
}

// Derived class for checklist goals
class ChecklistGoal : Goal
{
    public int TargetCount { get; set; }
    private int completedCount = 0;

    public ChecklistGoal(string name, int points, int targetCount)
    {
        Name = name;
        Points = points;
        TargetCount = targetCount;
    }

    public override void CompleteGoal()
    {
        completedCount++;
        if (completedCount == TargetCount)
        {
            IsCompleted = true;
            Points += 500; // Bonus points for completing the checklist
        }
    }

    public override string ToString()
    {
        return base.ToString() + $" - Completed {completedCount}/{TargetCount} times";
    }
}

// User class to manage goals and score
class User
{
    public List<Goal> Goals { get; set } = new List<Goal>();
    public int Score { get; set } = 0;

    public void RecordEvent(Goal goal)
    {
        if (!goal.IsCompleted)
        {
            goal.CompleteGoal();
            Score += goal.Points;
        }
    }

    public void SaveGoalsToFile(string fileName)
    {
        using (StreamWriter writer = new StreamWriter(fileName))
        {
            foreach (Goal goal in Goals)
            {
                writer.WriteLine($"{goal.GetType().Name}:{goal.Name}:{goal.Points}:{goal.IsCompleted}");
            }
        }
    }

    public void LoadGoalsFromFile(string fileName)
    {
        Goals.Clear();
        using (StreamReader reader = new StreamReader(fileName))
        {
            string line;
            while ((line = reader.ReadLine()) != null)
            {
                string[] parts = line.Split(':');
                if (parts.Length == 4)
                {
                    string goalType = parts[0];
                    string name = parts[1];
                    int points = int.Parse(parts[2]);
                    bool isCompleted = bool.Parse(parts[3]);

                    Goal goal;
                    if (goalType == nameof(SimpleGoal))
                        goal = new SimpleGoal(name, points);
                    else if (goalType == nameof(EternalGoal))
                        goal = new EternalGoal(name, points);
                    else if (goalType == nameof(ChecklistGoal))
                        goal = new ChecklistGoal(name, points, 5); // Default target count
                    else
                        continue;

                    goal.IsCompleted = isCompleted;
                    Goals.Add(goal);
                }
            }
        }
    }
}

class Program
{
    static void Main(string[] args)
    {
        User user = new User();

        // Load previously saved goals (if any)
        user.LoadGoalsFromFile("goals.txt");

        while (true)
        {
            Console.WriteLine("Eternal Quest - Goal Tracking Program");
            Console.WriteLine("1. Create Goal");
            Console.WriteLine("2. Record Event");
            Console.WriteLine("3. Show Goals");
            Console.WriteLine("4. Show Score");
            Console.WriteLine("5. Save Goals");
            Console.WriteLine("6. Exit");
            Console.Write("Enter your choice: ");
            int choice = int.Parse(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    CreateGoal(user);
                    break;
                case 2:
                    RecordEvent(user);
                    break;
                case 3:
                    ShowGoals(user);
                    break;
                case 4:
                    ShowScore(user);
                    break;
                case 5:
                    user.SaveGoalsToFile("goals.txt");
                    Console.WriteLine("Goals saved to 'goals.txt'");
                    break;
                case 6:
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please try again.");
                    break;
            }
        }
    }

    static void CreateGoal(User user)
    {
        Console.WriteLine("Create a Goal:");
        Console.Write("Enter the goal name: ");
        string name = Console.ReadLine();
        Console.Write("Enter the goal type (Simple/Eternal/Checklist): ");
        string goalType = Console.ReadLine();
        Console.Write("Enter the points for this goal: ");
        int points = int.Parse(Console.ReadLine());

        Goal goal;
        if (goalType.Equals("Simple", StringComparison.OrdinalIgnoreCase))
            goal = new SimpleGoal(name, points);
        else if (goalType.Equals("Eternal", StringComparison.OrdinalIgnoreCase))
            goal = new EternalGoal(name, points);
        else if (goalType.Equals("Checklist", StringComparison.OrdinalIgnoreCase))
        {
            Console.Write("Enter the target count for the checklist goal: ");
            int targetCount = int.Parse(Console.ReadLine());
            goal = new ChecklistGoal(name, points, targetCount);
        }
        else
        {
            Console.WriteLine("Invalid goal type. Goal not created.");
            return;
        }

        user.Goals.Add(goal);
        Console.WriteLine("Goal created successfully!");
    }

    static void RecordEvent(User user)
    {
        Console.WriteLine("Record an Event (Goal Completion):");
        Console.Write("Enter the goal name: ");
        string name = Console.ReadLine();

        Goal goal = user.Goals.Find(g => g.Name == name);
        if (goal != null)
        {
            user.RecordEvent(goal);
            Console.WriteLine($"Event recorded. You earned {goal.Points} points!");
        }
        else
        {
            Console.WriteLine("Goal not found. Please check the goal name.");
        }
    }

    static void ShowGoals(User user)
    {
        Console.WriteLine("List of Goals:");
        foreach (Goal goal in user.Goals)
        {
            Console.WriteLine(goal);
        }
    }

    static void ShowScore(User user)
    {
        Console.WriteLine($"Current Score: {user.Score} points");
    }
}
