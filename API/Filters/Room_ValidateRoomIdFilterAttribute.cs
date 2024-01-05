using API.Models.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
	public class Room_ValidateRoomIdFilterAttribute:ActionFilterAttribute
	{
		//TODO - 6.Model Validation with Action Filter
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);

			var RoomId = context.ActionArguments["id"] as int?;
			if(RoomId.HasValue)
			{
				if(RoomId.Value <= 0)
				{
					context.ModelState.AddModelError("RoomId", "RoomId is invalid");
					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Status = StatusCodes.Status400BadRequest,
					};
					context.Result = new BadRequestObjectResult(problemDetails);
				}
				else if (!RoomRepository.RoomExists(RoomId.Value)) 
				{
					context.ModelState.AddModelError("RoomId", "Room doesn't exist.");
					var problemDetails = new ValidationProblemDetails(context.ModelState)
					{
						Status = StatusCodes.Status404NotFound,
					};
					context.Result = new NotFoundObjectResult(problemDetails);
				}
			}
		}
	}
}
