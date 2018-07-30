using System.IO;
using McMaster.Extensions.CommandLineUtils;
using QRCodeTool.Services;

namespace QRCodeTool.Commands
{
    [HelpOption]
    [Command(Description = "Create a new QR code")]
    public class CreateCommand
    {
        private readonly IQRCodeService _qrCodeService;

        [Argument(0, "Message", "The message to be coded")]
        public string Message { get; set; }

        [Option("-o", "The output type of the QR Code.", CommandOptionType.SingleValue)]
        public OutputType OutputType { get; set; }

        public CreateCommand(IQRCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        public void OnExecute(CommandLineApplication app)
        {
            switch (OutputType)
            {
                case OutputType.PNG:
                    SaveQrCodeAsPng(app);
                    break;
                default:
                    WriteQrCode(app);
                    break;
            }
        }

        public void SaveQrCodeAsPng(CommandLineApplication app)
        {
            var path = Path.Combine(app.WorkingDirectory, "qrcode.png");
            _qrCodeService.SavePngQRCode(path, Message);
            app.Out.WriteLine("Your awesome QR Code was saved at:");
            app.Out.WriteLine(path);
        }

        public void WriteQrCode(CommandLineApplication app)
        {
            var qrcode = _qrCodeService.GetAsciiQRCode(Message);
            app.Out.WriteLine("Your awesome QR Code is:\n");
            app.Out.WriteLine(qrcode);
        }
    }
}