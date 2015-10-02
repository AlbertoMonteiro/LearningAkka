using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Akka.Actor;
using static System.Console;

namespace TestAkka
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("MyFristSystem");
            //for (int i = 0; i < 500000; i++)
            //{
            //    Console.WriteLine(i);
            //}

            int a;
            int b;
            ThreadPool.GetMaxThreads(out a, out b);
            ThreadPool.SetMaxThreads(a * 10, b * 10);
            ThreadPool.GetMaxThreads(out a, out b);
            ThreadPool.GetAvailableThreads(out a, out b);
            WriteLine(a);
            WriteLine(b);
            ReadLine();
            Clear();
            Parallel.For(0, 500000, i =>
            {
                var actorRef = actorSystem.ActorOf(Props.Create<MyFirtActor>());
                actorRef.Tell(i);
            });
            ForegroundColor = ConsoleColor.Red;
            WriteLine("All Actors created");
            ResetColor();
            ReadLine();
            actorSystem.Shutdown();
        }
    }

    public class MyFirtActor : UntypedActor
    {
        protected override void OnReceive(object message)
        {
            //if (((int)message) % 2 == 0)
            //{
            //    var random = new Random();
            //    Thread.Sleep(random.Next(200));
            //}
            WriteLine(message);
            ForegroundColor = ConsoleColor.Red;
            WriteLine(Process.GetCurrentProcess().Threads.Count);
            ResetColor();
        }
    }
}
