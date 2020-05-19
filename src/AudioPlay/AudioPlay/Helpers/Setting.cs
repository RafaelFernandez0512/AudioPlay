using AudioPlay.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;

namespace AudioPlay.Helpers
{
    public static class Setting
    {
        public static string Url { get; set; }
        public static ObservableCollection<Music> Musics { get; set; }
        public static Music SelectMusic { get; set; }
    }
}
