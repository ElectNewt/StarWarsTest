namespace StarWars.Model
{
    public struct StarshipStops
    {
        public readonly string Name;
        public readonly int NumberOfStops;

        public StarshipStops(string name, int numberOfStops)
        {
            Name = name;
            NumberOfStops = numberOfStops;
        }

        public override string ToString()
        {
            return $"{Name}: {NumberOfStops}";
        }
    }
}
