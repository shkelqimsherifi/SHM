using System.ComponentModel.DataAnnotations;
using API.Models.Validations;

namespace API.Models
{
	public class RoomModel
	{
        [Required]
        public int RoomId { get; set; }
        public string? Type { get; set; }
        public string? Floor { get; set; }
        //TODO - 6.Calling Validation Attributes
        [Required]
        [Validate_RoomModel_Id]
        public int Size { get; set; }
        [Required]
        public double Price { get; set; }
    }
}
