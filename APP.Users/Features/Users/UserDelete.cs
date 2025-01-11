using APP.Users.Domain;
using CORE.APP.Domain;
using CORE.APP.Features;
using MediatR;

namespace APP.Users.Features.Users
{
    public class UserDeleteRequest : IRecord, IRequest<CommandResponse>
    {
        public int Id { get; set; }
    }

    public class UserDeleteHandler : UsersDbHandler, IRequestHandler<UserDeleteRequest, CommandResponse>
    {
        public UserDeleteHandler(UsersDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(UserDeleteRequest request, CancellationToken cancellationToken)
        {
            var user = _db.Users.Find(request.Id);
            if (user is null)
                return Error("User not found!");
            _db.Users.Remove(user);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("User deleted successfully", user.Id);
        }
    }
}
