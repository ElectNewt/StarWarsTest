using Shared.ROP;
using Shared.Utils.Types;
using StarWars.Model;
using StarWars.Service.Consumables;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace StarWars.Service
{
    public interface ICalculateStarshipsStopsDependencies
    {
        Task<ReadOnlyCollection<Starship>> GetAllStarShips();
    }

    public class CalculateStarshipsStops
    {
        private readonly ICalculateStarshipsStopsDependencies _dependencies;
        public CalculateStarshipsStops(ICalculateStarshipsStopsDependencies dependencies)
        {
            _dependencies = dependencies;
        }

        public async Task<Result<ReadOnlyCollection<StarshipStops>>> GetShipsWithStopsByMGLT(long totalDistance)
        {
            return await GetAllShips()
                .Bind(DiscardUnknownMglt)
                .Bind(x => CalculateStopsForDistancePerShip(x, totalDistance))
                .Bind(DiscardNulls)
                .Bind(MapToStarShipStops);
        }

        private async Task<Result<ReadOnlyCollection<Starship>>> GetAllShips() =>
            await _dependencies.GetAllStarShips();

        private Task<Result<ReadOnlyCollection<Starship>>> DiscardUnknownMglt(ReadOnlyCollection<Starship> starships) =>
            starships.Where(a => IntUtils.IsInt(a.MGLT)).ToList().AsReadOnly().Success().Async();

        /// <summary>
        /// THis method calculates the number of stops for each ship.
        /// if the consumable has a non valid value, it returns null. (to be discarded) 
        /// </summary>
        private Task<Result<IEnumerable<(Starship, int)?>>> CalculateStopsForDistancePerShip(ReadOnlyCollection<Starship> starships, long totalDistance)
        {
            return starships.Select(CalculateStops).Success().Async();

            (Starship, int)? CalculateStops(Starship starShip)
            {
                int? consumableDurationHours = ConsumableMapper.GetConsumablesDurationInHours(starShip.consumables);

                if (!consumableDurationHours.HasValue)
                    return null;

                decimal tripTotalHours = CalculateNeededhoursForTheTrip(starShip.MGLT, totalDistance);

                int numberOfStops = (int)Math.Floor(tripTotalHours / consumableDurationHours.Value);

                return (starShip, numberOfStops);
            };
        }

        private decimal CalculateNeededhoursForTheTrip(string mgltPerHour, long totaldistance)
        {
            int mglt = Convert.ToInt32(mgltPerHour);
            return totaldistance / mglt;
        }

        private Task<Result<IEnumerable<(Starship, int)>>> DiscardNulls(IEnumerable<(Starship, int)?> obj)
         => obj.Where(a => a.HasValue).Cast<(Starship, int)>().Success().Async();

        private Task<Result<ReadOnlyCollection<StarshipStops>>> MapToStarShipStops(IEnumerable<(Starship, int)> obj)
            => obj.Select(a => new StarshipStops(a.Item1.name, a.Item2)).ToList().AsReadOnly().Success().Async();
    }
}
