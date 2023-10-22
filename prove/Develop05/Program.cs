/*using System;

class Program
{
    static void Main(string[] args)
    {
        
        Console.WriteLine("Hello Develop05 World!");
    }
}*/

using System;
using System.Collections.Generic;

// Base class for all types of goals
public abstract class Goal
{
    public string Name { get; set; }
    public int Points { get; protected set; }
    public bool IsComplete { get; protected set; }

    public Goal(string name)
    {
        Name = name;
        Points = 0;
        IsComplete = false;
    }

    public virtual void RecordEvent()
    {
        Points += CalculatePoints();
        CheckCompletion();
    }

    protected abstract int CalculatePoints();
    protected abstract void CheckCompletion();
}

// Simple goal class
public class SimpleGoal : Goal
{
    public int Value { get; private set; }

    public SimpleGoal(string name, int value)
        : base(name)
    {
        Value = value;
    }

    protected override int CalculatePoints()
    {
        return Points + Value;
    }

    protected override void CheckCompletion()
    {
        if (Points >= Value)
        {
            IsComplete = true;
        }
    }
}

// Eternal goal class
public class EternalGoal : Goal
{
    public EternalGoal(string name)
        : base(name)
    {
    }

    protected override int CalculatePoints()
    {
        return 100; // Fixed points for an eternal goal
    }

    protected override void CheckCompletion()
    {
        // Eternal goals are never completed
    }
}

// Checklist goal class
public class ChecklistGoal : Goal
{
    public int TargetCount { get; private set; }
    public int CompletedCount { get; private set; }

    public ChecklistGoal(string name, int targetCount)
        : base(name)
    {
        TargetCount = targetCount;
        CompletedCount = 0;
    }

    public override void RecordEvent()
    {
        base.RecordEvent();
        CompletedCount++;
    }

    protected override int CalculatePoints()
    {
        return 50; // Fixed points for a checklist goal
    }

    protected override void CheckCompletion()
    {
        if (CompletedCount >= TargetCount)
        {
            IsComplete = true;
            Points += 500; // Bonus points for completing a checklist goal
        }
    }
}

public class Program
{
    static void Main()
    {
        // Create and manage goals
        List<Goal> goals = new List<Goal>();
        goals.Add(new SimpleGoal("Run a Marathon", 1000));
        goals.Add(new EternalGoal("Read Scriptures"));
        goals.Add(new ChecklistGoal("Attend Temple", 10));

        // Simulate goal progress
        foreach (Goal goal in goals)
        {
            goal.RecordEvent();
        }

        // Display goals and scores
        Console.WriteLine("Goals:");
        foreach (Goal goal in goals)
        {
            string completionStatus = goal.IsComplete ? "[X]" : "[ ]";
            string checklistProgress = (goal is ChecklistGoal checklist) ? $"Completed {checklist.CompletedCount}/{checklist.TargetCount} times" : "";
            Console.WriteLine($"{completionStatus} {goal.Name} - Points: {goal.Points} {checklistProgress}");
        }
    }
}
