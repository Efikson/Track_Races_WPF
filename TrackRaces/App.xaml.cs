using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using System.Windows.Media;
using TrackRaces.Models;
using TrackRaces.Services;
using TrackRaces.ViewModels;
using TrackRaces.Views;

namespace TrackRaces
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; }

        protected override void OnStartup(StartupEventArgs e)
        {
            // Create and configure DI container
            var serviceCollection = new ServiceCollection();
            ConfigureServices(serviceCollection);

            // Create ServiceProvider
            ServiceProvider = serviceCollection.BuildServiceProvider();
            
            // Show the main window
            var mainWindow = ServiceProvider.GetRequiredService<MainMenu>();
            mainWindow.Show();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            //register models
            services.AddSingleton<GameRenderer>();
            services.AddSingleton<PlayerCollision>();
            services.AddSingleton<PlayerController>();
            services.AddSingleton<TimerManager>(); 
            services.AddSingleton<GameLoop>();            

            // Register ViewModels
            services.AddSingleton<GameWindowViewModel>();
            services.AddSingleton<MainMenuViewModel>();

            // Register Views
            services.AddSingleton<MainMenu>();
            services.AddTransient<GameWindow>();
        }
        
    }
}
