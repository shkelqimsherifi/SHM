using System.ComponentModel.DataAnnotations;

namespace API.Models.Validations
{
	//TODO - 6.Validation Attribute - Custom
	public class Validate_RoomModel_IdAttribute:ValidationAttribute
	{
		protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
		{
			var room = validationContext.ObjectInstance as RoomModel;
			if(room != null)
			{
				if(room.Size <3 || room.Size >55)
				{
					return new ValidationResult("Room Id can't be less than 3 or greater than 55.");
				}
			}
			return ValidationResult.Success;
		}
	}
}
