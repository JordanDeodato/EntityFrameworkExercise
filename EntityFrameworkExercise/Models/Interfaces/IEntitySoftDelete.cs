using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFrameworkExercise.Models.Interfaces
{
    public class IEntitySoftDelete
    {
        [Column("is_deleted")]
        public bool IsDeleted { get; set; }

        [Column("deleted_at")]
        public DateTime? DeletedAt { get; set; }
    }
}
