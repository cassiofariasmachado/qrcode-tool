using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using QRCoder;

namespace QRCodeTool.Services
{
    public class QRCodeService : IQRCodeService
    {
        private readonly QRCodeGenerator _qrCodeGenerator;

        public QRCodeService(QRCodeGenerator qrCodeGenerator)
        {
            this._qrCodeGenerator = qrCodeGenerator;
        }

        public string GetAsciiQRCode(string text)
        {
            var qrCodeData = _qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.H);
            var qrCode = new AsciiQRCode(qrCodeData);
            return qrCode.GetGraphic(1);
        }

        public void SavePngQRCode(string path, string text)
        {
            var qrCodeData = _qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.H);
            var qrCode = new PngByteQRCode(qrCodeData);
            var byteArray = qrCode.GetGraphic(20);

            using (var image = Image.FromStream(new MemoryStream(byteArray)))
            {
                image.Save(path, ImageFormat.Png);
            }
        }

    }
}
