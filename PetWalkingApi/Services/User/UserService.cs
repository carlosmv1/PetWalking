using PetWalkingApi.Common;
using PetWalkingApi.Data;
using PetWalkingApi.Models;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly AppDbContext _context;

    public UserService(AppDbContext context)
    {
        _context = context;
    }

    public async Task HandlePostUserCreationAsync(User user)
    {
        var userType = await _context.UserTypes
            .FirstOrDefaultAsync(ut => ut.UserTypeId == user.UserTypeId);

        if (userType != null && userType.Type == UserRoles.Walker)
        {
            var calendar = new Calendar
            {
                UserId = user.UserId,
                Status = UserStatus.Active
            };

            _context.Calendars.Add(calendar);
            await _context.SaveChangesAsync();
        }
    }
}
