using System.ComponentModel.DataAnnotations;

namespace UserNotes.Models
{
    public class UserNote : BaseEntity
    {
        [Required]
        public string Content { get; set; }
        public ICollection<UserNoteChild>? UserNoteChildrens { get; set; }
    }
}
