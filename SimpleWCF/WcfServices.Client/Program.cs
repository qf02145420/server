using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WcfServices.Client.CalculatorService;

namespace WcfServices.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            using (CalculatorClient proxy = new CalculatorClient())
            {
                Console.WriteLine("x + y = {2} when x = {0} and y = {1}", 1, 2, proxy.Add(1, 2));
                Console.Read();
            }
        }
    }
}
