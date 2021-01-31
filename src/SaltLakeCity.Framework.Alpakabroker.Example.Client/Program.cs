using System;
using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using SaltLakeCity.Framework.Alpakabroker.Config;
using SaltLakeCity.Framework.Alpakabroker.Example.Events.Connection;
using SaltLakeCity.Framework.Core;

namespace SaltLakeCity.Framework.Alpakabroker.Example.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var sp = new ServiceCollection()
                .ConfigureAlpakaBroker()
                .ConfigureAlpakaBrokerForAssembly(new AssemblyProxy(Assembly.GetExecutingAssembly()))
                .BuildServiceProvider()
                .ConfigureAlpakaBroker();
            
            
            ConnectionTimeoutEventEmitter.EmitConnectionTimeoutEvent(new ConnectionTimeoutEvent());
            Console.WriteLine("Hello World!");
        }
    }
}