using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Linq;
using MMOCore.Repository.Models;
using MMOCore.EFRepository;

/// <summary>
/// Referenced by https://docs.microsoft.com/de-de/dotnet/api/system.servicemodel.servicehost?view=netframework-4.8
/// Standart servicehost implementation
/// </summary>

namespace MMOCore.Server
{
    class Program
    {
        static void Main(string[] args)
        {
            //In order to run this project properly, 
            //Make sure to enable the MSSQL Service
            //It should generate a "MMOGameDB" database, in which it stores all sessions and events
            //If the database existed before runtime, it will be dropped and rebuilt empty

            using (var ctx = new GameContext())
            {
                GameEvent events = new GameEvent(ctx);
                GameSession session = new GameSession(ctx);
                SessionScope scope = new SessionScope(ctx, events, session);
                GameController controller = new GameController(scope);
                SessionService service = new SessionService() { _ctr = controller };

                //this method generates a context class, which handles DB connection some testing
                //EventLogMSG("Building Database", ConsoleColor.Cyan);
                //TestDbContext(controller);
                //EventLogMSG("Database Created", ConsoleColor.Green);

                //this method tests database querys
                //EventLogMSG("Testing Querys", ConsoleColor.Cyan);
                //TestDbLinqQuery(controller);
                //EventLogMSG("Testing Querys Done", ConsoleColor.Green);

                //this method starts a generic wcf service
                EventLogMSG("Starting WCF Host", ConsoleColor.Cyan);
                Host hostService = new Host(service, "net.tcp://localhost:8009/MMOGameDB");
                hostService.StartServiceHost();
                EventLogMSG("WCF closed", ConsoleColor.Red);

                //EventLogMSG("Test Done", ConsoleColor.Magenta);
                Console.ReadLine();
            }
        }

        //static void TestDbContext(GameController controller)
        //{
            
        //    string[] eventNames = { "EventA", "EventB", "EventC", "EventD" };
        //    int maxSessions = 10;
        //    int maxEvents = 200;
            
        //    //Create Test Game Sessions
        //    for (int i = 0; i < maxSessions; i++)
        //    {
        //        controller.CreateGameSession(i);
        //        Console.WriteLine("Created Game Session with ID {0}", i);
        //    }

        //    Random rand = new Random();
        //    //Create Test Events
        //    for (int i = 0; i < maxEvents; i++)
        //    {
        //        string evname = eventNames[rand.Next(0, eventNames.Length - 1)];
        //        int sid = rand.Next(0, maxSessions - 1);
        //        var gameEvent = new POCO_Event { Id = i, Name = evname, SessionId = sid};

        //        controller.CreateGameEvent(evname, sid);
        //        Console.WriteLine("Created Game Event with ID {0}", i);
        //    }

        //    controller.Save();
        //}

        //static void TestDbLinqQuery(GameController controller)
        //{
        //    foreach (var ev in controller.GetAllEventsOrderedByCountDesc())
        //    {
        //        Console.WriteLine("{0}, with ID {1} on Session {2}", ev.Name, ev.Id, ev.SessionId);
        //    }
        //}

        //static void TestWCFService()
        //{
            //using (ServiceHost serviceHost = new ServiceHost(typeof('UnitOfWork')))
            //{
            //    try
            //    {
            //        // Open the ServiceHost to start listening for messages.
            //        serviceHost.Open();

            //        // The service can now be accessed.
            //        Console.WriteLine("The service is ready.");
            //        Console.WriteLine("Press <ENTER> to terminate service.");
            //        Console.ReadLine();

            //        // Close the ServiceHost.
            //        serviceHost.Close();
            //    }
            //    catch (TimeoutException timeProblem)
            //    {
            //        Console.WriteLine(timeProblem.Message);
            //        Console.ReadLine();
            //    }
            //    catch (CommunicationException commProblem)
            //    {
            //        Console.WriteLine(commProblem.Message);
            //        Console.ReadLine();
            //    }
            //}
        //}

        static void EventLogMSG(string msg, ConsoleColor col)
        {
            ConsoleColor oldColor = Console.ForegroundColor;
            Console.ForegroundColor = col;
            Console.WriteLine("Server Log: [{0}]", msg);
            Console.ForegroundColor = oldColor;
        }
    }
}
