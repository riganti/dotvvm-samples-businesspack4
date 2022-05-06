using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotVVM.Framework.ViewModel;

namespace BPSamples.CustomProperties.ViewModels
{
    public class MasterPageViewModel : DotvvmViewModelBase
    {
        public bool IsBpOverrideEnabled { get; set; }

    }
}
