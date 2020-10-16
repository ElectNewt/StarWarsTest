using System;
using System.Text.RegularExpressions;

namespace StarWars.Service.Consumables
{
    public static class ConsumableMapper
    {
        public const int NumberOfHoursInADay = 24;
        public const int NumberOfDaysInAWeek = 7;
        public const int NumberOfDaysInAMonth = 30;
        public const int NumberOfDaysInAyear = 365;

        /// <summary>
        /// receive the information of the consumables and it parsed it to hours
        /// returns number of hours the consumables will last or null if cannot find a match.
        /// </summary>
        /// <param name="consumables"></param>
        /// <returns></returns>
        public static int? GetConsumablesDurationInHours(string consumables)
        {
            Match resultMatchCase = Regex.Match(consumables, @"^(\d*) (\D*)", RegexOptions.IgnoreCase);

            if (!resultMatchCase.Success)
                return null;

            int times = Convert.ToInt32(resultMatchCase.Groups[1].Value);
            int duration = MapDurationToHours(resultMatchCase.Groups[2].Value);

            return times * duration;
        }

        private static int MapDurationToHours(string duration)
        {
            return duration.ToLower() switch
            {
                "day" => NumberOfHoursInADay,
                "days" => NumberOfHoursInADay,
                "week" => NumberOfDaysInAWeek * NumberOfHoursInADay,
                "weeks" => NumberOfDaysInAWeek * NumberOfHoursInADay,
                "month" => NumberOfDaysInAMonth * NumberOfHoursInADay,
                "months" => NumberOfDaysInAMonth * NumberOfHoursInADay,
                "year" => NumberOfDaysInAyear * NumberOfHoursInADay,
                "years" => NumberOfDaysInAyear * NumberOfHoursInADay,
                _ => throw new NotSupportedException(),
            };
        }

    }
}
