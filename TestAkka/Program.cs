using System;
using Akka.Actor;
using static System.Console;

namespace TestAkka
{
    class Program
    {
        static void Main(string[] args)
        {
            var actorSystem = ActorSystem.Create("MyFirstSystem");

            actorSystem.ActorOf(Props.Create<MyFirstActor>(), "MyFirstActor");
            var actorSelection = actorSystem.ActorSelection("/user/MyFirstActor");
            actorSelection.Tell("2");
            actorSelection.Tell("1");
            ReadLine();
            actorSystem.Shutdown();
            actorSystem.AwaitTermination(TimeSpan.FromSeconds(5));
            ReadLine();
        }
    }

    public class MyFirstActor : ReceiveActor
    {
        public MyFirstActor()
        {
            Receive<string>(s => WriteLine(s));
        }
    }
}
