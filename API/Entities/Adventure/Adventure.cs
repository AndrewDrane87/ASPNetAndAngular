using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities.Adventure
{
    public class Adventure
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Location StartingLocation { get; set; }
        public List<Location> Locations { get; set; }
        public List<AdventureVariable> Variables { get; set; }
    }

    



    public class AdventureVariable
    {
        public int Id { get; set; }
        public string VariableType { get; set; }
        public string Value { get; set; }
    }
}
