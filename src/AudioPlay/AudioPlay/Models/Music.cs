using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace AudioPlay.Models
{
    public class Music:INotifyPropertyChanged
    {

        public string Title { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public string Gender { get; set; }
        public string Artist { get; set; }
        public bool IsRecent { get; set; }
        private string url;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Url
        {
            get { return url.Contains(".jpg") ? string.Empty: url; }
            set { url = value; }
        }

        public bool IsEnable { get; set; }
        public bool IsPlaying { get; set; } = false;
    }
}
