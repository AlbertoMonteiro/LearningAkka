using System;
using System.Diagnostics;
using System.IO;
using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Ninject;
using Ninject;
using static System.Console;

namespace TestAkka.Routes
{
    class Program
    {
        private static ActorSystem _actorSystem;

        static void Main(string[] args)
        {
            CreateActorSystem();

            var jobCoordinator = _actorSystem.ActorOf<JobCoordinatorActor>("JobCoordinator");

            var jobTime = Stopwatch.StartNew();

            jobCoordinator.Tell(new ProcessFileMessage(Path.Combine(Environment.CurrentDirectory, "file1.csv")));

            _actorSystem.AwaitTermination();

            jobTime.Stop();

            WriteLine($"Job completes {jobTime.Elapsed}");
            ReadLine();
        }

        private static void UsingGrouRouter()
        {
            _actorSystem.ActorOf(_actorSystem.DI().Props<PaymentWorkerActor>(), "PaymentWorker1");
            _actorSystem.ActorOf(_actorSystem.DI().Props<PaymentWorkerActor>(), "PaymentWorker2");
            _actorSystem.ActorOf(_actorSystem.DI().Props<PaymentWorkerActor>(), "PaymentWorker3");
        }

        private static void CreateActorSystem()
        {
            var kernel = new StandardKernel();
            kernel.Bind<IPaymentGateway>().To<DemoPaymentGateway>();
            kernel.Bind<PaymentWorkerActor>().ToSelf();

            _actorSystem = ActorSystem.Create("RouteActorSystem");

            var ninjectDependencyResolver = new NinjectDependencyResolver(kernel, _actorSystem);
        }
    }
}
