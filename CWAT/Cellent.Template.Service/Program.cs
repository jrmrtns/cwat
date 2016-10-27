using System;
using System.Linq;
using System.ServiceProcess;

namespace Cellent.Template.Service
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        private static void Main(string[] args)
        {
            if (args.Length >= 1 && args.Contains("/c"))
            {
                Service svc = new Service();
                svc.StartService(args);
                Console.ReadLine();
                svc.StopService();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new Service()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}