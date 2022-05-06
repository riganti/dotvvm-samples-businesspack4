using System;
using System.Threading.Tasks;
using BPSamples.Chat.Hubs;
using BPSamples.Chat.Models;
using DotVVM.BusinessPack.Messaging;
using DotVVM.Framework.ViewModel;

namespace BPSamples.Chat.Services;

public class ChatService
{
    private readonly IMessagingContext<ChatHub> hubContext;

    public ChatService(IMessagingContext<ChatHub> hubContext)
    {
        this.hubContext = hubContext;
    }

    [AllowStaticCommand]
    public async Task SendMessage(string message, string authorName)
    {
        if (!string.IsNullOrWhiteSpace(message))
        {
            var chatMessage = new ChatMessage()
            {
                Author = authorName,
                Message = message,
                CreatedDate = DateTime.UtcNow
            };

            await hubContext.Clients.All.InvokeMethod("chatMessage", chatMessage);
        }
    }
}
