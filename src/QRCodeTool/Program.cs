using McMaster.Extensions.CommandLineUtils;
using Microsoft.Extensions.DependencyInjection;
using QRCoder;
using QRCodeTool.Commands;
using QRCodeTool.Services;

namespace QRCodeTool
{
    [HelpOption]
    [Command("qrcode")]
    [Subcommand("create", typeof(CreateCommand))]
    public class QRCodeCommand
    {
        public static int Main(string[] args)
        {
            var app = new CommandLineApplication<QRCodeCommand>();

            app.Conventions
                .UseDefaultConventions()
                .UseConstructorInjection(Services);

            return app.Execute(args);
        }

        private static ServiceProvider Services => new ServiceCollection()
               .AddSingleton<IConsole>(PhysicalConsole.Singleton)
               .AddTransient<QRCodeGenerator>()
               .AddTransient<IQRCodeService, QRCodeService>()
               .BuildServiceProvider();

        private void OnExecute(CommandLineApplication app)
        {
            new ServiceCollection()
            .AddSingleton<IConsole>(PhysicalConsole.Singleton)
            .BuildServiceProvider();

            app.ShowHelp();
        }
    }
}
