namespace Contracts.Users;

public interface IUserService
{
    Task<UserInfoDto> GetUserInfoAsync(UserInfoRequestDto input);
}