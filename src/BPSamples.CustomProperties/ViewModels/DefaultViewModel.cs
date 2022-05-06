using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace BPSamples.CustomProperties.ViewModels
{
    public class DefaultViewModel : MasterPageViewModel
    {

        public string Title { get; set; }

        public DefaultViewModel()
        {
            Title = "Hello from DotVVM!";
        }

        public string SelectedFruit { get; set; } = "Banana";

        public List<string> Fruits { get; set; } = new List<string> {
            "Banana", "Apple", "Orange", "Peach"
        };
    }
}
