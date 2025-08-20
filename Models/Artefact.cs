using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Artefact
    {
        [Key]
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

    }
}