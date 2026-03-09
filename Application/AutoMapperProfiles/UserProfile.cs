using AutoMapper;
using Contracts.Users;
using Domain.Users;

namespace Application.AutoMapperProfiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserInfoDto>();
    }
}