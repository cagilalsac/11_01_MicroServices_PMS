using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Projects.Domain
{
    public class Work : IRecord
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime DueDate { get; set; }

        public int? ProjectId { get; set; }

        public Project _Project { get; set; }

        public List<UserWork> _UserWorks { get; set; } = new List<UserWork>();
    }
}
