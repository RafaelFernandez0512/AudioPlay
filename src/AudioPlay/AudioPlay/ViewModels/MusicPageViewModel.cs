using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using AudioPlay.Helpers;
using AudioPlay.Models;
using AudioPlay.Views.PrincipalPages;
using System.Linq;

namespace AudioPlay.ViewModels
{
    public class MusicPageViewModel : BaseViewModel,IInitialize
    {
        public DelegateCommand GoToPlayCommand { get; set; }
        public Album Albums { get; set; }
        public Music GetMusic { get; set; }
        public DelegateCommand GoToMusicCommand { get; set; }
        public string SelectTitle { get; set; }
        public MusicPageViewModel(INavigationService navigationService, IPageDialogService pageDialogService) : base(navigationService, pageDialogService)
        {
            GoToPlayCommand = new DelegateCommand(async () =>
            {
                var param = new NavigationParameters
                {
                    { nameof(Music), SelectMusic },
                    { nameof(MusicPage), Albums }
                };
                await navigationService.NavigateAsync(new Uri(NavigationConstants.PlayMusicPage, UriKind.Relative), param);
            });
            SelectCommad = new DelegateCommand<Music>(async (select) =>
            {

                if (SelectMusic == null || SelectMusic.Url != select.Url)
                {
                   
                    if (SelectMusic != null)
                        SelectMusic.IsPlaying = false;
                    Setting.Musics = Albums.Musics;
                    select.IsPlaying = true;
                    SelectMusic = select;
                    SelectTitle = select.Title;
                    Setting.SelectMusic= SelectMusic;
                }

                await PlayMusic(SelectMusic);
            });
            GoToMusicCommand = new DelegateCommand(async () =>
            {
                var param = new NavigationParameters();
                param.Add(nameof(Music), SelectMusic);
                await navigationService.NavigateAsync(new Uri(NavigationConstants.PlayMusicPage, UriKind.Relative), param);
            });

        }

        public void Initialize(INavigationParameters parameters)
        {
            var param = parameters[nameof(MusicPageViewModel)] as Album;
            var select = parameters[nameof(Music)] as Music;
            Albums = param;
            if (select != null)
            {
                SelectMusic = select;
                SelectCommad.Execute(SelectMusic);
            }
        }
    }
}
