using System.ComponentModel.DataAnnotations;

namespace Abbott_Pharma.Models
{
    public class Liquid
    {
        [Key]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Air_Pressure { get; set; }
        public string Air_Volume { get; set; }
        public string Filling_Speed { get; set; }
        public string Filling_Range { get; set; }
        public string Image { get; set; }
    }
}
