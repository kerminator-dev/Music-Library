using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MusicDbApp.DbContexts;
using MusicDbApp.ViewModels;
using System.Windows;

namespace MusicDbApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IHost _host;

        public App() : base()
        {
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    // Получение строки подключения и инициализация строителя
                    string connectionString = hostContext.Configuration.GetConnectionString("MusicDatabase");

                    // Регистрация сервисов
                    services.AddDbContext<MusicDbContext>(options => options.UseSqlite(connectionString));
                    services.AddSingleton<MainWindow>();
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.DataContext = new MainWindowViewModel
            (
                _host.Services.GetRequiredService<MusicDbContext>()
            );
            MainWindow.Show();

            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
