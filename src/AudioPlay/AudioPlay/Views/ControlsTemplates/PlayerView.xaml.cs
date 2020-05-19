using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioPlay.Views.ControlsTemplates
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayerView : ContentView
    {
        public PlayerView()
        {
            InitializeComponent();
        }
    }
}