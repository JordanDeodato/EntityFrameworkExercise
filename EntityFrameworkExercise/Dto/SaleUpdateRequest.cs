namespace EntityFrameworkExercise.Dto
{
    public class SaleUpdateRequest
    {
        public required DateTime Date { get; set; }
        public required int SellerId { get; set; }
        public required int CustomerId { get; set; }
    }
}
