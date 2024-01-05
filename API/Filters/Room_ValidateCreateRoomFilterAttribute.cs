using Microsoft.AspNetCore.Mvc.Filters;

namespace API.Filters
{
	public class Room_ValidateCreateRoomFilterAttribute:ActionFilterAttribute
	{
		public override void OnActionExecuting(ActionExecutingContext context)
		{
			base.OnActionExecuting(context);
		}
	}
}
