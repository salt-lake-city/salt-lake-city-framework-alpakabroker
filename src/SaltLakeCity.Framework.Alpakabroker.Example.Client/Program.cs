using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SaltLakeCity.Framework.Alpakabroker.Config;
using SaltLakeCity.Framework.Alpakabroker.Registry;
using SaltLakeCity.Framework.Core;

namespace SaltLakeCity.Framework.Alpakabroker.Example.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var sc = new ServiceCollection();

            AlpakaEventReceiverLocator.Locate(new AssemblyProxy(Assembly.GetExecutingAssembly()), sc);
            
            AlpakaEventRegistry.Init(sc.BuildServiceProvider());
            Console.WriteLine("Hello World!");
        }
    }
}