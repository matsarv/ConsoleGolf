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
                "\tCourse 1 = 640 meter\n" +
                "\tCourse 2 = 900 meter\n" +
                "\tCourse 3 = 1000 meter\n" +
                "\tCourse 4 = 1500 meter\n" +
                "");

            Console.Write("  Select the course to play? (1,2,3,4), any other key will exit: ");
            char course = Console.ReadKey(true).KeyChar;

            switch (course)
            {
                case '1':
                    Console.Clear();
                    PlaySelectedCourse(640);
                    break;
                case '2':
                    Console.Clear();
                    PlaySelectedCourse(900);
                    break;
                case '3':
                    Console.Clear();
                    PlaySelectedCourse(1000);
                    break;
                case '4':
                    Console.Clear();
                    PlaySelectedCourse(1500);
                    break;
                //case 'q':
                //    DisplayMessage("OK, welcome back to play our course another time.", ConsoleColor.Green);
                //    LeaveGolfCourse();
                //    break;
                default:
                    //DisplayMessage("Sorry, You have entered a wrong key.", ConsoleColor.Red);
                    LeaveGolfCourse();
                    break;
            }
        }

        static void PlaySelectedCourse(double cupDistance)
        {
            double angle = 0;
            double velocity = 0;
            double totalSwingLength = 0;
            double cupDistanceLeft = 0;
            int swingDistanceCount = 0;
            bool stayAlive = true;
            bool startNewPlay = true;

            List<double> swingDistance = new List<double>();

            while (stayAlive)
            {
                if (startNewPlay)
                {
                    swingDistance.Clear();

                    Console.WriteLine("\n  Welcome to Mats Golf course!\n");
                    Console.WriteLine("\nTEE ");
                    Console.WriteLine("Distance of the course is " + cupDistance + " meter.");
                    cupDistanceLeft = cupDistance;
                    startNewPlay = false;
                }
                else
                {
                    angle = CheckGetAngle(); // Get the angle form user
                    velocity = CheckGetVelocity(); // Get velocity form user

                    double bollDistance = Math.Abs(CalculateBollDistance(angle, velocity)); // Calculate swing distance 

                    swingDistance.Add(bollDistance); // Save distance in list
                    swingDistanceCount = swingDistance.Count; //Count swing

                    Console.WriteLine("\nSCORECARD ");
                    Console.WriteLine("Swing: " + swingDistanceCount); // Count number of swings
                    Console.WriteLine("Swing length: " + bollDistance + " meter."); //Lenght of swing

                    totalSwingLength = 0;

                    foreach (double distance in swingDistance)
                    {
                        totalSwingLength = totalSwingLength + distance;
                    }

                    cupDistanceLeft = Math.Abs(Math.Round(bollDistance - cupDistanceLeft, 2)); //Distance left, 2 digits

                    Console.WriteLine("Total swing length: " + totalSwingLength + " meter.");
                    Console.WriteLine("Length left to cup: " + cupDistanceLeft + " meter.");

                    // Ball in the cup
                    if (Math.Abs(cupDistanceLeft) < 0.2) //  
                    {
                        Console.WriteLine("\nCONGRATULATION\nYour ball is in the cup!\n");
                        for (int i = 0; i < swingDistance.Count; i++)
                        {
                            Console.WriteLine($"Swing {i + 1} of {swingDistanceCount} = {swingDistance[i]} meter of total {totalSwingLength} meters.");
                        }
                        Console.WriteLine("\nCOURSE FINISHED\n\nPress any key to continue...");
                        Console.ReadKey();
                        stayAlive = false;
                    }

                    // Reached 10 swings
                    if (swingDistanceCount == 10)
                    {
                        DisplayMessage("\nSorry!" +
                            "\nYou have reach 10 swings!", ConsoleColor.Yellow);
                        stayAlive = false;
                    }

                    // Swing to ling
                    if ((cupDistance - cupDistanceLeft) < 0)
                    {
                        DisplayMessage("\nSorry!\nYou passed the cup to far away!", ConsoleColor.Yellow);
                        stayAlive = false;
                    }

                }
            }

            LeaveGolfCourse();
        }
        
        // Display message with color
        static void DisplayMessage(string message, ConsoleColor color = ConsoleColor.White)
        {
            Console.ForegroundColor = color;
            Console.WriteLine("\n\n  " + message + "\n");
            Console.ResetColor();
            Console.WriteLine("\n\n  Press any key to continue...\n");
            Console.ReadKey();
        }

        // End text
        static void LeaveGolfCourse()
        {
            Console.Clear();
            Console.WriteLine("\n  Time for a Martini in the bar!\n\n  Bye, bye!");
            Console.ReadKey();
        }

        // Get and return velocity
        static double CheckGetVelocity()
        {
            bool stayAlive = true;
            int catchloop = 0;
            do
            {
                double velocity = 0;
                velocity = AskForNumber("\nEnter your velocity? (m/s) ");

                if (velocity <= 0 || velocity > 101) 
                {
                    Console.WriteLine("Please enter a number greater than 0 or lower than 101. The World record is 101 m/s. Try again.");
                    Console.ReadKey();
                }
                else
                {
                    return velocity;
                }
            } while (stayAlive);

            return catchloop;
        }

        // Get and return angle
        static double CheckGetAngle()
        {
            bool stayAlive = true;
            int catchloop = 0;

            do
            {
                double angle = 0;
                angle = AskForNumber("\nEnter your angle? (*) ");

                if (angle <= 0 || angle >= 90)
                {
                    Console.WriteLine("Please use a number above 0 and below 90 degrees.");
                    Console.ReadKey();
                }
                else
                {
                    return angle;
                }
            } while (stayAlive);

            return catchloop;
        }

        // Calculate distance from angle and velocity
        static double CalculateBollDistance(double angle, double velocity)
        {
            double gravity = 9.8;
            double angleInRadians = (Math.PI / 180) * angle;
            double Distance = Math.Pow(velocity, 2) / gravity * Math.Sin(2 * angleInRadians);

            return Math.Round(Distance, 2);
        }

        // Ask for a number with exceptions.
        // Used in CheckGetVelocity() and CheckGetAngle()
        static double AskForNumber(string inputSentence)
        {
            double number = 0;
            bool missingSentence = true;

            do
            {
                Console.Write(inputSentence);
                try
                {
                    number = Convert.ToDouble(Console.ReadLine());
                    missingSentence = false;
                }
                catch (FormatException)
                {
                    Console.WriteLine("Not a number, try again.");
                }
                catch (OverflowException)
                {
                    Console.WriteLine("Your number is to big, try again.");
                }

            } while (missingSentence);

            return number;
        }

    }

}
