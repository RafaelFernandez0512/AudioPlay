using AudioPlay.Models;
using MediaManager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms.Internals;

namespace AudioPlay.Helpers
{
    public static class DirectoryHelper
    {
        public static List<Music> GetFiles(List<string> urls)
        {
            List<Music> list = null;
            if (urls.Count==0)
            {
                return new List<Music>();
            }
            urls.ForEach(e => list = new List<Music>() { 
            new Music{ 
                Url=e,
                Title = Path.GetFileName(e),
                Artist = FindName(Path.GetFileName(e))
            }
            });
            return list;
        }
        public static List<Album> GetAlbumFiles(Dictionary<string,List<string>> urls)
        {
            List<Album> albums = new List<Album>();
            List<Music> list = new List<Music>();
            if (urls.Count == 0)
            {
                return new List<Album>();
            }
            foreach (var item in urls)
            {

                    list = new List<Music>();
                    item.Value.Where(e=>!e.Contains("jpg")).ForEach(e => list.Add(
                    new Music
                    {
                        Url = e,
                        Title = FindTitle(Path.GetFileName(e)),
                        Artist = FindName(Path.GetFileName(e))
                    }
                    ));
                albums.Add(new Album(item.Key, new ObservableCollection<Music>(list)) { Image=item.Value.Where(e=>e!=null&&e.Contains(".jpg")).FirstOrDefault()});
            }
            return albums;
        }
        static string FindName(string title)
        {

            if (title.Contains("-"))
            {
                int position = title.IndexOf(title.FirstOrDefault(e=>e=='-'));
                return title.Substring(0, position - 1);
            }
            return title;
        }
        static string FindTitle(string title)
        {

            if (title.Contains("-"))
            {
                int position = title.IndexOf(title.FirstOrDefault(e => e == '-'));
                return title.Substring(position+1);
            }
            return title;
        }


    }
}
