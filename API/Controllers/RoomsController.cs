using API.Filters;
using API.Models;
using API.Models.Repositories;
using HotelProject.Models;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
	//TODO - 12.Create API with Controller
	[ApiController]
	[Route("API/[controller]")]
	public class RoomsController:Controller
	{
		[HttpGet]
		public IActionResult GetRooms()
		{
			return Ok(RoomRepository.GetRooms());
		}

		[HttpGet("{id}")]
		public IActionResult GetRoomById(int id)
		{
			return Ok($"Reading room: {id}");
		}

		[HttpGet("{id}/{type}")]
		public IActionResult GetRoomById(int id, [FromRoute] string type)
		{
			return Ok($"Reading room: {id} and type: {type}");
		}

		[HttpPost("{id}")]
		public IActionResult AddRoomById(int id)
		{
			return Ok($"Added new room with id: {id}");
		}

		[HttpPost]
		public IActionResult AddRoom([FromBody] RoomModel room)
		{
			if(room == null) { return BadRequest(); }

			var existingRoom = RoomRepository.GetRoomByProperty(room.Type, room.Floor, room.Size, room.Price);
			if (existingRoom != null) { return BadRequest(); }
			RoomRepository.AddRoom(room);

			return CreatedAtAction(nameof(GetRoomById), new { id = room.RoomId }, room);
		}
		//[HttpPost]
		//public IActionResult AddRoomById([FromBody]RoomModel room)
		//{
		//	return Ok($"Added new room with id: {room.RoomId}");
		//}

		[HttpPut("{id}")]
		public IActionResult UpdateRoomById(int id)
		{
			return Ok($"Updated the room witt id: {id}");
		}

		[HttpDelete("{id}")]
		public IActionResult DeleteRoomById(int id)
		{
			return Ok($"Deleting the room with id: {id}");
		}

		[HttpGet("obj/{id}")]
		[Room_ValidateRoomIdFilter]
		public IActionResult GetRoomModelById(int id)
		{
			return Ok(RoomRepository.GetRoomById(id));
		}
	}
}
