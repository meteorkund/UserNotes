using Microsoft.Build.Framework;

namespace UserNotes.Models
{
    public class UserNoteChild : BaseEntity
    {
        public int UserNoteId { get; set; }
        [Required]
        public string Content { get; set; }
        public virtual UserNote? UserNote { get; set; }
    }
}
