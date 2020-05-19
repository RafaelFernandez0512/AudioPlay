using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AudioPlay.Models
{
    public class Playlist
    {
        public ObservableCollection<Music> MyPlaylists { get; set; }    
    }
}
