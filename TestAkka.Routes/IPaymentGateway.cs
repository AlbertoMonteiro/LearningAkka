namespace TestAkka.Routes
{
    public interface IPaymentGateway
    {
        void Pay(int accountNumber, decimal value);
    }
}