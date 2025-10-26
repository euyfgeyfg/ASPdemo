using System;
using APIDemo.Entities;

namespace APIDemo.Interfaces;

public interface IMemberRepository
{
    void Update(Member member);
    Task<bool> SaveAllAsyc();
    Task<IReadOnlyList<Member>> GetMembersAsync();
    Task<Member?> GetMemberByIdAsync(string id);
    Task<IReadOnlyList<Photo>> GetPhotosForMemberAsync(string memberId);
}
