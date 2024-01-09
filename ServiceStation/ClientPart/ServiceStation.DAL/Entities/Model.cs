using System.ComponentModel.DataAnnotations.Schema;

namespace ServiceStation.DAL.Entities
{
    public class Model
    {
        public Model(string name)
        {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        [NotMapped]
        public List<Job> Jobs { get; set; }
    }
}
