namespace TestAkka.Routes
{
    public class PaymentSentMessage
    {
        public int AccountNumber { get; }

        public PaymentSentMessage(int accountNumber)
        {
            AccountNumber = accountNumber;
        }
    }
}