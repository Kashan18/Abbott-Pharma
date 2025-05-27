using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace Abbott_Pharma.Models
{
    public class Tablet
    {
         [Key]
        public int Id { get; set; }
        public string ModelNumber { get; set; }
        public string Dies { get; set; }
        public string MaxPressure { get; set; }
        public string MaxDiameter { get; set; }
        public string MaxDepth { get; set; }
        public string ProductionCapacity { get; set; }
        public string MachineSize { get; set; }
        public string NetWeight { get; set; }
        public string Image { get; set; }
    }
}
