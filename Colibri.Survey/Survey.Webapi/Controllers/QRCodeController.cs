using Microsoft.AspNetCore.Mvc;
using Survey.ApplicationLayer.Services.Interfaces;

namespace Survey.Webapi.Controllers
{
    [Route("api/qr-code")]
    public class QRCodeController : Controller
    {
        private readonly IQRCodeService _qrCodeService;

        public QRCodeController(IQRCodeService qrCodeService)
        {
            _qrCodeService = qrCodeService;
        }

        [HttpGet]
        public IActionResult Get(string text)
        {
            return Ok(_qrCodeService.GetBase64QRCode(text));
        }
    }
}