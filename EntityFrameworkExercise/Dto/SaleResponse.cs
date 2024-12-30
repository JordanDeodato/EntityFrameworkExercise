namespace EntityFrameworkExercise.Dto
{
    public class SaleResponse
    {
        public long Id { get; set; }
        public required DateTimeOffset Date { get; set; }
        public required int SellerId { get; set; }
        public required int CustomerId { get; set; }
    }
}
