using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AudioPlay.Droid;
using AudioPlay.Services;
using Java.Net;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

[assembly: Dependency(typeof(FileSystemImplementation))]
namespace AudioPlay.Droid
{
    public class FileSystemImplementation : IFileSystem
    {
        Dictionary<string, List<string>> folderMusics = new Dictionary<string, List<string>>();
        public Dictionary<string, List<string>> GetExternalStorage()
        {
            
            var filePath = Android.OS.Environment.ExternalStorageDirectory.AbsolutePath;
            try
            {
                var dir = Directory.GetFiles(filePath,"*.mp3",SearchOption.AllDirectories).ToList();
                dir.ForEach(e => GetMusic(Path.GetFileName(Path.GetDirectoryName(e)), e));
                return folderMusics;
            }
            catch (Exception err)
            {
                var klks = err;
                return null; 
            }

        }
        public void GetMusic(string key,string urls)
        {
            if (folderMusics.ContainsKey(key)&& folderMusics != null)
            {
                folderMusics[key].Add( urls);
            }
            else
            {
                folderMusics.Add(key, new List<string> { urls });
                var dir = Directory.GetFiles(Path.GetDirectoryName(urls), "*.jpg").FirstOrDefault() ;
                if (dir!=null)
                {
                    folderMusics[key].Add(dir);
                }
              
            }
        }
    }
}