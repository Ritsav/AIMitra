using AutoMapper;
using Contracts.Users;
using Domain.Users;

namespace Application.Users;

public class UserService(
    IUserRepository userRepository,
    IMapper mapper) : IUserService
{
    public async Task<UserInfoDto> GetUserInfoAsync(UserInfoRequestDto input)
    {
        var identifier = input.Username ?? input.Email;

        var user = await userRepository.GetUserByIdentifierAsync(identifier!);
        
        if (user == null)
            throw new Exception("User not found.");
        
        return mapper.Map<User, UserInfoDto>(user);
    }
}