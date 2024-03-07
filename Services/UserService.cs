using Pix.DTOs;
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

    public User CreateUser(CreateUserDTO dto)
    {
        User user = _userRepository.CreateUser(dto.ToEntity());
        return user;
    }
}