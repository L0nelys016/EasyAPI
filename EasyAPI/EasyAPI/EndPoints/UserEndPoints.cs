using EasyAPI.Models;
using EasyAPI.Models.DTOs;
using EasyAPI.Services;

namespace EasyAPI.EndPoints;

public static class UserEndPoints
{
    public static IEndpointRouteBuilder MapUserEndPoints(this IEndpointRouteBuilder app)
    {
        app.MapGet("/UserList", (UserService userService) =>  userService.GetUsers());
        
        app.MapGet("/UserById/{id:int}", (UserService userService, int id) => userService.GetUserById(id));
        
        app.MapDelete("/DeleteUser/{id:int}", (UserService userService, int id) => userService.DeleteUserById(id));
        
        app.MapPut("/UpdateUser/{id:int}", async (UserService userService, int id, UserDto userDto) =>
        {
            try
            {
                await userService.UpdateUser(id, userDto);
                return Results.Ok("User updated successfully");
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
        
        app.MapPost("/AddUser", async (UserService userService, UserDto userDto) =>
        {
            try
            {
                var result = await userService.AddUser(userDto);
                return Results.Created($"/UserById/{result.UserId}", result);
            }
            catch (Exception ex)
            {
                return Results.Problem(ex.Message);
            }
        });
        
        return app;
    }
}