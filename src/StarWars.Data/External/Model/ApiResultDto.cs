using System.Collections.Generic;

namespace StarWars.Data.External.Model
{
    public class ApiResultDto<T>
    {
        public int count { get; set; }
        public string next { get; set; }
        public string previous { get; set; }
        public List<T> results { get; set; }
    }
}
