using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using WpfStartFromNet9.Stores;
using WpfStartFromNet9.ViewModels;

namespace WpfStartFromNet9
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : System.Windows.Application
    {
        private readonly IHost _host;
        private readonly System.Windows.Forms.NotifyIcon _icon;

        public App()
        {
            _icon = new System.Windows.Forms.NotifyIcon();
            _host = Host.CreateDefaultBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddSingleton<CounterStore>();
                    services.AddSingleton<MainWindowViewModel>();

                    services.AddSingleton(s => new MainWindow()
                    {
                        DataContext = s.GetRequiredService<MainWindowViewModel>()
                    });
                })
                .Build();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _host.Start();

            _icon.Icon = System.Drawing.SystemIcons.WinLogo;
            _icon.Text = "SingletonSean";
            _icon.Click += NotifyIcon_Click;

            _icon.ContextMenuStrip = new System.Windows.Forms.ContextMenuStrip();
            _icon.ContextMenuStrip.Items.Add("Show", new Bitmap(16, 16), OnShowClicked);
            _icon.Visible = true;

            MainWindow = _host.Services.GetRequiredService<MainWindow>();
            MainWindow.Show();

            base.OnStartup(e);
        }

        private void OnShowClicked(object? sender, EventArgs e)
        {
            System.Windows.MessageBox.Show("Show");
        }

        private void NotifyIcon_Click(object? sender, EventArgs e)
        {
            MainWindow.WindowState = WindowState.Normal;
            MainWindow.Activate();
        }


        protected override void OnExit(ExitEventArgs e)
        {
            _host.Dispose();

            base.OnExit(e);
        }
    }
}
