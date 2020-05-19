using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Text;

namespace AudioPlay.Models
{
    public class Album
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }
        public ObservableCollection<Music> Musics { get; set; }
        public Album(string tile,  ObservableCollection<Music> musics)
        {
            Title = tile;
            Musics = musics;
        }
    }
}
