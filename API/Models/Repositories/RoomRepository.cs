namespace API.Models.Repositories
{
	public static class RoomRepository
	{
		private static List<RoomModel> rooms = new List<RoomModel>()
		{
			new RoomModel { RoomId = 1, Type = "Single", Floor = "1", Size = 5, Price = 75.00 },
			new RoomModel { RoomId = 2, Type = "Double", Floor = "2", Size = 15, Price = 105.00 },
			new RoomModel { RoomId = 3, Type = "Suit", Floor = "3", Size = 12, Price = 155.00 },
			new RoomModel { RoomId = 4, Type = "Single", Floor = "3", Size = 8, Price = 75.00 }
		};

		public static List<RoomModel> GetRooms()
		{
			return rooms;
		}

		public static bool RoomExists(int id)
		{
			return rooms.Any(x=>x.RoomId == id);
		}
		public static RoomModel? GetRoomById(int id)
		{
			return rooms.FirstOrDefault(x=>x.RoomId == id);
		}

		public static RoomModel? GetRoomByProperty(string? type, string? floor, int size, double price)
		{
			return rooms.FirstOrDefault(x =>
				!string.IsNullOrWhiteSpace(type) &&
				!string.IsNullOrWhiteSpace(x.Type) &&
				x.Type.Equals(type, StringComparison.OrdinalIgnoreCase) &&
				!string.IsNullOrWhiteSpace(floor) &&
				!string.IsNullOrWhiteSpace(x.Floor) &&
				x.Floor.Equals(floor, StringComparison.OrdinalIgnoreCase) &&
				size.Equals(x.Size) &&
				price.Equals(x.Price)
				);
		}

		public static void AddRoom(RoomModel room)
		{
			int maxId = rooms.Max(x=>x.RoomId);
			room.RoomId = maxId + 1;
			rooms.Add(room);
		}
	}
}
