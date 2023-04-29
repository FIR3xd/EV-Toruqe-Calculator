using System;
using System.Globalization;

namespace EV_Power_Calculation
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            string version = "0.0.2";
            
            Console.Clear();
            Console.WriteLine("EV Torque Calculator                                           Version " + version);
            Console.WriteLine("____________________________________________________________________________");
            string cont = "y";
            while (cont == "y")
            {
                Console.Write("Choose a power measurement [KW / hp]: ");
                string measurement = Console.ReadLine();
                bool convertToKw = false;

                if (measurement.ToLower() == "hp") {convertToKw = true;}

                Console.Write("Enter the desired power: ");
                double power = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                double powerInKw = convertToKw ? power * 0.7457 : power;

                Console.Write("Enter the maximum torque (in Nm): ");
                double maxTorque = Convert.ToDouble(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Enter Maximum RPM: ");
                int maxrpm = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);
                Console.Write("Enter the torque curve precision (500 = Standard) [1-5000]: ");
                int curvequality = Convert.ToInt32(Console.ReadLine(), CultureInfo.InvariantCulture);

                Console.WriteLine("___________________________________");
                Console.WriteLine("[\"rpm\", \"torque\"]");
                for (int rpm = 0; rpm <= maxrpm; rpm += curvequality)
                {
                    double torque = powerInKw * 9549.297 / rpm;
                    if (torque > maxTorque) {torque = maxTorque;}
                    Console.WriteLine($"[{rpm}, {torque.ToString("0.0000", CultureInfo.InvariantCulture)}]");
                }
                
                Console.WriteLine("___________________________________");
                Console.WriteLine("Do you want to calculate again? [y/n]");
                cont = Console.ReadLine();
            }
        }   
    }
}