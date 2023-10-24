using System;
using System.Collections.Generic;

class Comment
{
    public string CommenterName { get; set; }
    public string CommentText { get; set; }

    public Comment(string commenterName, string commentText)
    {
        CommenterName = commenterName;
        CommentText = commentText;
    }

    public override string ToString()
    {
        return $"{CommenterName}: {CommentText}";
    }
}

class Video
{
    public string Title { get; set; }
    public string Author { get; set; }
    public int LengthInSeconds { get; set; }
    public List<Comment> Comments { get; set; }

    public Video(string title, string author, int lengthInSeconds)
    {
        Title = title;
        Author = author;
        LengthInSeconds = lengthInSeconds;
        Comments = new List<Comment>();
    }

    public void AddComment(Comment comment)
    {
        Comments.Add(comment);
    }

    public int GetNumberOfComments()
    {
        return Comments.Count;
    }

    public void DisplayVideoInfo()
    {
        Console.WriteLine($"Title: {Title}");
        Console.WriteLine($"Author: {Author}");
        Console.WriteLine($"Length (seconds): {LengthInSeconds}");
        Console.WriteLine($"Number of Comments: {GetNumberOfComments()}");
        Console.WriteLine("Comments:");
        foreach (var comment in Comments)
        {
            Console.WriteLine(comment);
        }
        Console.WriteLine();
    }
}

class Program
{
    static void Main(string[] args)
    {
        List<Video> videos = new List<Video>();

        // Create 3-4 videos and add comments
        Video video1 = new Video("Video 1", "Author 1", 300);
        video1.AddComment(new Comment("User1", "Great video!"));
        video1.AddComment(new Comment("User2", "I learned a lot from this."));
        videos.Add(video1);

        Video video2 = new Video("Video 2", "Author 2", 420);
        video2.AddComment(new Comment("User3", "Nice explanation."));
        videos.Add(video2);

        Video video3 = new Video("Video 3", "Author 3", 240);
        video3.AddComment(new Comment("User4", "Not bad."));
        video3.AddComment(new Comment("User5", "Could be better."));
        videos.Add(video3);

        Video video4 = new Video("Video 4", "Author 4", 600);
        videos.Add(video4);

        // Display video information
        foreach (var video in videos)
        {
            video.DisplayVideoInfo();
        }
    }
}
