using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ServiceModel;
using System.ServiceModel.Description;
using WcfServices.Contracts;
using WcfServices.Services;

namespace WcfServices.Hosting
{
    class Program
    {
        static void Main(string[] args)
        {
            /**
             * Create a host to provide service.
             * ServiceType is HelloWorld
             * BaseAddress is in localhost
             */
            using (ServiceHost host = new ServiceHost(typeof(CalculatorService), new Uri("http://localhost:8080/calculatorservice")))
            {
                /**
                * Add an serviceEndpoint to this host
                * Implemented Contract is ICalculator
                * Binding pattern is BasicHttpBinding
                * Address  'SVC' is a relative address
                */
                host.AddServiceEndpoint(typeof(ICalculator), new BasicHttpBinding(), "SVC");
                if (host.Description.Behaviors.Find<ServiceMetadataBehavior>() == null)
                {
                    ServiceMetadataBehavior behavior = new ServiceMetadataBehavior();
                    behavior.HttpGetEnabled = true;
                    behavior.HttpGetUrl = new Uri("http://localhost:8080/calculatorservice");
                    host.Description.Behaviors.Add(behavior);
                }
                host.Opened += delegate
                {
                    Console.WriteLine("CalculaorService have started, press any key to stop it!");
                };

                host.Open();
                Console.Read();
            }
        }
    }
}
