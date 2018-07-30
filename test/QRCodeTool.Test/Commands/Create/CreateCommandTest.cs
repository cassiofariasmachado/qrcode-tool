using System;
using FakeItEasy;
using McMaster.Extensions.CommandLineUtils;
using QRCodeTool.Commands;
using QRCodeTool.Services;
using Xunit;

namespace QRCodeTool.Test.Commands.Create
{
    public class CreateCommandTest
    {
        private IQRCodeService qrCodeService;
        private CommandLineApplication commandLineApplication;

        public CreateCommandTest()
        {
            qrCodeService = A.Fake<IQRCodeService>();
            commandLineApplication = A.Fake<CommandLineApplication>();
        }


        [Fact]
        public void CreateCommandShouldWriteQRCodeOnCli()
        {
            var createCommand = new CreateCommand(qrCodeService);

            createCommand.Message = "message";
            createCommand.OutputType = OutputType.CLI;

            createCommand.OnExecute(commandLineApplication);

            A.CallTo(() => qrCodeService.GetAsciiQRCode(createCommand.Message))
                .MustHaveHappened();
        }

        [Fact]
        public void CreateCommandShouldSaveQRCodePNG()
        {
            var createCommand = new CreateCommand(qrCodeService);

            createCommand.Message = "message";
            createCommand.OutputType = OutputType.PNG;

            createCommand.OnExecute(commandLineApplication);

            A.CallTo(() => qrCodeService.SavePngQRCode(A<string>.Ignored, createCommand.Message))
                .MustHaveHappened();
        }
    }
}
