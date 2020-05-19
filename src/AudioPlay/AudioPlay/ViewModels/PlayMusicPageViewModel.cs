using MediaManager;
using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using AudioPlay.Models;
using Xamarin.Forms;
using AudioPlay.Helpers;
using Prism.Xaml;

namespace AudioPlay.ViewModels
{
    public class PlayMusicPageViewModel:BaseViewModel,IInitialize
    {
        public DelegateCommand RunningMusicCommand { get; set; }
        public ObservableCollection<Music> Musics { get; set; }
        private bool isTabStop;

        public bool IsTabStop
        {
            get { return isTabStop; }
            set { isTabStop = value; }
        }

        public PlayMusicPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            
            RunningMusicCommand = new DelegateCommand(async () => await PlayMusic(SelectMusic));
        }

        public void Initialize(INavigationParameters parameters)
        {
            var param = parameters[nameof(Music)] as Music;
            if (param!=null)
            {
                SelectMusic = param;
                RunningMusicCommand.Execute();
            }
        }
    }
}
