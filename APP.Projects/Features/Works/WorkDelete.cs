﻿using APP.Projects.Domain;
using CORE.APP.Domain;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Features.Works
{
    public class WorkDeleteRequest : IRecord, IRequest<CommandResponse>
    {
        public int Id { get; set; }
    }

    public class WorkDeleteHandler : ProjectsDbHandler, IRequestHandler<WorkDeleteRequest, CommandResponse>
    {
        public WorkDeleteHandler(ProjectsDb db) : base(db)
        {
        }

        public async Task<CommandResponse> Handle(WorkDeleteRequest request, CancellationToken cancellationToken)
        {
            var work = _db.Works.Include(w => w.UserWorks).SingleOrDefault(w => w.Id == request.Id);
            if (work is null)
                return Error("Work not found!");
            _db.UserWorks.RemoveRange(work.UserWorks);
            _db.Works.Remove(work);
            await _db.SaveChangesAsync(cancellationToken);
            return Success("Work deleted successfully", work.Id);
        }
    }
}
