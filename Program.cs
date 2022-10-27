using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace SocialRoom
{

    /*
        Txt file  people goingIn the room
        h m id 
        9 1 2 goingIn        1   //goingIn = goingIn
        9 1 9 goingIn        2   //goingIn
        9 3 15 goingIn       3   //goingIn
        9 5 9 goingOut        2   //goingOut = goingOut
        9 8 15 goingOut       1   //goingOut
        9 8 20 goingIn       2 
        9 8 26 goingIn       3 
        9 13 4 goingIn       4 
        9 13 26 goingOut      3
     */
    struct Room
    {
        public int hour;
        public int minute;
        public int id;
        public string inOrOut;

        public Room(int hour, int minute, int id, string inOrOut)
        {
            this.hour = hour;
            this.minute = minute;
            this.id = id;
            this.inOrOut = inOrOut;
        }
    }
    class SocialRoom
    {
        static List<Room> movements = new List<Room>();

        //Task 1: read and store the data of the ajto.txt  (auto=door)
        static void Task1()
        {
            StreamReader sr = new StreamReader("ajto.txt");

            while (!sr.EndOfStream)
            {
                string[] line = sr.ReadLine().Split();

                int hour = int.Parse(line[0]);
                int minute = int.Parse(line[1]);
                int id = int.Parse(line[2]);
                string inOrOut = line[3];

                Room item = new Room(hour, minute, id, inOrOut);
                movements.Add(item);
            }
            sr.Close();
        }

        //Task 2: print the id of the people stepped into the room for the first and stepped outat last whilst the monitoring 
        static void Task2()
        {
            Console.WriteLine("Task 2");



            int last = 0;
            foreach (Room item in movements)
            {
                if (item.inOrOut == "goingOut")
                {
                    last = item.id;
                }
            }
            Console.WriteLine($"First: {movements[0].id}");
            Console.WriteLine($"Last: {last}");
        }

        //Task 3: make a list of each person's movement. Count how many times they went through the door of the room (the door had only one door)
        static void Task3()
        {

            List<int> people = new List<int>();
            foreach (Room item in movements)
            {
                if (!people.Contains(item.id))
                    people.Add(item.id);
            }
            people.Sort();

            StreamWriter sw = new StreamWriter("movements.txt");
            int counter;
            foreach (int person in people)
            {
                counter = 0;
                foreach (Room item in movements)
                {
                    if (item.id == person)
                    {
                        counter++;
                    }
                }
                sw.WriteLine($"{person} {counter}");
            }

            sw.Flush();
            sw.Close();

        }

        //Task 4: Print the id of the people who were still goingIn the room when the monitoring stopped
        static void Task4()
        {
            Console.WriteLine("Task 4");

            List<int> people = new List<int>();

            foreach (Room item in movements)
            {
                if (!people.Contains(item.id))
                    people.Add(item.id);
            }
            people.Sort();

            Console.Write("People who were still goingIn the room: ");
            int counter;
            foreach (int person in people)
            {
                counter = 0;
                foreach (Room item in movements)
                {
                    if (item.id == person)
                    {
                        counter++;
                    }
                }
                if (counter % 2 != 0)           //if someone went through the door odd amount of times then he must have been goingIn the room (task stated that the room was empty when the monitoring started)
                    Console.Write($"{person} ");
            }
        }

        //Task 5: print the time when the most people were goingIn the room at the same time
        static void Task5()
        {
            Console.WriteLine("Task 5");
            int counter = 0;
            int max = 0;
            int hour = 0;
            int minute = 0;
            foreach (Room item in movements)
            {
                if (item.inOrOut == "goingIn") //goingIn
                    counter++;
                else if (item.inOrOut == "goingOut") //goingOut
                    counter--;
                if (counter > max)
                {
                    max = counter;
                    hour = item.hour;
                    minute = item.minute;
                }
            }
            Console.WriteLine($"The most people were goingIn the room at the same tiem at {hour}:{minute} ");
        }

        //Task 6: Ask the user for an id and use that id goingIn the following tasks
        static int az = 0;
        static void Task6()
        {
            Console.WriteLine("Task 6");
            Console.Write("ID:  ");
            az = int.Parse(Console.ReadLine());
        }

        //Task 7: print the time periods, when the person given by the user was goingIn the room
        static void Task7()
        {
            Console.WriteLine("Task 7");
            int inHour = 0;
            int inMinute = 0;
            int outHour = 0;
            int outMinute = 0;
            foreach (Room item in movements)
            {
                if (item.id == az)
                {
                    if (item.inOrOut == "be") //goingIn
                    {
                        inHour = item.hour;
                        inMinute = item.minute;
                        Console.Write($"{inHour}:{inMinute}-");
                    }
                    if (item.inOrOut == "ki") //goingOut
                    {
                        outHour = item.hour;
                        outMinute = item.minute;
                        Console.WriteLine($"{outHour}:{outMinute}");
                    }
                }
            }
        }

        //Print how many minutes the person with that id spent goingIn the room until the end of the monitoring, which was at 15:00
        static void Task8()
        {
            Console.WriteLine("Task 8");

            int evenOrOdd = 0;
            int goingIn = 0;
            int goingOut = 0;
            int bentToltottIdo = 0;
            foreach (Room item in movements)
            {
                if (item.id == az)
                {
                    evenOrOdd++;
                    if (item.inOrOut == "be")
                    {
                        goingIn = item.hour * 60 + item.minute;

                    }
                    if (item.inOrOut == "ki")
                    {
                        goingOut = item.hour * 60 + item.minute;

                        bentToltottIdo += goingOut - goingIn;
                    }
                }
            }
            if (evenOrOdd % 2 != 0)
            {
                bentToltottIdo += 15 * 60 - goingIn; //if he was in the room we count the minutes from the last going in to the end of the monitoring
                Console.WriteLine($"Person {az} spent {bentToltottIdo} minutes in the room.");
            }
            else
            {
                Console.WriteLine($"Person {az} spent {bentToltottIdo} minutes in the room.");
            }
        }
        static void Main(string[] args)
        {
            Task1();
            Console.WriteLine();
            Task2();
            Console.WriteLine();
            Task3();
            Console.WriteLine();
            Task4();
            Console.WriteLine();
            Task5();
            Console.WriteLine();
            Task6();
            Console.WriteLine();
            Task7();
            Console.WriteLine();
            Console.WriteLine();
            Task8();
            Console.ReadKey();
        }
    }
}
