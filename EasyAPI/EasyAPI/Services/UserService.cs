using EasyAPI.Models;
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

    public async Task UpdateUser(User user) => await _userRepository.UpdateUser(user);

    public async Task<User> AddUser(User user) => await _userRepository.AddUser(user);
}
