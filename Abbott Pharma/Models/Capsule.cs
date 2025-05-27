using System.ComponentModel.DataAnnotations;

namespace Abbott_Pharma.Models
{
    public class Capsule
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Output { get; set; }
        public string Capsule_Size { get; set; }
        public string Material { get; set; }
        public string Dimensions { get; set; }
        public string Weight { get; set; }
        public string Image { get; set; }

    }
}
