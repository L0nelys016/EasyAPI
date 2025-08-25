using EasyAPI.Models;
using EasyAPI.Models.DTOs;
using EasyAPI.Repositories;
using Newtonsoft.Json;

namespace EasyAPI.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository) => this._userRepository = userRepository;

    public async Task<IResult> GetUsers() =>
        Results.Text(
            JsonConvert.SerializeObject(
                await _userRepository.GetUsers(),
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            )
        );

    public async Task<IResult> GetUserById(int userid) =>
        Results.Text(
            JsonConvert.SerializeObject(
                await _userRepository.GetUserById(userid),
                Formatting.Indented,
                new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }
            )
        );

    public async Task DeleteUserById(int userid) => await _userRepository.DeleteUserById(userid);

    public async Task UpdateUser(int id, UserDto userDto)
    {
        User user = await _userRepository.GetUserById(id);

        if (user == null)
            throw new Exception("User not found");

        user.Username = string.IsNullOrEmpty(userDto.Username) ? user.Username : userDto.Username;
        user.Email = string.IsNullOrEmpty(userDto.Email) ? user.Email : userDto.Email;
        user.Password = string.IsNullOrEmpty(userDto.Password) ? user.Password : userDto.Password;
        user.FullName = string.IsNullOrEmpty(userDto.FullName) ? user.FullName : userDto.FullName;

        await _userRepository.SaveChangesAsync();
    }

    public async Task<User> AddUser(UserDto userDto)
    {
        User user = new User
        {
            Username = userDto.Username,
            Email = userDto.Email,
            Password = userDto.Password,
            FullName = userDto.FullName,
            CreatedAt = DateTime.Now,
            IsActive = true,
        };

        User result = await _userRepository.AddUser(user);
        return result;
    }
}
