namespace IOBBank.Domain.Interfaces.Repositories
{
    public interface IApiWhatsApp
    {
        void SendMessage(string phoneNumber, string message);
    }
}
