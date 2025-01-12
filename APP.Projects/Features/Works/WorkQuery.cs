﻿using APP.Projects.Domain;
using APP.Projects.Features.Projects;
using CORE.APP.Features;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace APP.Projects.Features.Works
{
    public class WorkQueryRequest : IRequest<IQueryable<WorkQueryResponse>>
    {
    }

    public class WorkQueryResponse : QueryResponse
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public string StartDateF { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateF { get; set; }
        public int? ProjectId { get; set; }
        public ProjectQueryResponse Project { get; set; }
        //public List<UserWorkQueryResponse> UserWorks { get; set; }
    }

    public class WorkQueryHandler : ProjectsDbHandler, IRequestHandler<WorkQueryRequest, IQueryable<WorkQueryResponse>>
    {
        public WorkQueryHandler(ProjectsDb db) : base(db)
        {
        }

        public Task<IQueryable<WorkQueryResponse>> Handle(WorkQueryRequest request, CancellationToken cancellationToken)
        {
            var query = _db.Works.Include(w => w._Project).ThenInclude(p => p._ProjectTags).ThenInclude(pt => pt._Tag)
                .OrderByDescending(w => w.DueDate).ThenByDescending(w => w.StartDate).ThenBy(w => w.Name)
                .Select(w => new WorkQueryResponse()
            {
                Description = w.Description,
                DueDate = w.DueDate,
                DueDateF = w.DueDate.ToString("MM/dd/yyyy HH:mm:ss"),
                Id = w.Id,
                Name = w.Name,
                Project = w.ProjectId.HasValue ? new ProjectQueryResponse()
                {
                    Description = w._Project.Description,
                    Id = w._Project.Id,
                    Name = w._Project.Name,
                    TagIds = w._Project.TagIds,
                    Tags = string.Join(", ", w._Project._ProjectTags.Select(pt => pt._Tag.Name)),
                    Url = w._Project.Url,
                    Version = w._Project.Version
                } : null,
                ProjectId = w.ProjectId,
                StartDate = w.StartDate,
                StartDateF = w.StartDate.ToShortDateString()
            });
            return Task.FromResult(query);
        }
    }
}
