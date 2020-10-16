using Microsoft.Extensions.DependencyInjection;
using Shared.ROP;
using Shared.Utils.Serialization;
using StarWars.Data.External;
using StarWars.Model;
using StarWars.Service;
using StarWars.ServiceDependencies;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StarWars
{
    class Program
    {
        static async Task Main()
        {
            ServiceProvider provider = BuildServiceProvider();

            var service = provider
                    .GetService<CalculateStarshipsStops>();

            var distance = AskDistance();

            await service.GetShipsWithStopsByMGLT(distance)
                .Bind(PrintInformation);
        }


        /// <summary>
        /// Prints the .ToString() of T
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objList"></param>
        private static Task<Result<Unit>> PrintInformation<T>(ReadOnlyCollection<T> objList)
        {
            foreach (var obj in objList)
            {
                Console.WriteLine(obj);
            }
            return Result.Unit.Success().Async();
        }

        /// <summary>
        /// It will request to the user to enter in the command line the distance as a long.
        /// </summary>
        /// <returns>distance entered by the user</returns>
        public static long AskDistance()
        {
            long? distance = null;
            Misc.Draw.PrintYoda();
            Console.WriteLine("the distance to your destiny you have to indicate (in MGLT): ");
            do
            {
                
                string data = Console.ReadLine();

                if (Shared.Utils.Types.IntUtils.IsInt64(data))
                {
                    distance = Convert.ToInt64(data.Trim());
                }
                else
                {
                    Console.WriteLine("Repeat you must, as a number was not entered (in MGLT): ");
                }
            } while (distance == null);
            Console.WriteLine("getting ready the data is...");
            return distance.Value;
        }


        /// <summary>
        /// Build the service provider for the DI
        /// </summary>
        /// <returns></returns>
        private static ServiceProvider BuildServiceProvider()
        {
            return new ServiceCollection()
                .AddScoped<ISerializer, Serializer>()
                .AddScoped<CalculateStarshipsStops>()
                .AddScoped<SwApi<Starship>>()
                .AddScoped<ICalculateStarshipsStopsDependencies, CalculateStarshipsStopsServiceDependencies>()
                .BuildServiceProvider();
        }


    }
}
