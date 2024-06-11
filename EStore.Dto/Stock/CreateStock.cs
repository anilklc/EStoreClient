namespace EStore.Dto.Stock
{
    public class CreateStock
    {
        public string ProductSize { get; set; }
        public int ProductStock { get; set; }
        public Guid ProductId { get; set; }
    }
}
