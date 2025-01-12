using CORE.APP.Domain;

namespace APP.Projects.Domain
{
    public class ProjectTag : IRecord
    {
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public Project _Project { get; set; }
        public int TagId { get; set; }
        public Tag _Tag { get; set; }
    }
}
