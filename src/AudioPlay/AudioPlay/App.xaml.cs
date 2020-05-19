using AudioPlay.ViewModels;
using AudioPlay.Views;
using AudioPlay.Views.PrincipalPages;
using Plugin.SharedTransitions;
using Prism;
using Prism.Ioc;
using Prism.Unity;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace AudioPlay
{
    public partial class App : PrismApplication
    {
        public App(IPlatformInitializer initializer = null) : base(initializer) { }
        protected override void OnInitialized()
        {
            InitializeComponent();
            NavigationService.NavigateAsync(new Uri("/SharedTransitionNavigationPage/HomePage"));

        }
        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<SharedTransitionNavigationPage>();
            containerRegistry.RegisterForNavigation<HomePage, HomePageViewModel>();
            containerRegistry.RegisterForNavigation<MusicPage, MusicPageViewModel>();
            containerRegistry.RegisterForNavigation<PlayMusicPage, PlayMusicPageViewModel>();

        }
    }
}
