using System.ComponentModel.DataAnnotations;

namespace Models
{

    public class Comment
    {

        [Key]
        public int Id { get; set; }
        public required string UserComment { get; set; }
        public required string Name { get; set; }
        public required string Time { get; set; }
        public required string IP { get; set; }

    }
}







