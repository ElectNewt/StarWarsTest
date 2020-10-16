using Moq;
using StarWars.Model;
using StarWars.Service;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using Shared.ROP;

namespace StarWars.UnitTest.Services
{
    public class TestCalculateStarshipsStops
    {

        public class TestState
        {
            public Mock<ICalculateStarshipsStopsDependencies> Dependencies { get; set; }
            public CalculateStarshipsStops Subject { get; set; }
        }


        private TestState BuildTest()
        {
            Mock<ICalculateStarshipsStopsDependencies> dependencies = new Mock<ICalculateStarshipsStopsDependencies>();

            dependencies.Setup(a => a.GetAllStarShips()).Returns(Task.FromResult(BuildFakeStarships()));

            CalculateStarshipsStops subject = new CalculateStarshipsStops(dependencies.Object);

            return new TestState()
            {
                Dependencies = dependencies,
                Subject = subject
            };

            ReadOnlyCollection<Starship> BuildFakeStarships()
            {
                return new List<Starship>()
                {
                    new Starship{consumables = "1 week", MGLT = "80", name = "Y-wing" },
                    new Starship{consumables = "2 months", MGLT = "75", name = "Millennium Falcon" },
                    new Starship{consumables = "6 months", MGLT = "20", name = "Rebel transport" },
                }.AsReadOnly();
            }
        }



        [Fact]
        public async Task Test_CalculateStarshipsStops_With_ValidValues()
        {
            TestState state = BuildTest();

            var result = await state.Subject.GetShipsWithStopsByMGLT(1000000);

            Assert.True(result.Success);
            var ships = result.Throw();

            StarshipStops yWing = ships.First(a => a.Name == "Y-wing");
            StarshipStops falcon = ships.First(a => a.Name == "Millennium Falcon");
            StarshipStops rebelTransport = ships.First(a => a.Name == "Rebel transport");

            Assert.Equal(74, yWing.NumberOfStops);
            Assert.Equal(9, falcon.NumberOfStops);
            Assert.Equal(11, rebelTransport.NumberOfStops);
        }


        [Fact]
        public async Task Test_CalculateStarshipsStops_With_InvalidValues_Then_Noships()
        {
            TestState state = BuildTest();
            state.Dependencies.Setup(a => a.GetAllStarShips())
                .Returns(Task.FromResult(new List<Starship>()
                {
                    new Starship{consumables = "lot of months", MGLT = "5", name = "fakeName1" },
                    new Starship{consumables = "2 months", MGLT = "test", name = "FakeNAmeTwo" },
                }.AsReadOnly()));


            var result = await state.Subject.GetShipsWithStopsByMGLT(1000000);

            Assert.True(result.Success);
            var ships = result.Throw();

            Assert.Empty(ships);
        }



    }
}
