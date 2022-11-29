using System.ComponentModel.DataAnnotations;

namespace UserNotes.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        [ScaffoldColumn(false)]
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
