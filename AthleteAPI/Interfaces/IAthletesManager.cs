using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthleteLibrary;

namespace AthleteAPI.Interfaces
{
    public interface IAthletesManager
    {
        IEnumerable<Athlete> Get();
        Athlete Get(int id);
        bool Create(Athlete athlete);
        Athlete Delete(int id);

        IEnumerable<Athlete> FilterByName(string name);
        IEnumerable<Athlete> FilterByCountry(string country);
        IEnumerable<Athlete> FilterByHeight(int minHeight, int maxHeight);
    }
}
