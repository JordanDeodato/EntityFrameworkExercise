namespace EntityFrameworkExercise.Dto
{
    public class SaleCreateRequest
    {
        public required DateTime Date { get; set; }
        public required int SellerId { get; set; }
        public required int CustomerId { get; set;}

        public required List<Models.Product> Products { get; set; }
    }
}
