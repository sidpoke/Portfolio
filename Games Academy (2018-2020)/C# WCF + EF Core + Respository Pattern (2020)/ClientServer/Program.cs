using System;
using System.Collections.Generic;
// Assembly: Microsoft.VisualStudio.QualityTools.UnitTestFramework
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace MMOCore.Client
{
    /// <summary>
    /// Testprogramm
    /// Because it uses a few minutes for the test interval the database should be deleted before each run.
    /// </summary>
    public class Program
    {
        /// <summary>
        /// Number of event names to create for testing
        /// </summary>
        private const int EVENT_NAMES = 100; //100

        /// <summary>
        /// Number of game sessions to create for testing
        /// </summary>
        private const int GAME_SESSIONS = 1000; //1000

        /// <summary>
        /// Maximum number of events per game session during testing
        /// </summary>
        private const int MAX_EVENTS_PER_SESSION = 10;

        /// <summary>
        /// The consumer class provides the interface of the event service host.
        /// It wraps each function and creates a new channel for each method call.
        /// </summary>
        private Consumer consumer;

        /// <summary>
        /// All event names used in this test
        /// </summary>
        private string[] eventNames = new string[EVENT_NAMES];

        /// <summary>
        /// Lookup dictionary for event names
        /// </summary>
        private Dictionary<string, int> eventIndices = new Dictionary<string, int>();

        /// <summary>
        /// How often the events occurred in this test
        /// </summary>
        private int[] eventOccurrences = new int[EVENT_NAMES];

        /// <summary>
        /// Which events are following the events and how often
        /// </summary>
        private int[][] eventFollowers = new int[EVENT_NAMES][];

        /// <summary>
        /// How often the events are last in a game session
        /// </summary>
        private int[] eventBeingLast = new int[EVENT_NAMES];


        Program(Consumer consumer)
        {
            this.consumer = consumer;
            InitializeEvents();
        }

        /// <summary>
        /// Helper method to create event names
        /// </summary>
        private void InitializeEvents()
        {
            for (var i = 0; i < EVENT_NAMES; ++i)
            {
                eventNames[i] = string.Format("ev{0}", i);
                eventIndices[eventNames[i]] = i;
                eventFollowers[i] = new int[EVENT_NAMES];
            }
        }

        /// <summary>
        /// Test the methods Consumer.CreateGameSession and Consumer.CreateEvent
        /// </summary>
        void TestMethodsCreate()
        {
            Console.WriteLine("Create");
            for (var i = 0; i < GAME_SESSIONS; ++i)
            {
                // some deterministic, pseudo random values for initialisation and increment
                int newEventIndex = (i * 21277) % (EVENT_NAMES / 13);
                int de = (((i * 37049) % EVENT_NAMES) / 17) + 1;

                // create game session
                var gameSession = consumer.CreateGameSession();

                // create first event
                consumer.CreateEvent(eventNames[newEventIndex], gameSession);
                eventOccurrences[newEventIndex] += 1;

                int oldEventIndex = newEventIndex;
                newEventIndex += de;

                for (var j = 0; (newEventIndex < EVENT_NAMES) && (j < MAX_EVENTS_PER_SESSION - 1); ++j)
                {
                    // create following events
                    consumer.CreateEvent(eventNames[newEventIndex], gameSession);
                    eventOccurrences[newEventIndex] += 1;
                    eventFollowers[oldEventIndex][newEventIndex] += 1;

                    oldEventIndex = newEventIndex;
                    newEventIndex += de;
                }

                // memorize last event
                eventBeingLast[oldEventIndex] += 1;
            }
            Assert.AreEqual(GAME_SESSIONS, eventBeingLast.Sum());
        }

        /// <summary>
        /// Test the method Consumer.GetAllEventsOrderedByCount
        /// </summary>
        void TestMethodGetAllEventsOrderedByCountDesc()
        {
            Console.WriteLine("GetAllEventsOrderedByCountDesc");
            var events = consumer.GetAllEventsOrderedByCountDesc().ToList();
            Console.WriteLine("Event Count: {0}", events.Count);

            // test ordering
            int last = GAME_SESSIONS * EVENT_NAMES;
            foreach (var ev in events)
            {
                Assert.IsTrue(last >= ev.Count);
                last = ev.Count;
            }

            // test correct count
            int total = 0;
            foreach (var ev in events)
            {
                int eventIndex;
                Assert.IsTrue(eventIndices.TryGetValue(ev.Name, out eventIndex));
                Assert.AreEqual(eventOccurrences[eventIndex], ev.Count);
                total += ev.Count;
            }

            // test total count
            Assert.AreEqual(eventOccurrences.Sum(), total);
        }

        /// <summary>
        /// Test the method Consumer.GetNextEventsOrderedByCountDesc
        /// </summary>
        void TestMethodGetNextEventsOrderedByCountDesc()
        {
            Console.WriteLine("GetNextEventsOrderedByCountDesc");
            for (var i = 0; i < EVENT_NAMES; ++i)
            {
                var events = consumer.GetNextEventsOrderedByCountDesc(eventNames[i]);

                // test ordering
                int last = GAME_SESSIONS * EVENT_NAMES;
                foreach (var ev in events)
                {
                    Assert.IsTrue(last >= ev.Count);
                    last = ev.Count;
                }

                // test correct count
                int total = 0;
                foreach (var ev in events)
                {
                    int eventIndex;
                    Assert.IsTrue(eventIndices.TryGetValue(ev.Name, out eventIndex));
                    if (eventFollowers[i][eventIndex] != 0)
                    {
                        Assert.AreEqual(eventFollowers[i][eventIndex], ev.Count);
                        total += ev.Count;
                    }
                }

                // test total count
                Assert.AreEqual(eventFollowers[i].Sum(), total);
            }
        }

        /// <summary>
        /// Test the method Consumer.GetLastEventsOrderedByCountDesc
        /// </summary>
        void TestMethodGetLastEventsOrderedByCountDesc()
        {
            Console.WriteLine("GetLastEventsOrderedByCountDesc");
            var events = consumer.GetLastEventsOrderedByCountDesc();

            // test ordering
            int last = GAME_SESSIONS * EVENT_NAMES;
            foreach (var ev in events)
            {
                Assert.IsTrue(last >= ev.Count);
                last = ev.Count;
            }

            // test correct count
            int total = 0;
            foreach (var ev in events)
            {
                int eventIndex;
                Assert.IsTrue(eventIndices.TryGetValue(ev.Name, out eventIndex));
                Assert.AreEqual(eventBeingLast[eventIndex], ev.Count);
                total += ev.Count;
            }

            // test total count
            Assert.AreEqual(eventBeingLast.Sum(), total);
        }

        static void Main(string[] args)
        {
            using (var consumer = new Consumer())
            {
                var p = new Program(consumer);

                p.TestMethodsCreate(); //Aufgabe 1
                p.TestMethodGetAllEventsOrderedByCountDesc(); //Aufgabe 1
                p.TestMethodGetNextEventsOrderedByCountDesc(); //Aufgabe 3
                p.TestMethodGetLastEventsOrderedByCountDesc(); //Aufgabe 4
            }
            Console.WriteLine("Keine Exceptions");
            Console.ReadKey();
        }
    }
}
