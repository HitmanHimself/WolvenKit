using System;
using System.CommandLine;
using System.CommandLine.NamingConventionBinder;
using System.Diagnostics;
using System.IO;
using Microsoft.Extensions.Hosting;

namespace CP77Tools.Commands
{
    internal class SettingsCommand : CommandBase
    {
        private const string s_description = "App settings";
        private const string s_name = "settings";

        public SettingsCommand() : base(s_name, s_description) => SetInternalHandler(CommandHandler.Create<IHost>(Action));

        private void Action(IHost host)
        {
            var basedir = AppDomain.CurrentDomain.BaseDirectory;
            var settingsPath = Path.Combine(basedir, "appsettings.json");
            Console.WriteLine($"Opening {settingsPath}");
            new Process
            {
                StartInfo = new ProcessStartInfo(settingsPath)
                {
                    UseShellExecute = true
                }
            }.Start();
        }
    }
}
