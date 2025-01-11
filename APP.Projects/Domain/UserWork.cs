using CORE.APP.Domain;
using System.ComponentModel.DataAnnotations;

namespace APP.Projects.Domain
{
    public class UserWork : IRecord
    {
        public int Id { get; set; }

        [Required]
        [StringLength(75)]
        public string UserName { get; set; }

        [StringLength(250)]
        public string Url { get; set; }

        public decimal Percentage { get; set; }

        public DateTime? EndDate { get; set; }

        public int WorkId { get; set; }

        public Work Work { get; set; }
    }
}
