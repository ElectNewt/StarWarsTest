using Microsoft.Extensions.DependencyInjection;
using Shared.ROP;
using Shared.Utils.Serialization;
using StarWars.Data.External;
using StarWars.Model;
using StarWars.Service;
using StarWars.ServiceDependencies;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace StarWars.IntegrationTest
{
    public class FullTestCalculateStarshipsStops
    {
        [Fact]
        public async Task Test_FullProcess_IncludingApiCall()
        {
            ServiceProvider serviceProvider = BuildServiceProvider();

            var service = serviceProvider
                   .GetService<CalculateStarshipsStops>();

            var result = await service.GetShipsWithStopsByMGLT(1000000);

            Assert.True(result.Success);
            var ships = result.Throw();

            StarshipStops yWing = ships.First(a => a.Name == "Y-wing");
            StarshipStops falcon = ships.First(a => a.Name == "Millennium Falcon");
            StarshipStops rebelTransport = ships.First(a => a.Name == "Rebel transport");

            Assert.Equal(74, yWing.NumberOfStops);
            Assert.Equal(9, falcon.NumberOfStops);
            Assert.Equal(11, rebelTransport.NumberOfStops);

        }


        private ServiceProvider BuildServiceProvider()
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
