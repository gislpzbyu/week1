using System;
using System.Threading;

class Activity
{
    protected string name;
    protected int duration;
    protected string[] prompts;

    public Activity(string name, string[] prompts)
    {
        this.name = name;
        this.prompts = prompts;
    }

    public void Start()
    {
        Console.WriteLine($"Starting {name} Activity...");
        Console.Write("How long, in seconds, would you like for this session? ");
        duration = int.Parse(Console.ReadLine());

        Console.WriteLine("Get ready");
        Thread.Sleep(3000);

        PerformActivity();

        Console.WriteLine($"{name} Activity completed. You did a great job!");
        Console.WriteLine($"You completed the {name} Activity in {duration} seconds.");
        Console.WriteLine("Pausing for a moment...");
        Thread.Sleep(3000);
    } 

    protected virtual void PerformActivity()
    {
        
    }
}

class BreathingActivity : Activity
{
    public BreathingActivity() : base("Breathing", new string[] { "Breathe in...", "Breathe out..." }) { }

    protected override void PerformActivity()
    {
        int remainingTime = duration;
        int index = 0;

        while (remainingTime > 0)
        {
            Console.WriteLine(prompts[index]);
            Thread.Sleep(1000);
            remainingTime--;
            index = (index + 1) % prompts.Length;
        }
    }
}

class ReflectionActivity : Activity
{
    public ReflectionActivity() : base("Reflection", new string[] {
        "Think of a time when you stood up for someone else.",
        "Think of a time when you did something really difficult.",
        "Think of a time when you helped someone in need.",
        "Think of a time when you did something truly selfless." }) { }

    protected override void PerformActivity()
    {
        int remainingTime = duration;
        Random random = new Random();

        while (remainingTime > 0)
        {
            string prompt = prompts[random.Next(prompts.Length)];
            Console.WriteLine(prompt);
            Thread.Sleep(3000);

            string[] questions = new string[]
            {
                "Why was this experience meaningful to you?",
                "Have you ever done anything like this before?",
                "How did you get started?",
                "How did you feel when it was complete?",
                "What made this time different than other times when you were not as successful?",
                "What is your favorite thing about this experience?",
                "What could you learn from this experience that applies to other situations?",
                "What did you learn about yourself through this experience?",
                "How can you keep this experience in mind in the future?"
            };

            foreach (var question in questions)
            {
                Console.WriteLine(question);
                Thread.Sleep(3000);
            }

            remainingTime -= 20; 
        }
    }
}

class ListingActivity : Activity
{
    public ListingActivity() : base("Listing", new string[] {
        "Who are people that you appreciate?",
        "What are personal strengths of yours?",
        "Who are people that you have helped this week?",
        "When have you felt the Holy Ghost this month?",
        "Who are some of your personal heroes?" }) { }

    protected override void PerformActivity()
    {
        Console.WriteLine("Get ready");
        Thread.Sleep(3000);
        Console.WriteLine("Go!");
        Thread.Sleep(1000);

        int itemsCount = 0;
        DateTime startTime = DateTime.Now;

        while (itemsCount < duration)
        {
            Console.Write("Enter an item: ");
            Console.ReadLine();
            itemsCount++;
        }

        DateTime endTime = DateTime.Now;
        TimeSpan elapsed = endTime - startTime;

        Console.WriteLine($"You listed {itemsCount} items.");
        Console.WriteLine($"Time taken: {elapsed.TotalSeconds} seconds.");
    }
}

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("Menu Options:");
            Console.WriteLine("1. Start Breathing Activity");
            Console.WriteLine("2. Start Reflection Activity");
            Console.WriteLine("3. Start Listing Activity");
            Console.WriteLine("4. Quit");

            Console.Write("Select a choice  from the menu: ");
            int choice = int.Parse(Console.ReadLine());

            if (choice == 4)
            {
                break;
            }

            Activity activity;

            switch (choice)
            {
                case 1:
                    activity = new BreathingActivity();
                    break;
                case 2:
                    activity = new ReflectionActivity();
                    break;
                case 3:
                    activity = new ListingActivity();
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid activity.");
                    continue;
            }

            activity.Start();
        }
    }
}