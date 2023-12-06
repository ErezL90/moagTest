using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Assignments.API.Data.Models
{
    public class Assignment
    {
        [Key]
        public int AssignmentId { get; set; }

        [Required]
        [StringLength(50)]
        public string AssignmentName { get; set; } = default!;
        [StringLength(250)]
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        [Required]
        public bool IsPeriodic { get; set; } = false;
        [Required]
        public bool IsCompleted { get; set; } = false;
        [Required]
        public bool IsArchive { get; set; } = false;


        [ForeignKey("AssignmentType")]
        public int AssignmentTypeId { get; set; } 
        public AssignmentType AssignmentType { get; set; } = default!; //Navigation property 




    }
}
