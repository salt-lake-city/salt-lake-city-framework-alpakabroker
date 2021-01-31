using System;
using Microsoft.Extensions.DependencyInjection;
using SaltLakeCity.Framework.Alpakabroker.Config;

namespace SaltLakeCity.Framework.Alpakabroker.Example.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            var sc = new ServiceCollection();

            sc.ConfigureAlpakaBroker();
            
            
            Console.WriteLine("Hello World!");
        }
    }
}