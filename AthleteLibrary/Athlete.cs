using System;

namespace AthleteLibrary
{
    public class Athlete
    {
        public int Id { get; set; }      
        public string Name { get; set; }
        public string Country { get; set; }
        public double Height { get; set; }

        public Athlete()
        {

        }

        public Athlete(int id, string name, string country, double height)
        {
            Id = id;
            Name = name;
            Country = country;
            Height = height;
        }

        public override bool Equals(object? obj)
        {
            if (obj == null) return false;
            try
            {
                var otherAthlete = (Athlete)obj;
                if (this.Id.Equals(otherAthlete.Id)  && this.Name.Equals(otherAthlete.Name) &&
                    this.Country.Equals(otherAthlete.Country) && this.Height.Equals(otherAthlete.Height)) return true;
                return false;
            }
            catch (InvalidCastException)
            {
                return false;
            }
        }

        public override string ToString()
        {
            return $"Athlete {Id}: {Name} from {Country}. {Height} cm tall";
        }
    }
}
