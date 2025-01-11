using APP.Users.Domain;
using CORE.APP.Domain;
using CORE.APP.Features;
using MediatR;
using System.ComponentModel.DataAnnotations;

namespace APP.Users.Features.Users
{
    public class UserUpdateRequest : IRecord, IRequest<CommandResponse>
    {
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string UserName { get; set; }

        [Required]
        [StringLength(15, MinimumLength = 3)]
        public string Password { get; set; }

        public bool IsActive { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        [StringLength(50)]
        public string Surname { get; set; }

        public int RoleId { get; set; }
    }

    public class UserUpdateHandler : UsersDbHandler, IRequestHandler<UserUpdateRequest, CommandResponse>
    {
        public UserUpdateHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(UserUpdateRequest request, CancellationToken cancellationToken)
        {
            if (_db.Users.Any(u => u.Id != request.Id && (u.UserName == request.UserName || (u.Name == request.Name && u.Surname == request.Surname))))
                return Error("User with the same user name or full name exists!");
            var user = _db.Users.Find(request.Id);
            if (user is null)
                return Error("User not found!");
            user.IsActive = request.IsActive;
            user.Name = request.Name;
            user.Password = request.Password;
            user.RoleId = request.RoleId;
            user.Surname = request.Surname;
            user.UserName = request.UserName;
            _db.Users.Update(user);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("User updated successfully.", user.Id);
        }
    }
}
