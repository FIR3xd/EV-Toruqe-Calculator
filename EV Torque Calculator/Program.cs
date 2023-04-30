using System;
using System.Globalization;

namespace EV_Power_Calculation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string version = "30.4.2023Dev";

            Console.WriteLine($"EV Torque Calculator\nVersion {version}");
            Console.WriteLine("___________________________________");

            for (string cont = "y"; cont == "y";)
            {
                Console.Write("Choose a power measurement [KW / hp]: ");
                string measurement = Console.ReadLine().ToLower();

                bool convertToKw = false;
                switch (measurement)
                {
                    case "hp":
                        convertToKw = true;
                        break;
                    case "kw":
                        break;
                    default:
                        Console.WriteLine("Invalid input.");
                        continue;
                }

                Console.Write("Enter the desired power: ");
                double power;
                while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out power))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                double powerInKw = convertToKw ? power * 0.7457 : power;

                Console.Write("Enter the maximum torque (in Nm): ");
                double maxTorque;
                while (!double.TryParse(Console.ReadLine(), NumberStyles.Float, CultureInfo.InvariantCulture, out maxTorque))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.Write("Enter Maximum RPM: ");
                int maxrpm;
                while (!int.TryParse(Console.ReadLine(), out maxrpm))
                {
                    Console.WriteLine("Invalid input. Please enter a valid number.");
                }

                Console.Write("Enter the torque curve precision (500 = Standard) [1-5000]: ");
                int curvequality;
                while (!int.TryParse(Console.ReadLine(), out curvequality) || curvequality < 1 || curvequality > 5000)
                {
                    Console.WriteLine("Invalid input. Please enter a number between 1 and 5000.");
                }

                Console.WriteLine("___________________________________");
                Console.WriteLine("[\"rpm\", \"torque\"]");
                for (int rpm = 0; rpm <= maxrpm; rpm += curvequality)
                {
                    double torque = powerInKw * 9549.297 / rpm;
                    if (torque > maxTorque) {torque = maxTorque;}
                    Console.WriteLine($"[{rpm}, {torque.ToString("0.0000", CultureInfo.InvariantCulture)}]");
                }

                Console.WriteLine("___________________________________");
                Console.Write("Do you want to calculate again? [y/n]");
                cont = Console.ReadLine().ToLower();
            }

        }   
    }
}