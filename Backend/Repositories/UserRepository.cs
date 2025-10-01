using Backend.Models.Context;
using Backend.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Backend.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> CreateUserAsync(UserEntity user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user.Id;
    }

    public async Task<int> UpdateUserDataAsync(UserDataEntity userData)
    {
        UserDataEntity? userDataEntity = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.Id == userData.Id);
        if (userDataEntity == null) await _context.UserDatas.AddAsync(userData);
        else _context.UserDatas.Entry(userDataEntity).CurrentValues.SetValues(userData);

        await _context.SaveChangesAsync();
        return userData.Id;
    }

    public async Task<List<UserEntity>> GetUsersAsync()
    {
        return await _context.Users.Include(u => u.UserData).OrderBy(u => u.UserData.Surname).ToListAsync();
    }

    public async Task<UserEntity?> GetByIdAsync(int userId)
    {
        return await _context.Users.Include(u => u.UserData).Include(u => u.Role).FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<bool> IsEmailExist(string email)
    {
        return await _context.Users.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> IsLoginExist(string login)
    {
        return await _context.Users.AnyAsync(u => u.Login == login);
    }

    public async Task<UserEntity?> GetByEmailAsync(string email)
    {
        return await _context.Users.Include(u => u.UserData).Include(u => u.Role).FirstOrDefaultAsync(u => u.Email == email);
    }

    public async Task<UserEntity?> GetByLoginAsync(string login)
    {
        return await _context.Users.Include(u => u.UserData).Include(u => u.Role).FirstOrDefaultAsync(u => u.Login == login);
    }

    public async Task RemoveByIdAsync(int userId)
    {
        UserEntity? userEntity = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
        UserDataEntity? userDataEntity = await _context.UserDatas.FirstOrDefaultAsync(ud => ud.Id == userId);

        if (userEntity != null) _context.Users.Remove(userEntity);
        if (userDataEntity != null) _context.UserDatas.Remove(userDataEntity);

        await _context.SaveChangesAsync();
    }
}