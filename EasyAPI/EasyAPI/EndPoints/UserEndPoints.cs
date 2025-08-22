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

        app.MapPut("/UpdateUser", (UserService userService, User user) => userService.UpdateUser(user));
        
        app.MapPost("/AddUser", async (UserService userService, UserDto userDto) =>
        {
            try
            {
                User user = new User
                {
                    Username = userDto.Username,
                    Email = userDto.Email,
                    Password = userDto.Password,
                    FullName = userDto.FullName,
                    CreatedAt = DateTime.UtcNow,
                    IsActive = true
                };
        
                var result = await userService.AddUser(user);
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