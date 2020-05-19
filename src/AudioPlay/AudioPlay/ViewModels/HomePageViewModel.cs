using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AudioPlay.Helpers;
using AudioPlay.Models;
using Xamarin.Forms.Internals;
using AudioPlay.Services;
using Xamarin.Essentials;

namespace AudioPlay.ViewModels
{
    public class HomePageViewModel:BaseViewModel,INavigatedAware
    {
		public ObservableCollection<Music> TopsMusics { get; set; } = new ObservableCollection<Music>();
		private Music currentMusic;


		public Music CurrentMusic
		{
			get { return currentMusic; }
			set {
				currentMusic = value;
				if (currentMusic != null)
				{
					TopsMusics = new ObservableCollection<Music>(TopsMusics.Select(e => { e.IsEnable = false; return e; }));
					CurrentMusic.IsEnable = true;
				}
			}
		}
		private Album selectAlbum;

		public Album SelectAlbum
		{
			get { return selectAlbum; }
			set { 
				selectAlbum = value;
				if (selectAlbum!=null)
				{
					GoToAlbumCommand.Execute();
				}


			}
		}
		public string SelectTitle { get; set; }
		public DelegateCommand PermissonCommand { get; set; }
		public ObservableCollection<Album> MusicGenrecs { get; set; }
		public ObservableCollection<Album> MyPlaylist { get; set; }
		public DelegateCommand LoadCommand { get; set; }
		public DelegateCommand GoToMusicCommand { get; set; }
		IFileSystem fileSystem;
		public DelegateCommand  GoToAlbumCommand { get; set; }
		public HomePageViewModel(INavigationService navigationService, IPageDialogService pageDialogService, IFileSystem fileSystem) :base(navigationService,pageDialogService)
		{
				
			this.fileSystem = fileSystem;
			PermissonCommand = new DelegateCommand(async () => await RequestPermissionAsync());
			PermissonCommand.Execute();
			
			SelectCommad = new DelegateCommand<Music>(async(select) =>
			{
				if (SelectMusic == null|| SelectMusic.Url!= select.Url)
				{
					
					if (SelectMusic != null)
						SelectMusic.IsPlaying = false;
					Setting.Musics = TopsMusics;
					SelectMusic = select;
					SelectTitle = select.Title;
					
				}
				await PlayMusic(SelectMusic);
			});
			
			MusicGenrecs = new ObservableCollection<Album>(){
				new Album("Rap",null),
				new Album("HipHop",null),
				new Album("Pop Music",null),
				new Album("Blues",null),
				new Album("Children’s Music",null),
				new Album("Classical",null),
				new Album("Techno",null),
				};
			CurrentMusic = TopsMusics.FirstOrDefault();
			GoToMusicCommand = new DelegateCommand(async () =>
			{
				var param = new NavigationParameters();
				param.Add(nameof(Music), SelectMusic);
				await navigationService.NavigateAsync(new Uri(NavigationConstants.PlayMusicPage, UriKind.Relative),param);
			});
			GoToAlbumCommand = new DelegateCommand(async () =>
			{
				var param = new NavigationParameters();
				param.Add(nameof(MusicPageViewModel), SelectAlbum);
				await navigationService.NavigateAsync(new Uri(NavigationConstants.MusicPage, UriKind.Relative),param);
				SelectAlbum = null;
			});



		}
		async Task RequestPermissionAsync()
		{
			var status = await Permissions.CheckStatusAsync<Permissions.StorageRead>();
			if (status != PermissionStatus.Granted)
			{
				status = await Permissions.RequestAsync<Permissions.StorageRead>();
			}
			var paths = fileSystem.GetExternalStorage();
			var list = DirectoryHelper.GetAlbumFiles(paths);
			MyPlaylist = new ObservableCollection<Album>(list);
			TopsMusics = new ObservableCollection<Music>(MyPlaylist.First(e => e.Title == "Bad Bunny - YHLQMDLG (Album) (2020).Net)").Musics);
		}

		public void OnNavigatedFrom(INavigationParameters parameters)
		{
		}

		public void OnNavigatedTo(INavigationParameters parameters)
		{
			if (Setting.SelectMusic != null)
			{
				SelectMusic = Setting.SelectMusic;
				SelectCommad.Execute(SelectMusic);
			}
		}
	}
}
