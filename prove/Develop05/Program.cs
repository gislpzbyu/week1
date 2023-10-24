using System;
using System.Collections.Generic;

class Goal
{
    protected string description;
    protected int currentProgress;
    protected int pointsAwarded;

    public Goal(string description, int pointsAwarded)
    {
        this.description = description;
        this.pointsAwarded = pointsAwarded;
        currentProgress = 0;
    }

    public virtual void MarkComplete()
    {
        currentProgress++;
    }

    public virtual bool IsComplete()
    {
        return false; // Base goals are never complete
    }

    public virtual string GetProgress()
    {
        return $"{currentProgress}/{pointsAwarded}";
    }

    public int GetPointsEarned()
    {
        return currentProgress * pointsAwarded;
    }

    public override string ToString()
    {
        return $"{description} [{(IsComplete() ? "X" : " ")}] ({GetProgress()})";
    }
}

class SimpleGoal : Goal
{
    public SimpleGoal(string description, int pointsAwarded) : base(description, pointsAwarded)
    {
    }

    public override bool IsComplete()
    {
        return currentProgress > 0;
    }
}

class EternalGoal : Goal
{
    public EternalGoal(string description, int pointsAwarded) : base(description, pointsAwarded)
    {
    }
}

class ChecklistGoal : Goal
{
    public int requiredProgress;

    public ChecklistGoal(string description, int pointsAwarded, int requiredProgress) : base(description, pointsAwarded)
    {
        this.requiredProgress = requiredProgress;
    }

    public override void MarkComplete()
    {
        currentProgress++;
        if (IsComplete())
            currentProgress = 0;
    }

    public override bool IsComplete()
    {
        return currentProgress >= requiredProgress;
    }
}

class QuestManager
{
    public List<Goal> goals;
    public int score;

    public QuestManager()
    {
        goals = new List<Goal>();
        score = 0;
    }

    public void AddGoal(Goal goal)
    {
        goals.Add(goal);
    }

    public void RecordEvent(Goal goal)
    {
        goal.MarkComplete();
        score += goal.GetPointsEarned();
    }

    public int GetScore()
    {
        return score;
    }

    public List<Goal> GetGoals()
    {
        return goals;
    }

    public void SaveGoalsToFile(string filename)
    {
        // Implement code to save goals to a file
    }

    public void LoadGoalsFromFile(string filename)
    {
        // Implement code to load goals from a file
    }
}

class Program
{
    static void Main(string[] args)
    {
        QuestManager questManager = new QuestManager();

        // Implement the user interface for creating, managing, and recording events for goals

        // Example:
        SimpleGoal marathonGoal = new SimpleGoal("Run a Marathon", 1000);
        questManager.AddGoal(marathonGoal);

        EternalGoal scripturesGoal = new EternalGoal("Read Scriptures", 100);
        questManager.AddGoal(scripturesGoal);

        ChecklistGoal templeGoal = new ChecklistGoal("Attend the Temple", 50, 10);
        questManager.AddGoal(templeGoal);

        // Display the user's goals and current score
        Console.WriteLine("Your Goals:");
        foreach (var goal in questManager.GetGoals())
        {
            Console.WriteLine(goal);
        }

        Console.WriteLine($"Current Score: {questManager.GetScore()}");

        // Save and load goals as needed
        questManager.SaveGoalsToFile("goals.txt");
        questManager.LoadGoalsFromFile("goals.txt");

        // Exceed requirements by adding creativity, e.g., adding levels, bonuses, or negative goals
    }
}
