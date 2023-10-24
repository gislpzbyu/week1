using System;

class Address
{
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string ZipCode { get; set; }

    public Address(string street, string city, string state, string zipCode)
    {
        Street = street;
        City = city;
        State = state;
        ZipCode = zipCode;
    }

    public string GetFormattedAddress()
    {
        return $"{Street}, {City}, {State} {ZipCode}";
    }
}

class Event
{
    private string Title { get; set; }
    private string Description { get; set; }
    private DateTime Date { get; set; }
    private string Time { get; set; }
    private Address Location { get; set; }

    public Event(string title, string description, DateTime date, string time, Address location)
    {
        Title = title;
        Description = description;
        Date = date;
        Time = time;
        Location = location;
    }

    public string GetStandardDetails()
    {
        return $"Title: {Title}\nDescription: {Description}\nDate: {Date.ToShortDateString()}\nTime: {Time}\nAddress: {Location.GetFormattedAddress()}";
    }

    public virtual string GetFullDetails()
    {
        return GetStandardDetails();
    }

    public virtual string GetShortDescription()
    {
        return $"Event Type: Generic\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }
}

class Lecture : Event
{
    private string Speaker { get; set; }
    private int Capacity { get; set; }

    public Lecture(string title, string description, DateTime date, string time, Address location, string speaker, int capacity)
        : base(title, description, date, time, location)
    {
        Speaker = speaker;
        Capacity = capacity;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nEvent Type: Lecture\nSpeaker: {Speaker}\nCapacity: {Capacity}";
    }

    public override string GetShortDescription()
    {
        return $"Event Type: Lecture\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }
}

class Reception : Event
{
    private string RSVP { get; set; }

    public Reception(string title, string description, DateTime date, string time, Address location, string rsvp)
        : base(title, description, date, time, location)
    {
        RSVP = rsvp;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nEvent Type: Reception\nRSVP Email: {RSVP}";
    }

    public override string GetShortDescription()
    {
        return $"Event Type: Reception\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }
}

class OutdoorGathering : Event
{
    private string Weather { get; set; }

    public OutdoorGathering(string title, string description, DateTime date, string time, Address location, string weather)
        : base(title, description, date, time, location)
    {
        Weather = weather;
    }

    public override string GetFullDetails()
    {
        return $"{base.GetFullDetails()}\nEvent Type: Outdoor Gathering\nWeather: {Weather}";
    }

    public override string GetShortDescription()
    {
        return $"Event Type: Outdoor Gathering\nTitle: {Title}\nDate: {Date.ToShortDateString()}";
    }
}

class Program
{
    static void Main(string[] args)
    {
        Address location1 = new Address("123 Main St", "Anytown", "CA", "12345");
        Event genericEvent = new Event("Generic Event", "A generic event description", DateTime.Now, "12:00 PM", location1);

        Address location2 = new Address("456 Elm St", "Other City", "NY", "54321");
        Lecture lectureEvent = new Lecture("Tech Talk", "An informative tech talk", DateTime.Now, "2:00 PM", location2, "John Smith", 50);

        Address location3 = new Address("789 Oak St", "Another City", "TX", "67890");
        Reception receptionEvent = new Reception("Networking Mixer", "A networking mixer for professionals", DateTime.Now, "5:00 PM", location3, "rsvp@example.com");

        Address location4 = new Address("101 Pine St", "Small Town", "AZ", "24680");
        OutdoorGathering outdoorEvent = new OutdoorGathering("Picnic in the Park", "A fun outdoor picnic", DateTime.Now, "3:30 PM", location4, "Sunny and clear");

        Console.WriteLine("Generic Event - Standard Details:");
        Console.WriteLine(genericEvent.GetStandardDetails() + "\n");

        Console.WriteLine("Tech Talk - Full Details:");
        Console.WriteLine(lectureEvent.GetFullDetails() + "\n");

        Console.WriteLine("Networking Mixer - Short Description:");
        Console.WriteLine(receptionEvent.GetShortDescription() + "\n");

        Console.WriteLine("Picnic in the Park - Full Details:");
        Console.WriteLine(outdoorEvent.GetFullDetails());
    }
}
