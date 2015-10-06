using Akka.Actor;
using Akka.TestKit.NUnit;
using NUnit.Framework;

namespace TestAkka.UnitTest
{
    [TestFixture]
    public class SampleActorTest : TestKit
    {
        public class MyFirstActor : ReceiveActor
        {
            public MyFirstActor()
            {
                Receive<string>(s => Sender.Tell(s));
            }
        }

        [Test]
        public void MyFirstActorShouldPrintOnConsole()
        {
            var actorRef = Sys.ActorOf(Props.Create<MyFirstActor>());

            actorRef.Tell("Ola");
            ExpectMsg("Ola");
        }
    }
}
