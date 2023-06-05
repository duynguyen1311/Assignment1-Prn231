using BusinessObjects.Models;
using DataAccess.Dto;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class MemberDAO
    {

        public static bool Login(string email, string password)
        {
            bool isLogin = false;
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var mem = context.Members.FirstOrDefault(i => i.Email == email && i.Password == password);
                    if (mem != null)
                    {
                        isLogin = true;
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return isLogin;
        }

        public static void Register(MemberRequestDto member)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var mem = new Member()
                    {
                        Email = member.Email,
                        CompanyName = member.CompanyName,
                        City = member.City,
                        Country = member.Country,
                        Password = member.Password
                    };
                    context.Members.Add(mem);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static List<Member>? GetMembers(string? keyword)
        {
            List<Member>? listMembers = null;
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    if (keyword == null)
                    {
                        listMembers = context.Members.AsNoTracking().ToList();
                    }
                    else
                    {
                        listMembers = context.Members.Where(i => !string.IsNullOrEmpty(i.CompanyName) && i.CompanyName.ToLower().Contains(keyword.ToLower())
                                                              || !string.IsNullOrEmpty(i.Email) && i.Email.ToLower().Contains(keyword.ToLower())).AsNoTracking().ToList();
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return listMembers;
        }

        public static Member FindMemberById(int prodId)
        {
            Member p = new Member();
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    p = context.Members.SingleOrDefault(x => x.MemberId == prodId);
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
            return p;
        }

        public static void SaveMember(MemberRequestDto p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var mem = new Member()
                    {
                        Email = p.Email,
                        CompanyName = p.CompanyName,
                        City = p.City,
                        Country = p.Country,
                        Password = p.Password
                    };
                    context.Members.Add(mem);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void UpdateMember(MemberUpdateRequest p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var mem = new Member()
                    {
                        MemberId = p.MemberId,
                        Email = p.Email,
                        CompanyName = p.CompanyName,
                        City = p.City,
                        Country = p.Country,
                        Password = p.Password,
                    };
                    context.Entry<Member>(mem).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public static void DeleteMember(Member p)
        {
            try
            {
                using (var context = new PRN231_AS1Context())
                {
                    var p1 = context.Members.SingleOrDefault(
                        c => c.MemberId == p.MemberId);
                    context.Members.Remove(p1);
                    context.SaveChanges();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
