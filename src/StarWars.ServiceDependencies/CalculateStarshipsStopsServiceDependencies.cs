using StarWars.Data.External;
using StarWars.Model;
using StarWars.Service;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace StarWars.ServiceDependencies
{
    public class CalculateStarshipsStopsServiceDependencies : ICalculateStarshipsStopsDependencies
    {
        private readonly SwApi<Starship> _starshipApi;

        public CalculateStarshipsStopsServiceDependencies(SwApi<Starship> starshipApi)
        {
            _starshipApi = starshipApi;
        }

        public async Task<ReadOnlyCollection<Starship>> GetAllStarShips()
        {
            return await _starshipApi.GetAllItems();
        }
    }
}
