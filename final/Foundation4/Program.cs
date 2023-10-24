using System;
using System.Collections.Generic;

class ExerciseTracker
{
    static void Main(string[] args)
    {
        List<Activity> activities = new List<Activity>();

        DateTime date1 = new DateTime(2022, 11, 3);
        activities.Add(new RunningActivity(date1, 30, 3.0));

        DateTime date2 = new DateTime(2022, 11, 3);
        activities.Add(new CyclingActivity(date2, 45, 20.0));

        DateTime date3 = new DateTime(2022, 11, 3);
        activities.Add(new SwimmingActivity(date3, 60, 25));

        Console.WriteLine("Exercise Activities:");

        foreach (var activity in activities)
        {
            Console.WriteLine(activity.GetSummary());
        }
    }
}

class Activity
{
    private DateTime Date { get; }
    private int Minutes { get; }

    public Activity(DateTime date, int minutes)
    {
        Date = date;
        Minutes = minutes;
    }

    public virtual double GetDistance()
    {
        return 0;
    }

    public virtual double GetSpeed()
    {
        return 0;
    }

    public virtual double GetPace()
    {
        return 0;
    }

    public virtual string GetSummary()
    {
        return $"{Date:dd MMM yyyy} ({Minutes} min)";
    }
}

class RunningActivity : Activity
{
    private double Distance { get; }

    public RunningActivity(DateTime date, int minutes, double distance) : base(date, minutes)
    {
        Distance = distance;
    }

    public override double GetDistance()
    {
        return Distance;
    }

    public override double GetSpeed()
    {
        return Distance / (Minutes / 60.0);
    }

    public override double GetPace()
    {
        return Minutes / Distance;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {Distance:F1} miles, Speed: {GetSpeed():F1} mph, Pace: {GetPace():F1} min/mile";
    }
}

class CyclingActivity : Activity
{
    private double Speed { get; }

    public CyclingActivity(DateTime date, int minutes, double speed) : base(date, minutes)
    {
        Speed = speed;
    }

    public override double GetSpeed()
    {
        return Speed;
    }

    public override double GetPace()
    {
        return 60.0 / Speed;
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Speed: {Speed:F1} mph, Pace: {GetPace():F1} min/mile";
    }
}

class SwimmingActivity : Activity
{
    private int Laps { get; }

    public SwimmingActivity(DateTime date, int minutes, int laps) : base(date, minutes)
    {
        Laps = laps;
    }

    public override double GetDistance()
    {
        return Laps * 50 / 1000.0; 
    }

    public override double GetSpeed()
    {
        return GetDistance() / (Minutes / 60.0);
    }

    public override double GetPace()
    {
        return Minutes / GetDistance();
    }

    public override string GetSummary()
    {
        return $"{base.GetSummary()} - Distance: {GetDistance():F1} km, Speed: {GetSpeed():F1} kph, Pace: {GetPace():F1} min/km";
    }
}
