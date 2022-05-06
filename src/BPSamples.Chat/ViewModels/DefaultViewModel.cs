using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using BPSamples.Chat.Models;
using DotVVM.Framework.ViewModel;

namespace BPSamples.Chat.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        [Required(ErrorMessage = "Enter your name")]
        public string AuthorName { get; set; }

        public void EnterChat()
        {
            Context.RedirectToRoute("Chat", query: new { author = AuthorName });
        }
    }
}
