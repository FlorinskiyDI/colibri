namespace Survey.ApplicationLayer.Services.Interfaces
{
    public interface IQRCodeService
    {
        string GetBase64QRCode(string text);
    }
}
