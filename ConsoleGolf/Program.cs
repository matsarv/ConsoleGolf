using System;
using System.Collections.Generic;

namespace ConsoleGolf
{
    class Program
    {
        static void Main(string[] args)
        {
            
            double angle = 0;
            double velocity = 0;

            List<double> distance = new List<double>();

            Console.Write("Distance to cup? ");
            int cup = int.Parse(Console.ReadLine());

            // Loop 
            angle = CheckGetAngle();
            velocity = CheckGetVelocity();
            

            // count swing / save distance
            double bollDistance = CalculateBollDistance(angle, velocity);

            distance.Add(bollDistance);
            distance.Add(bollDistance);
            distance.Add(bollDistance);

            //Console.WriteLine(angle);
            //Console.WriteLine(bollDistance);
            Console.WriteLine(distance.Count + " " );



            for (int i = 0; i < distance.Count; i++)
            {
                Console.WriteLine($"{i} = {distance[i]}");

            }

            Console.ReadKey();


        }


        static double CheckGetVelocity()
        {
            Console.Write("Enter your velocity? ");
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
            Console.Write("Enter your angle? ");
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
