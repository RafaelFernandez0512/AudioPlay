using MediaManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioPlay.Views.PrincipalPages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlayMusicPage : ContentPage
    {
        public PlayMusicPage()
        {
            InitializeComponent();
        }

        private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            var control = sender as Slider;
            var value = Convert.ToDouble(CrossMediaManager.Current.Position.TotalSeconds);
            if (control.Value >value)
                CrossMediaManager.Current.SeekTo(TimeSpan.FromSeconds(e.NewValue));
        }
    }
}