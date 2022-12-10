using E_HealthCareApplication.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace E_HealthCareApplication.Controllers
{
    [ApiController]
    [Route("Api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly HealthCareDbContext _context;

        public AccountController(HealthCareDbContext context)
        {
            _context = context;
        }

        [HttpGet("getAllUserAccounts")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<List<Account>>> GetAllAccounts()
        {
            return Ok(await _context.Accounts.ToListAsync());
        }

        [HttpGet("getAccountInfoByEmail")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Account>> GetAccountDetailsByEmail(string email)
        {
            var account = await _context.Accounts.Where(a => a.Email == email).FirstOrDefaultAsync();

            if (account is null)
                return BadRequest("Account doesn't exist");

            return Ok(account);
        }

        [HttpGet("getAccountInfoById")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Account>> GetAccountDetailsbyId(int id)
        {
            var account = await _context.Accounts.Where(a => a.Id == id).FirstOrDefaultAsync();

            if (account is null)
                return BadRequest("Account doesn't exist");

            return Ok(account);
        }

        [HttpPut("EditAccountById")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Account>> EditAccountbyId(Account newdata, int id)
        {

            if (id != newdata.Id)
                return BadRequest("Account doesn't exist");

            newdata.Id = id;
            newdata.Email = newdata.Email;
            newdata.Address = newdata.Address;
            newdata.DateOfBirth = newdata.DateOfBirth;
            newdata.FirstName = newdata.FirstName;
            newdata.LastName = newdata.LastName;
            newdata.Phone = newdata.Phone;
            newdata.Password = newdata.Password;
            newdata.Admin = newdata.Admin;

            _context.Entry(newdata).State = EntityState.Modified;

            await _context.SaveChangesAsync();

            return Ok(newdata);
        }

        [HttpDelete("DeleteAccountById")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Account>> DeleAccountbyId(int id)
        {
            var account = await _context.Accounts.FindAsync(id);

            if (account is null)
                return BadRequest("Account doesn't exist");

            _context.Remove(account);
            await _context.SaveChangesAsync();

            return Ok("Account is removed");
        }
    }
}
