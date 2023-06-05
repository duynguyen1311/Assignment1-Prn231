using BusinessObjects.Models;
using DataAccess.DAO;
using DataAccess.Dto;
using DataAccess.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class MemberRepository : IMemberRepository
    {
        public void DeleteMember(Member p) => MemberDAO.DeleteMember(p);
        public void SaveMember(MemberRequestDto p) => MemberDAO.SaveMember(p);
        public void UpdateMember(MemberUpdateRequest p) => MemberDAO.UpdateMember(p);
        public List<Category> GetCategories() => CategoryDAO.GetCategories();
        public List<Member>? GetMembers(string? keyword) => MemberDAO.GetMembers(keyword);
        public Member GetMemberById(int id) => MemberDAO.FindMemberById(id);
        public bool Login(string email, string password) => MemberDAO.Login(email, password);
        public void Register(MemberRequestDto member) => MemberDAO.Register(member);
    }
}
