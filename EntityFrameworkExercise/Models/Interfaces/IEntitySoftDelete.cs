namespace EntityFrameworkExercise.Models.Interfaces
{
    public class IEntitySoftDelete
    {
        public bool IsDeleted { get; set; }
        public DateTime? DeletedAt { get; set; }
    }
}
