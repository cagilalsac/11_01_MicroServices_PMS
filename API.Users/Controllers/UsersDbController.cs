using APP.Users.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Users.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersDbController : ControllerBase
    {
        private readonly UsersDb _db;

        public UsersDbController(UsersDb db)
        {
            _db = db;
        }

        [HttpGet("Seed")]
        public IActionResult Seed()
        {
            var users = _db.Users.ToList();
            _db.Users.RemoveRange(users);
            var roles = _db.Roles.ToList();
            _db.Roles.RemoveRange(roles);

            if (users.Any() || roles.Any())
            {
                _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Users', RESEED, 0)");
                _db.Database.ExecuteSqlRaw("dbcc CHECKIDENT ('Roles', RESEED, 0)");
            }

            _db.Roles.Add(new Role()
            {
                Name = "Admin",
                _Users = new List<User>()
                {
                    new User()
                    {
                        IsActive = true,
                        Name = "Çağıl",
                        Password = "admin",
                        Surname = "Alsaç",
                        UserName = "admin"
                    }
                }
            });
            _db.Roles.Add(new Role()
            {
                Name = "User",
                _Users = new List<User>()
                {
                    new User()
                    {
                        IsActive = true,
                        Name = "Leo",
                        Password = "user",
                        Surname = "Alsaç",
                        UserName = "user"
                    }
                }
            });

            _db.SaveChanges();

            return Ok("Database seed successful.");
        }
    }
}
