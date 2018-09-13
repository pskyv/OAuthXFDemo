using Prism;
using Prism.Ioc;
using OAuthXFDemo.ViewModels;
using OAuthXFDemo.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Prism.Unity;
using OAuthXFDemo.Services;
using Xamarin.Essentials;
using System;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace OAuthXFDemo
{
    public partial class App : PrismApplication
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();
            
            if (Preferences.Get("IsLoggedIn", false) && DateTime.Now < Preferences.Get("ExpiryDate", DateTime.Now))
            {
                await NavigationService.NavigateAsync("NavigationPage/UserProfilePage");
            }
            else
            {
                await NavigationService.NavigateAsync("LoginPage");
            }
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage>();
            containerRegistry.RegisterForNavigation<LoginPage>();

            containerRegistry.RegisterSingleton(typeof(IAuthenticationService), typeof(AuthenticationService));
            containerRegistry.RegisterSingleton(typeof(IApiService), typeof(ApiService));
            containerRegistry.RegisterForNavigation<UserProfilePage>();
        }
    }
}
