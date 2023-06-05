using BusinessObjects.Models;
using DataAccess.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.IRepository
{
    public interface IMemberRepository
    {
        bool Login(string email, string password);
        void Register(MemberRequestDto member);
        void SaveMember(MemberRequestDto p);
        Member GetMemberById(int id);
        void DeleteMember(Member p);
        void UpdateMember(MemberUpdateRequest p);
        List<Member>? GetMembers(string? keyword);
    }
}
