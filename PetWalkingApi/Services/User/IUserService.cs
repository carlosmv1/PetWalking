public interface IUserService
{
    Task HandlePostUserCreationAsync(User user);
}