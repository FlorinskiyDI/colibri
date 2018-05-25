using Microsoft.Extensions.Configuration;
using QRCoder;
using Survey.ApplicationLayer.Services.Interfaces;

namespace Survey.ApplicationLayer.Services
{
    public class QRCodeService : IQRCodeService
    {
        const int DefaultPixelsPerModule = 20;

        private readonly IConfiguration _configuration;

        public QRCodeService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string GetBase64QRCode(string text)
        {
            var qrCodeGenerator = new QRCodeGenerator();
            var qrCode = qrCodeGenerator.CreateQrCode(text, QRCodeGenerator.ECCLevel.Q);
            var base64QRCode = new Base64QRCode(qrCode);
            
            if (!int.TryParse(_configuration["QRCode:PixelsPerModule"], out int pixelsPerModule))
            {
                pixelsPerModule = DefaultPixelsPerModule;
            }

            return base64QRCode.GetGraphic(pixelsPerModule);
        }
    }
}