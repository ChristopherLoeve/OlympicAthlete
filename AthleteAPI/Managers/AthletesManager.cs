using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AthleteAPI.Interfaces;
using AthleteLibrary;

namespace AthleteAPI.Managers
{
    public class AthletesManager : IAthletesManager
    {
        private static List<Athlete> _athletes;
        private static int _nextId = 8;

        public AthletesManager()
        {
            _athletes ??= new List<Athlete>()
            {
                new Athlete(1, "Henrik", "Denmark", 155.2),
                new Athlete(2, "Steven", "United Kingdom", 185.5),
                new Athlete(3, "Mikkel", "Greece", 183),
                new Athlete(4, "Oscar", "United States", 194.6),
                new Athlete(5, "Peter", "Greenland", 175.5),
                new Athlete(6, "Patrick", "China", 182),
                new Athlete(7, "Christopher", "Denmark", 183)
            };
        }

        public IEnumerable<Athlete> Get()
        {
            return _athletes;
        }

        public Athlete Get(int id)
        {
            var athlete = _athletes.Find(a => a.Id == id);
            if (athlete == null) throw new KeyNotFoundException("Athlete with Id not found");
            return athlete;
        }

        public bool Create(Athlete athlete)
        {
            try
            {
                athlete.Id = _nextId++;
                _athletes.Add(athlete);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Athlete Delete(int id)
        {
            try
            {
                var athleteToDelete = Get(id);
                _athletes.Remove(athleteToDelete);
                return athleteToDelete;
            }
            catch (KeyNotFoundException knfe)
            {
                throw new KeyNotFoundException(knfe.Message);
            }
        }

        public IEnumerable<Athlete> FilterByName(string name)
        {
            return _athletes.Where(a => a.Name.ToLower().Contains(name.ToLower()));
        }

        public IEnumerable<Athlete> FilterByCountry(string country)
        {
            return _athletes.Where(a => a.Country.ToLower().Contains(country.ToLower()));
        }

        public IEnumerable<Athlete> FilterByHeight(int minHeight, int maxHeight)
        {
            return _athletes.Where(a => a.Height >= minHeight && a.Height <= maxHeight);
        }

        // FOR TESTING PURPOSES ONLY
        public void TESTCleanUp()
        {
            _athletes = new List<Athlete>()
            {
                new Athlete(1, "Henrik", "Denmark", 155.2),
                new Athlete(2, "Steven", "United Kingdom", 185.5),
                new Athlete(3, "Mikkel", "Greece", 183),
                new Athlete(4, "Oscar", "United States", 194.6),
                new Athlete(5, "Peter", "Greenland", 175.5),
                new Athlete(6, "Patrick", "China", 182),
                new Athlete(7, "Christopher", "Denmark", 183)
            };
        }
    }
}
