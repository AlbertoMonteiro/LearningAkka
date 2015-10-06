using System;
using System.Threading;

namespace TestAkka.Routes
{
    public class DemoPaymentGateway : IPaymentGateway
    {
        public void Pay(int accountNumber, decimal value)
        {
            Thread.Sleep(200);
            Console.WriteLine($"Paied {value:C} to {accountNumber}");
        }
    }
}