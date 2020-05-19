using MediaManager;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Security.Authentication.ExtendedProtection;
using System.Text;
using System.Threading.Tasks;
using AudioPlay.Models;
using Xamarin.Forms;
using AudioPlay.Helpers;
using MediaManager.Forms;
using System.Linq;

namespace AudioPlay.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        protected INavigationService navigationService;
        protected IPageDialogService pageDialogService;
        public DelegateCommand<Music> SelectCommad { get; set; }
        public DelegateCommand PlayCommand { get; set; }
        public DelegateCommand<string> ChangeMusicCommand { get; set; }
        private TimeSpan position;

        public TimeSpan Position
        {
            get { return position; }
            set { 
                position = value;

            }
        }

        private double maximum=100f;

        public bool IsClean { get; set; } 
        public double Maximum
        {
            get { return maximum; }
            set
            {
                if (value > 0)
                {
                    maximum = value;
                }
            }
        }
        private Music selectMusic;

        public Music SelectMusic
        {
            get { return selectMusic; }
            set { selectMusic = value; }
        }

        public TimeSpan Duration { get; set; }
        public BaseViewModel(INavigationService navigationService,IPageDialogService pageDialogService)
        {
            this.navigationService = navigationService;
            this.pageDialogService = pageDialogService;
            IsClean = CrossMediaManager.Current.IsPlaying();
            PlayCommand = new DelegateCommand(async () => {

                await Play(SelectMusic);
            });

            ChangeMusicCommand = new DelegateCommand<string>(async (p) =>
            {
                if (p == "N")
                {
                    await NextMusic(SelectMusic);
                }
                else
                {
                    await PreviusMusic(SelectMusic);
                }
                Setting.SelectMusic = SelectMusic;
            });
        }
        protected async Task PlayMusic(Music music)
        {
            var mediaInfo = CrossMediaManager.Current;
            
            if (!mediaInfo.IsPlaying()||music.Url!=Setting.Url)
            {
                if (music.Url != Setting.Url)
                    await mediaInfo.Stop();
                Setting.Url = music.Url;
                await mediaInfo.Play(music.Url);
                music.IsPlaying = true;

            }
            else
            {
                   music.IsPlaying = mediaInfo.IsPlaying();
                 
            }
            IsClean = true;
            mediaInfo.MediaItemFinished += async(sender, args) =>
            {
                music.IsPlaying = false;
                await NextMusic(music);
                

            };
            Device.StartTimer(TimeSpan.FromMilliseconds(500), () =>
            {
                Duration = mediaInfo.Duration;
                Maximum = Duration.TotalSeconds;
                Position = mediaInfo.Position;
                return true;
            });

        }
        protected async Task NextMusic( Music music)
        {
            var currrent = Setting.Musics.IndexOf(music);
            if (currrent < Setting.Musics.Count - 1)
            {
                music.IsPlaying = false;
                music = Setting.Musics[currrent + 1];
                SelectMusic = music;
                await PlayMusic(music);

            }
        }
        protected async Task PreviusMusic( Music music)
        {
            var currrent = Setting.Musics.IndexOf(SelectMusic);
            if (currrent > 0)
            {
                music.IsPlaying = false;
                SelectMusic = Setting.Musics[currrent - 1];
                await PlayMusic(music);
            }
        }
        protected async Task Play(Music music)
        {
            if (music.IsPlaying)
            {
                await CrossMediaManager.Current.Pause();
                music.IsPlaying = false;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                music.IsPlaying = true;
            }
        }
    }
}
