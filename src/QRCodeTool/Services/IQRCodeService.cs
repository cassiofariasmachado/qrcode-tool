namespace QRCodeTool.Services
{
    public interface IQRCodeService
    {
        string GetAsciiQRCode(string text);

        void SavePngQRCode(string path, string text);
    }
}