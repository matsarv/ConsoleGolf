using System;
using System.Collections.Generic;

namespace ConsoleGolf
{
    class Program
    {
        static void Main(string[] args)
        {

            Console.WriteLine("\n  Welcome to Mats Golf course!");
            QuestionCourse();
        }

        static void QuestionCourse()
        {
            Console.WriteLine
                ("\n" +
                "\tCourse 1 = 319,5 meter\n" +
                "\tCourse 2 = 640 meter\n" +
                "\tCourse 3 = 1000 meter\n" +
                "\tCourse 4 = 1500 meter\n" +
                "");

            Console.Write("  Select the course to play? (1,2,3,4): ");
            int course = int.Parse(Console.ReadLine());

            switch (course)
            {
                case 1:
                    Console.Clear();
                    QuestionsAndAnswers(319.5);
                    break;
                case 2:
                    Console.Clear();
                    QuestionsAndAnswers(640);
                    break;
                case 3:
                    Console.Clear();
                    QuestionsAndAnswers(1000);
                    break;
                case 4:
                    Console.Clear();
                    QuestionsAndAnswers(1500);
                    break;
                default:

                    break;
            }
            Console.Clear();
        }

        static void QuestionsAndAnswers(double cupDistance)
        {
            double angle = 0;
            double velocity = 0;
            double totalSwingLength = 0;
            //double cupDistance = 0;
            double cupDistanceLeft = 0;
            int swingDistanceCount = 0;
            bool stayAlive = true;
            bool startNewPlay = true;

            List<double> swingDistance = new List<double>();
            //Console.WriteLine(course);
            while (stayAlive)
            {
                if (startNewPlay)
                {
                    swingDistance.Clear();

                    Console.WriteLine("\n  Welcome to Mats Golf course!\n");
                    Console.WriteLine("\nTEEE ");
                    Console.WriteLine("Distance of the course is " + cupDistance + " meter.");
                    //Console.Write("Distance of the course? "); // Distance to cup
                    //cupDistance = double.Parse(Console.ReadLine());
                    
                    startNewPlay = false;
                }
                else
                {
                    angle = CheckGetAngle(); // Get the angle form user
                    velocity = CheckGetVelocity(); // Get velocity form user

                    double bollDistance = CalculateBollDistance(angle, velocity); // Swing distance 

                    swingDistance.Add(bollDistance); // Save distance
                    swingDistanceCount = swingDistance.Count;

                    Console.WriteLine("\nSCORECARD ");
                    Console.WriteLine("Swing: " + swingDistanceCount); // Count number of swings
                    Console.WriteLine("Swing length: " + bollDistance + " meter."); //Fairway

                    totalSwingLength = 0;

                    foreach (double distance in swingDistance)
                    {
                        //Console.WriteLine(dist);
                        totalSwingLength = totalSwingLength + distance;
                        //Console.WriteLine(swingDistance.Count);
                        cupDistanceLeft = cupDistance - totalSwingLength;
                    }

                    Console.WriteLine("Total swing length: " + totalSwingLength + " meter.");
                    Console.WriteLine("Length left to cup: " + Math.Abs(cupDistanceLeft) + " meter.");
                    //Console.WriteLine("Length left: " + Math.Abs(cupDistance - totalSwingLength) + " meter.");
                    //Console.WriteLine("Length left: " + (cupDistance - totalSwingLength) + " meter.");

                    if (Math.Abs(cupDistanceLeft) < 1) //  
                    {
                        Console.WriteLine("\nCOURSE FINISHED\nCongratulation, your ball is in the cup!");
                        for (int i = 0; i < swingDistance.Count; i++)
                        {
                            Console.WriteLine($"Swing {i + 1} = {swingDistance[i]} meter.");
                        }
                        Console.ReadKey();
                        stayAlive = false;
                    }

                    if (swingDistanceCount == 10)
                    {
                        Console.WriteLine("\nYou have reach 10 swings, time to move on to next course!");
                        Console.ReadKey();
                        stayAlive = false;
                    }

                    if (cupDistanceLeft < -100)
                    {
                        Console.WriteLine("\nYou past the cup to far away, over 100 meters!");
                        Console.ReadKey();
                        stayAlive = false;
                    }
                    //totalSwingLength = 0;
                }
            }
            Console.Clear();
            Console.WriteLine("\n  Welcome to Mats Golf course!\n");
            Console.WriteLine("\nTime for a Martini in the bar!\nBye, bye!");
            Console.ReadKey();
        }

        static double CheckGetVelocity()
        {
            Console.Write("Enter your velocity? (m/s) ");
            double velocity = double.Parse(Console.ReadLine());
            if (velocity <= 0 || velocity > 101)
            {
                Console.WriteLine("Error! World record is 101 m/s.");
                Console.ReadKey();
                return CheckGetVelocity();
            }
            else
            {
                return velocity;
            }
        }

        static double CheckGetAngle()
            {
            Console.Write("\nEnter your angle? (*) ");
            double angle = double.Parse(Console.ReadLine());
            if (angle < 0 || angle >= 90)
            {
                Console.WriteLine("Error! Please use between 0 and 90 degrees."); 
                Console.ReadKey();
                return CheckGetAngle();
            }
            else
            {
                return angle;
            }
        }

        static double CalculateBollDistance(double angle, double velocity)
        {
            double gravity = 9.8;
            double angleInRadians = (Math.PI / 180) * angle;
            double Distance = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * angleInRadians);

            return Math.Round(Distance,2);
        }
    }

}
