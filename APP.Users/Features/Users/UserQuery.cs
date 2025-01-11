using APP.Users.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Users.Features.Users
{
    public class UserQueryRequest : IRequest<IQueryable<UserQueryResponse>>
    {
    }

    public class UserQueryResponse : QueryResponse
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string IsActiveF { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName { get; set; }
        public int RoleId { get; set; }
        public string Role { get; set; }
    }

    public class UserQueryHandler : UsersDbHandler, IRequestHandler<UserQueryRequest, IQueryable<UserQueryResponse>>
    {
        public UserQueryHandler(UsersDb db) : base(db)
        {
        }

        public Task<IQueryable<UserQueryResponse>> Handle(UserQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Users.Include(u => u.Role)
                .OrderBy(u => u.Name).Select(u => new UserQueryResponse()
            {
                Id = u.Id,
                Name = u.Name,
                FullName = u.Name + " " + u.Surname,
                IsActive = u.IsActive,
                IsActiveF = u.IsActive ? "Active" : "Inactive",
                Password = u.Password,
                Role = u.Role.Name,
                Surname = u.Surname,
                UserName = u.UserName,
                RoleId = u.RoleId
            });
            return Task.FromResult(query);
        }
    }
}
