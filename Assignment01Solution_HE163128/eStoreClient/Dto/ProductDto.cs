namespace eStoreClient.Dto
{
    public class ProductAddRequest
    {
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public double Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
    }

    public class ProductUpdateRequest
    {
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public string? ProductName { get; set; }
        public double Weight { get; set; }
        public decimal UnitPrice { get; set; }
        public int? UnitsInStock { get; set; }
    }

}
