using BusinessObjects.Models;
using DataAccess.Dto;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MemberController : ControllerBase
    {
        private IMemberRepository repository = new MemberRepository();

        [HttpPost("Login")]
        public ActionResult<bool> Login([FromBody] LoginRequest request) => repository.Login(request.Email, request.Password);

        [HttpPost("Register")]
        public IActionResult Register(MemberRequestDto member)
        {
            repository.Register(member);
            return NoContent();
        }
        // GET: api/Members
        [HttpGet("GetAllMember")]
        public ActionResult<IEnumerable<Member>> GetMembers(string? keyword) => repository.GetMembers(keyword);

        [HttpPost("AddMember")]
        public IActionResult AddMember(MemberRequestDto p)
        {
            repository.SaveMember(p);
            return NoContent();
        }

        [HttpGet("Detail/{id}")]
        public ActionResult<Member> GetMemberById(int id) => repository.GetMemberById(id);

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteMember(int id)
        {
            var p = repository.GetMemberById(id);
            if (p == null)
            {
                return NotFound("Can not found Member to delete");
            }
            repository.DeleteMember(p);
            return NoContent();
        }

        [HttpPut("Update")]
        public IActionResult UpdateMember(MemberUpdateRequest p)
        {
            var pTmp = repository.GetMemberById(p.MemberId);
            if (pTmp == null)
            {
                return NotFound($"Can not find Member have name {p.CompanyName}");
            }
            repository.UpdateMember(p);
            return NoContent();
        }
    }
}
