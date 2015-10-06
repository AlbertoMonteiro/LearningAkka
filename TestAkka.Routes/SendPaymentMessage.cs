namespace TestAkka.Routes
{
    public class SendPaymentMessage
    {
        public string FirstName { get; }
        public string LastName { get; }
        public int AccountNumber { get; }
        public decimal Value { get; }

        public SendPaymentMessage(string firstName, string lastName, int accountNumber, decimal value)
        {
            FirstName = firstName;
            LastName = lastName;
            AccountNumber = accountNumber;
            Value = value;
        }
    }
}