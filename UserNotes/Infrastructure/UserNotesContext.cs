using Microsoft.EntityFrameworkCore;
using UserNotes.Models;

namespace UserNotes.Infrastructure
{
    public class UserNotesContext : DbContext
    {
        public UserNotesContext(DbContextOptions<UserNotesContext> options) : base(options)
        {

        }

        public DbSet<UserNote> UserNotes { get; set; }
        public DbSet<UserNoteChild> UserNoteChildren { get; set; }


        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {

            var datas = ChangeTracker
                 .Entries<BaseEntity>();

            foreach (var data in datas)
            {
                _ = data.State switch
                {
                    EntityState.Added => data.Entity.CreatedDate = DateTime.Now,
                    _ => DateTime.Now
                };
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}
