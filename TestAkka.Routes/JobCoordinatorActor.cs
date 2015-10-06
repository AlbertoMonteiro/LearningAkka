using System.Collections.Generic;
using System.IO;
using System.Linq;
using Akka.Actor;
using Akka.DI.Core;
using Akka.Routing;

namespace TestAkka.Routes
{
    public class JobCoordinatorActor : ReceiveActor
    {
        private readonly IActorRef _paymentWorker;
        private int _numeberOrRemainPayments;

        public JobCoordinatorActor()
        {
            _paymentWorker = UsingPoolRoutePaymentWorker();

            Receive<ProcessFileMessage>(message => StartNewJob(message.FilePath));

            Receive<PaymentSentMessage>(message =>
            {
                _numeberOrRemainPayments--;

                var jobIsComplete = _numeberOrRemainPayments == 0;

                if (jobIsComplete)
                    Context.System.Shutdown();
            });
        }

        private IActorRef UsingPoolRoutePaymentWorker()
        {
            return Context.ActorOf(Context.DI()
                .Props<PaymentWorkerActor>()
                .WithRouter(new RoundRobinPool(10)));
        }

        private static IActorRef UsingGroupRoutePaymentWorker()
        {
            return Context.ActorOf(
                Props.Empty.WithRouter(new RoundRobinGroup(
                    "/user/PaymentWorker1",
                    "/user/PaymentWorker2",
                    "/user/PaymentWorker3")));
        }

        private void StartNewJob(string filePath)
        {
            var requests = ParseCsvFile(filePath);
            _numeberOrRemainPayments = requests.Count;

            foreach (var request in requests)
            {
                _paymentWorker.Tell(request);
            }
        }

        private static List<SendPaymentMessage> ParseCsvFile(string filePath)
            => File.ReadLines(filePath)
                .Select(x => x.Split(','))
                .Select(x => new SendPaymentMessage(x[0], x[1], int.Parse(x[3]), decimal.Parse(x[2])))
                .ToList();
    }
}