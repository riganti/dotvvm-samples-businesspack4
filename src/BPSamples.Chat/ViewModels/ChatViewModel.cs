using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BPSamples.Chat.Models;
using DotVVM.Framework.ViewModel;

namespace BPSamples.Chat.ViewModels
{
    public class ChatViewModel : MasterPageViewModel
    {

        public List<ChatMessage> Messages { get; set; } = new List<ChatMessage>();

        public bool IsConnected { get; set; }

        [FromQuery("author")]
        public string AuthorName { get; set; }

        public string NewMessageText { get; set; }


        public override Task Init()
        {
            if (string.IsNullOrWhiteSpace(AuthorName))
            {
                Context.RedirectToRoute("Default");
            }

            return base.Init();
        }
    }
}

