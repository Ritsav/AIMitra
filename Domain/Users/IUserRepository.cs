namespace Domain.Users;

public interface IUserRepository
{
    /// <summary>
    /// It is used to query using an identifier for getting the user details
    /// </summary>
    /// <param name="identifier">Can be email or username</param>
    /// <returns>User details or null if no user found</returns>
    public Task<User?> GetUserByIdentifierAsync(string identifier);
}