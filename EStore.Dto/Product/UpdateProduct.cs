namespace EStore.Dto.Product
{
    public class UpdateProduct
    {
        public Guid Id { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public string ProductCoverImagePath { get; set; }
        public float Price { get; set; }
    }
}
