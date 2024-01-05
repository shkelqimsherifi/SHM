//#define MinimalAPI
#define ControllerAPI

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

#if ControllerAPI
builder.Services.AddControllers();
#endif

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

#if MinimalAPI

//TODO - 12.Minimal API
#region Minimal API
//Routing
app.MapGet("/API", () =>
{ 
	return "Reading all rooms"; 
});

app.MapGet("/API/{id}", (int id) =>
{
	return $"Reading room: {id}";
});

app.MapPost("/API/{id}", (int id) =>
{
	return $"Added new room with id: {id}";
}
);

app.MapPut("/API/{id}", (int id) =>
{
	return $"Updated the room witt id: {id}";
});

app.MapDelete("/API/{id}", (int id) =>
{
	return $"Deleting the room with id: {id}";
});
#endregion

#else

//TODO - 12.Controller API
#region Controller API
app.MapControllers();
#endregion

#endif

app.Run();
