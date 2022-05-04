using System.ComponentModel.DataAnnotations;

namespace GCW.Models
{
    public class Producer
    {
        [Key]
        public int ProducerNumber { get; set; }
        public string? ProducerName { get; set; }
    }
}
