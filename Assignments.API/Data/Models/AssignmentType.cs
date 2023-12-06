using System.ComponentModel.DataAnnotations;

namespace Assignments.API.Data.Models
{
    public class AssignmentType
    {
        [Key]
        public int AssignmentTypeId { get; set; }

        [StringLength(50)]
        public string AssignmentTypeName { get; set; } = default!;

        public ICollection<Assignment> Assignments { get; set; } = default!;
    }
}
