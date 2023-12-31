﻿using API.Entities;

namespace API;

public interface IUserRepository
{
    void Update(AppUser user);
    Task<IEnumerable<AppUser>> GetUsersAsync();
    Task<AppUser> GetUserByIDAsync(int id);
    Task<AppUser> GetUserByUserNameAsync(string username);
    Task<MemberDto> GetMemberAsync(string username);
}
