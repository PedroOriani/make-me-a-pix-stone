using Pix.DTOs;
using Pix.Exceptions;
using Pix.Models;
using Pix.Repositories;

namespace Pix.Services;

public class UserService
{
    private readonly UserRepository _userRepository;

    public UserService(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> CreateUser(CreateUserDTO dto)
    {
        User user = await _userRepository.CreateUsersync(dto.ToEntity());
        return user;
    }
}