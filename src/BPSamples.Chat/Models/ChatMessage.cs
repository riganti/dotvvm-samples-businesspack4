using System;

namespace BPSamples.Chat.Models;

public class ChatMessage
{
    public string Author { get; set; }

    public string Message { get; set; }

    public DateTime CreatedDate { get; set; }
}